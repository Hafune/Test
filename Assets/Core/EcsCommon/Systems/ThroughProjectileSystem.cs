using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class ThroughProjectileSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                NodeComponent,
                ThroughProjectileSlotTag
            >,
            Exc<
                InProgressTag<ThroughProjectileSlotTag>
            >> _filter;

        private readonly EcsFilterInject<
            Inc<
                NodeComponent,
                InProgressTag<ThroughProjectileSlotTag>
            >,
            Exc<
                ThroughProjectileSlotTag
            >> _cancelFilter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                _pools.InProgressThroughProjectileSlot.Add(i);
                foreach (var child in _pools.Node.Get(i).children)
                    _pools.ThroughProjectileSlot.AddIfNotExist(child);
            }

            foreach (var i in _cancelFilter.Value)
            {
                _pools.InProgressThroughProjectileSlot.Del(i);
                foreach (var child in _pools.Node.Get(i).children)
                    _pools.ThroughProjectileSlot.DelIfExist(child);
            }
        }
    }
}