using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using UnityEngine;

namespace Core.Systems
{
    public class RecoveryActionSystem<V> : IEcsRunSystem
        where V : struct, IActionComponent //текущее значение
    {
        private readonly EcsFilterInject<
            Inc<
                RecoveryActionValueComponent<V>,
                EventValueUpdated<RecoveryActionValueComponent<V>>,
                RecoverySpeedValueComponent<RecoveryActionValueComponent<V>>
            >,
            Exc<
                InProgressTag<RecoveryActionValueComponent<V>>
            >> _activateFilter;

        private readonly EcsFilterInject<
            Inc<EventValueUpdated<RecoveryActionValueComponent<V>>
            >> _testFilter;

        private readonly EcsFilterInject<
            Inc<
                V,
                RecoverySpeedValueComponent<RecoveryActionValueComponent<V>>,
                InProgressTag<RecoveryActionValueComponent<V>>
            >> _progressFilter;

        private readonly EcsPoolInject<RecoveryActionValueComponent<V>> _valuePool;
        private readonly EcsPoolInject<RecoverySpeedValueComponent<RecoveryActionValueComponent<V>>> _regenerationValuePool;
        private readonly EcsPoolInject<InProgressTag<RecoveryActionValueComponent<V>>> _progressPool;
        private readonly EcsPoolInject<EventValueUpdated<RecoveryActionValueComponent<V>>> _eventValueUpdatedPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _activateFilter.Value)
            {
                var value = _valuePool.Value.Get(i);

                if (value.value >= 1)
                    return;

                _progressPool.Value.Add(i);
            }

            foreach (var i in _progressFilter.Value)
            {
                ref var value = ref _valuePool.Value.Get(i);
                value.value += _regenerationValuePool.Value.Get(i).value * Time.deltaTime;
                _eventValueUpdatedPool.Value.AddIfNotExist(i);

                if (value.value < 1)
                    return;

                value.value = 1;
                _progressPool.Value.Del(i);
            }
        }
    }
}