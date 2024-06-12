using System;
using Cinemachine;
using UnityEngine;

namespace Core.Lib
{
    public class CameraFieldOfViewUpdater : MonoBehaviour
    {
        [SerializeField] private Vector2 _baseAspectRatio = new(16, 9);
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineVirtualCamera _vCam;

        private float _baseFov;
        private float _baseRatio;
        private float _lastFov;

        private void OnValidate()
        {
            _camera = _camera ? _camera : GetComponent<Camera>();
            _vCam = _vCam ? _vCam : GetComponent<CinemachineVirtualCamera>();
        }

        private void Awake()
        {
            _baseFov = _vCam ? _vCam.m_Lens.FieldOfView : _camera.fieldOfView;
            _baseRatio = _baseAspectRatio.x / _baseAspectRatio.y;
        }

        private void FixedUpdate()
        {
            float ration = Mathf.Min(_camera ? _camera.aspect : _vCam.m_Lens.Aspect, _baseRatio);
            float fov = _baseRatio / ration * _baseFov;

            if (_lastFov == fov)
                return;

            var maxFov = Math.Min(fov, 80);
            
            if (_camera)
                _camera.fieldOfView = maxFov;

            if (_vCam)
                _vCam.m_Lens.FieldOfView = maxFov;

            _lastFov = fov;
        }
    }
}