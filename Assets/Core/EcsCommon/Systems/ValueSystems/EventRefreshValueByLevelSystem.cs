using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class EventRefreshValueByLevelSystem<T> : IEcsRunSystem
        where T : struct, IValue
    {
        private readonly EcsFilterInject<Inc<T, EventValueUpdated<T>, ValueLevelComponent<T>>> _filter;

        private readonly EcsPoolInject<T> _valuePool;
        private readonly EcsPoolInject<ValueLevelComponent<T>> _valueLevelPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                UpdateEntity(i);
        }

        private void UpdateEntity(int entity)
        {
            ref var value = ref _valuePool.Value.Get(entity);
            var valueLevel = _valueLevelPool.Value.Get(entity);
            value.value += valueLevel.level * valueLevel.valuePerLevel;
        }
    }
}