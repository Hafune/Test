using System.Collections;
using Core.Components;
using JetBrains.Annotations;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using UnityEngine;
using Voody.UniLeo.Lite;

public class HitPointBar : MonoConstruct
{
    [SerializeField] private ConvertToEntity _convertToEntity;
    [SerializeField] private Transform _slider;
    [SerializeField] private GameObject _container;
    private float _visibleTimer;
    private bool _colorWasChanged;
    private SpriteRenderer[] _sprites;
    [CanBeNull] private Coroutine _fadeCoroutine;

    private const float _maxFadeTime = .2f;
    private const float _maxVisibleTime = 2f;

    private Context _context;
    private EcsPool<UiValue<HitPointValueComponent>> _hitPointValuePool;
    private EcsPool<UiValue<HitPointMaxValueComponent>> _hitPointMaxValuePool;
    private float _value = 1;
    private float _valueMax = 1;
    private bool _valueIsInitialized;
    private bool _valueMaxIsInitialized;

    protected override void Construct(Context context) => _context = context;

    private void Awake()
    {
        var world = _context.Resolve<EcsWorld>();
        _hitPointValuePool = world.GetPool<UiValue<HitPointValueComponent>>();
        _hitPointMaxValuePool = world.GetPool<UiValue<HitPointMaxValueComponent>>();
        _sprites = GetComponentsInChildren<SpriteRenderer>();
        _container.SetActive(false);
        _convertToEntity.OnEntityWasConnected += Reload;
        Reload(_convertToEntity);
    }

    private void OnDestroy() => _convertToEntity.OnEntityWasConnected -= Reload;

    private void OnEnable()
    {
        _valueIsInitialized = false;
        _valueMaxIsInitialized = false;
    }

    private void OnDisable()
    {
        if (_fadeCoroutine is not null)
        {
            StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = null;
        }

        _container.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (transform.rotation == Quaternion.identity)
            return;

        transform.rotation = Quaternion.identity;
    }

    private void Reload(ConvertToEntity _)
    {
        var entity = _convertToEntity.RawEntity;

        if (entity == -1)
            return;

        ref var value = ref _hitPointValuePool.GetOrInitialize(entity);
        ref var valueMax = ref _hitPointMaxValuePool.GetOrInitialize(entity);
        value.update -= UpdateValue;
        value.update += UpdateValue;
        valueMax.update -= UpdateMaxValue;
        valueMax.update += UpdateMaxValue;
    }

    private void UpdateValue<T>(T c) where T : struct, IValue => SetValue(c.value);
    private void UpdateMaxValue<T>(T c) where T : struct, IValue => SetValueMax(c.value);

    private void SetValue(float value)
    {
        Change(_value = value, _valueMax);
        _valueIsInitialized = true;
    }

    private void SetValueMax(float value)
    {
        Change(_value, _valueMax = value);
        _valueMaxIsInitialized = true;
    }

    private void Change(float min, float max)
    {
        var percent = Mathf.Max(min / max, 0);
        _slider.localScale = new Vector3(percent, 1, 1);
        _visibleTimer = _maxVisibleTime;

        if (_colorWasChanged)
        {
            _colorWasChanged = false;
            foreach (var sprite in _sprites)
                sprite.color = Color.white;
        }

        if (_fadeCoroutine is not null ||
            percent == 1f ||
            !_valueIsInitialized ||
            !_valueMaxIsInitialized)
            return;

        _container.SetActive(true);
        _fadeCoroutine = StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        while (_visibleTimer > 0)
        {
            if (_visibleTimer < _maxFadeTime)
            {
                _colorWasChanged = true;
                foreach (var sprite in _sprites)
                    sprite.color = new Color(1, 1, 1, _visibleTimer / _maxFadeTime);
            }

            _visibleTimer -= Time.deltaTime;
            yield return null;
        }

        _fadeCoroutine = null;
        _container.SetActive(false);
    }
}