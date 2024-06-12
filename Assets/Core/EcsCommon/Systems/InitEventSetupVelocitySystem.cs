using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class InitEventSetupVelocitySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<EventSetupVelocity, RigidbodyComponent, EventInit>> _filter;
        private readonly EcsFilterInject<Inc<EventSetupVelocity>> _eventFilter;

        private readonly EcsPoolInject<EventSetupVelocity> _eventPool;
        private readonly EcsPoolInject<RigidbodyComponent> _rigidbodyPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                _rigidbodyPool.Value.Get(i).rigidbody.velocity = _eventPool.Value.Get(i).velocity;

            foreach (var i in _eventFilter.Value)
                _eventPool.Value.Del(i);
        }
    }
}