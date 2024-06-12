using Cinemachine;
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Reflex;
using UnityEngine;

namespace Core.Systems
{
    public class EventCameraShakeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<EventCameraShakeComponent>> _filter;

        private readonly EcsPoolInject<EventCameraShakeComponent> _pool;

        private readonly CinemachineBasicMultiChannelPerlin _cinemachineBasicMultiChannelPerlin;
        private bool _hasShake;
        private float _time;
        private float _maxTime;
        private float _maxIntensity;

        public EventCameraShakeSystem(Context context) => _cinemachineBasicMultiChannelPerlin =
            context.Resolve<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var shake = _pool.Value.Get(i);
                _maxIntensity = shake.maxIntensity;
                _maxTime = shake.maxTime;
                _time = _maxTime;
                _hasShake = true;
                _pool.Value.Del(i);
            }

            if (!_hasShake)
                return;

            _time -= Time.unscaledDeltaTime;
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(_maxIntensity, 0f, _time / _maxTime);

            if (_time > 0)
                return;

            _hasShake = false;
            _cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
        }
    }
}