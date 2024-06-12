using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class DeathOnDealDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                DeathOnDealDamageTag,
                EventCausedDamage
            >,
            Exc<
                ThroughProjectileSlotTag
            >> _filter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                _pools.EventStartActionDeath.AddIfNotExist(i);
        }
    }
    
    // public class DeathOnDealDamageSystem : IEcsRunSystem
    // {
    //     private readonly EcsFilterInject<
    //         Inc<
    //             DeathOnDealDamageTag,
    //             EventCausedDamage
    //         >,
    //         Exc<
    //             ThroughProjectileSlotTag
    //         >> _filter;
    //
    //     private readonly EcsFilterInject<
    //         Inc<
    //             DeathOnDealDamageTag,
    //             EventCausedDamage,
    //             ThroughProjectileSlotTag,
    //             ThroughProjectileValueComponent
    //         >,
    //         Exc<
    //             InProgressTag<ThroughProjectileSlotTag>
    //         >> _throughFilter;
    //
    //     private readonly EcsFilterInject<
    //         Inc<
    //             InProgressTag<ThroughProjectileSlotTag>,
    //             EventCausedDamage,
    //             ThroughProjectileValueComponent
    //         >> _throughProgressFilter;
    //
    //     private readonly ComponentPools _pools;
    //
    //     public void Run(IEcsSystems systems)
    //     {
    //         foreach (var i in _filter.Value)
    //             _pools.EventStartActionDeath.AddIfNotExist(i);
    //
    //         foreach (var i in _throughFilter.Value)
    //             _pools.InProgressThroughProjectileSlot.Add(i);
    //
    //         foreach (var i in _throughProgressFilter.Value)
    //         {
    //             if (_pools.ThroughProjectileValue.Get(i).value-- > -1)
    //                 continue;
    //
    //             _pools.EventStartActionDeath.AddIfNotExist(i);
    //         }
    //     }
    // }
}