using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class RemoveOnDealDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                RemoveOnDealDamageTag,
                EventCausedDamage
            >,
            Exc<
                ThroughProjectileSlotTag
            >> _filter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                _pools.EventRemoveEntity.AddIfNotExist(i);
        }
    }
}