using System;
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using UnityEngine;

namespace Core.Systems
{
    public class RecoveryValueSystem<V, M> : IEcsRunSystem
        where V : struct, IValue //текущее значение
        where M : struct, IValue //максимальное
    {
        private readonly EcsFilterInject<
            Inc<
                V,
                M,
                EventValueUpdated<V>
            >> _clampFilter;

        private readonly EcsFilterInject<
            Inc<
                V,
                M,
                EventValueUpdated<M>
            >> _clampByMaxFilter;

        private readonly EcsFilterInject<
            Inc<
                V,
                M,
                EventValueUpdated<V>,
                RecoverySpeedValueComponent<V>
            >,
            Exc<
                InProgressTag<RecoveryValueSystem<V, M>>
            >> _activateFilter;

        private readonly EcsFilterInject<
            Inc<
                V,
                M,
                EventValueUpdated<M>,
                RecoverySpeedValueComponent<V>
            >,
            Exc<
                InProgressTag<RecoveryValueSystem<V, M>>
            >> _activateByMaxFilter;

        private readonly EcsFilterInject<
            Inc<
                V,
                M,
                RecoverySpeedValueComponent<V>,
                InProgressTag<RecoveryValueSystem<V, M>>
            >> _progressFilter;

        private readonly EcsPoolInject<V> _valuePool;
        private readonly EcsPoolInject<M> _maxValuePool;
        private readonly EcsPoolInject<RecoverySpeedValueComponent<V>> _regenerationValuePool;
        private readonly EcsPoolInject<InProgressTag<RecoveryValueSystem<V, M>>> _progressPool;
        private readonly EcsPoolInject<EventValueUpdated<V>> _eventValueUpdatedPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _clampFilter.Value)
                _valuePool.Value.Get(i).value =
                    Math.Clamp(_valuePool.Value.Get(i).value, 0, _maxValuePool.Value.Get(i).value);

            foreach (var i in _clampByMaxFilter.Value)
                _valuePool.Value.Get(i).value =
                    Math.Clamp(_valuePool.Value.Get(i).value, 0, _maxValuePool.Value.Get(i).value);

            foreach (var i in _activateFilter.Value)
                Activate(i);

            foreach (var i in _activateByMaxFilter.Value)
                Activate(i);

            foreach (var i in _progressFilter.Value)
                UpdateEntity(i);
        }

        private void Activate(int entity)
        {
            var value = _valuePool.Value.Get(entity);
            var maxValue = _maxValuePool.Value.Get(entity).value;

            if (value.value >= maxValue)
                return;

            _progressPool.Value.Add(entity);
        }

        private void UpdateEntity(int entity)
        {
            var step = _regenerationValuePool.Value.Get(entity).value;
            ref var value = ref _valuePool.Value.Get(entity);
            value.value += step * Time.deltaTime;

            if (step != 0)
                _eventValueUpdatedPool.Value.AddIfNotExist(entity);

            var maxValue = _maxValuePool.Value.Get(entity).value;

            if (value.value < maxValue)
                return;

            value.value = maxValue;
            _progressPool.Value.Del(entity);
        }
    }
}