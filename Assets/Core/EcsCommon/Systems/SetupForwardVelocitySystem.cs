using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class SetupForwardVelocitySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<EventSetupForwardVelocity, RigidbodyComponent>> _filter;
        private readonly EcsFilterInject<Inc<EventSetupForwardVelocity>> _eventFilter;

        private readonly EcsPoolInject<MoveSpeedValueComponent> _speedValuePool;
        private readonly EcsPoolInject<EventSetupForwardVelocity> _eventPool;
        private readonly EcsPoolInject<RigidbodyComponent> _rigidbodyPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var rigidbody = _rigidbodyPool.Value.Get(i).rigidbody;
                var speedValue = _speedValuePool.Value.Get(i);

                rigidbody.velocity = rigidbody.transform.up * speedValue.value;
            }

            foreach (var i in _eventFilter.Value)
                _eventPool.Value.Del(i);
        }
    }
}