using Lib;
using Reflex;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EffectWithText : MonoConstruct
{
    [SerializeField] private TextMeshPro _text;

    private Transform _uiCameraTransform;
    private Transform _selfTransform;

    protected override void Construct(Context context)
    {
        _uiCameraTransform = context.Resolve<Camera>().transform;
        _selfTransform = transform;
    }

    public void SetText(string value) => _text.SetText(value);
    
    private void FixedUpdate() => _selfTransform.rotation = _uiCameraTransform.rotation;

    //Метод для аниматора
    private void OnAnimationEnded() => gameObject.SetActive(false);
}