using Cinemachine;
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Reflex;

namespace Core.Systems
{
    public class EventVirtualCameraFollowSetupSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TransformComponent, EventVirtualCameraFollowSetup>> _filter;

        private readonly EcsPoolInject<TransformComponent> _transformComponentPool;
        private readonly EcsPoolInject<EventVirtualCameraFollowSetup> _eventVirtualCameraFollowSetupPool;

        private CinemachineVirtualCamera _virtualCamera;

        public EventVirtualCameraFollowSetupSystem(Context context)
        {
            _virtualCamera = context.Resolve<CinemachineVirtualCamera>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var transform = _transformComponentPool.Value.Get(i).transform;
                _virtualCamera.PreviousStateIsValid = false;
                _virtualCamera.Follow = transform;
                _eventVirtualCameraFollowSetupPool.Value.Del(i);
            }
        }
    }
}