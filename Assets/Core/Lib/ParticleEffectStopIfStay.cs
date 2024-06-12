using UnityEngine;

namespace Core.Lib
{
    public class ParticleEffectStopIfStay : MonoBehaviour
    {
        [SerializeField] private float _maxStayTime = .5f;
        [SerializeField] private ParticleSystem _particleSystem;
        private ParticleSystem.MainModule _main;
        private float _defaultDuration;
        private float _stayTime;
        private Vector3 _lastPosition;
        private bool _wasStopped;

        private void OnValidate() => _particleSystem = GetComponent<ParticleSystem>();

        private void OnEnable() => _wasStopped = false;

        private void FixedUpdate()
        {
            if (_wasStopped)
                return;

            if (_lastPosition != transform.position)
            {
                _lastPosition = transform.position;
                _stayTime = Time.time;
                return;
            }

            if (Time.time - _stayTime <= _maxStayTime)
                return;

            _wasStopped = true;
            _particleSystem.Stop();
        }
    }
}