using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using JetBrains.Annotations;
using Lib;
using Reflex;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarWithDoTween : MonoConstruct
{
    public event Action<HealthBarWithDoTween> OnDeactivate;

    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Slider _slider;
    [SerializeField] private Slider _sliderDamage;

    private Vector3 _positionOffset;
    private Transform _uiCameraTransform;
    private Transform _selfTransform;
    private Context _context;

    [CanBeNull] private TweenerCore<float, float, FloatOptions> _tween;
    private bool _isFirstValuesAfterEnable;

    private void OnValidate()
    {
        _text = _text ??= GetComponentInChildren<TextMeshProUGUI>();
        _slider = _slider ??= GetComponentInChildren<Slider>();
        _sliderDamage = _sliderDamage ??= GetComponentInChildren<Slider>();
    }

    protected override void Construct(Context context) => _context = context;

    private void Awake()
    {
        _uiCameraTransform = _context.Resolve<Camera>().transform;
        _selfTransform = transform;
    }

    private void OnEnable()
    {
        _slider.gameObject.SetActive(true);
        _slider.normalizedValue = 1;
        _sliderDamage.normalizedValue = 1;
        _isFirstValuesAfterEnable = true;
    }

    private void OnDisable() => OnDeactivate?.Invoke(this);

    private void OnDestroy()
    {
        OnDeactivate?.Invoke(this);
        _sliderDamage.DOKill();
    }

    private void FixedUpdate() => _selfTransform.rotation = _uiCameraTransform.rotation;

    private void ChangeInstantly(float percent)
    {
        _slider.value = percent;
        _sliderDamage.value = percent;
    }

    public void Change(float min, float max)
    {
        var percent = Mathf.Max(min / max, 0);

        if (_text)
            _text.text = ((int)Mathf.Max(min, 0)).ToString();
        
        if (_isFirstValuesAfterEnable)
        {
            ChangeInstantly(percent);
            _isFirstValuesAfterEnable = false;
            return;
        }

        _slider.value = percent;

        if (ReferenceEquals(_tween, null))
        {
            // _tween = _sliderDamage.DOValue(percent, _duration).SetDelay(.2f);
            _tween.SetAutoKill(false);
            return;
        }

        _tween.ChangeValues(_sliderDamage.value, percent);
        
        if (!_tween.IsPlaying())
            _tween.Restart();
    }

    public void SetVisible(bool b) => _slider.gameObject.SetActive(b);
}