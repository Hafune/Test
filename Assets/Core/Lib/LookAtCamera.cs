using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class LookAtCamera : MonoBehaviour
{
    private Transform _uiCameraTransform;
    private Transform _selfTransform;
    
    private void Start()
    {
        _uiCameraTransform = GetComponent<Canvas>().worldCamera.transform;
        _selfTransform = transform;
    }

    private void FixedUpdate() => _selfTransform.rotation = _uiCameraTransform.rotation;
}