using Cinemachine;
using UnityEngine;
using static UnityEngine.Screen;

namespace Core.Lib
{
    public class OrthographicConstantSize : MonoBehaviour
    {
        [SerializeField] private Vector2 _targetRatio = new(16, 9);
        [SerializeField] private CinemachineVirtualCamera _vCam;
        [SerializeField] private PolygonCollider2D _collider;

        private float _baseRatio;
        private float _lastCalc;
        private float _startSize;
        private float _pixelSize;

        private void OnValidate()
        {
            _vCam = _vCam ? _vCam : GetComponent<CinemachineVirtualCamera>();
        }

        private void Awake()
        {
            _startSize = _vCam.m_Lens.OrthographicSize;
            _baseRatio = _targetRatio.x / _targetRatio.y;
        }

        private void Update()
        {
            float w = width;
            float h = height;

            float ratio = w / h;

            if (_lastCalc == ratio)
                return;

            _lastCalc = ratio;
            
            if (ratio < _baseRatio)
            {
                _vCam.m_Lens.OrthographicSize = _startSize * (_baseRatio / ratio);
                _collider.transform.parent.localScale = Vector3.one;
            }
            else
            {
                _vCam.m_Lens.OrthographicSize = _startSize;
                var scale = Vector3.one;
                scale.x = ratio / _baseRatio;
                _collider.transform.parent.localScale = scale;
            }
        }
    }
}