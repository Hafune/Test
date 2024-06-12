using Lib;
using Reflex;
using UnityEngine;

public class LookAtCameraEffect : MonoConstruct
{
    private Transform _uiCameraTransform;
    private Transform _selfTransform;

    protected override void Construct(Context context)
    {
        _uiCameraTransform = context.Resolve<Camera>().transform;
        _selfTransform = transform;
    }
    
    private void FixedUpdate() => _selfTransform.rotation = _uiCameraTransform.rotation;

    //Метод для аниматора
    private void OnAnimationEnded() => gameObject.SetActive(false);
}