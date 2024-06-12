using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class RestoreByCheckpointSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                EventRestoreConsumables,
                HitPointValueComponent,
                HitPointMaxValueComponent,
                HealingPotionValueComponent,
                HealingPotionMaxValueComponent
            >> _filter;

        private readonly EcsFilterInject<
            Inc<
                EventRestoreConsumables
            >> _eventFilter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                _pools.HitPointValue.Get(i).value = _pools.HitPointMaxValue.Get(i).value;
                _pools.EventUpdatedHitPointValue.AddIfNotExist(i);

                _pools.HealingPotionValue.Get(i).value = _pools.HealingPotionMaxValue.Get(i).value;
                _pools.EventUpdatedHealingPotionValue.AddIfNotExist(i);
            }

            foreach (var i in _eventFilter.Value)
                _pools.EventRestoreConsumables.Del(i);
        }
    }
}