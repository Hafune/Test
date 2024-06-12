using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Unity.VisualScripting;

namespace Core.Systems
{
    public class SlotSystem<T> : IEcsRunSystem
        where T : struct, ISlotData
    {
        private readonly EcsWorld _world;

        private readonly EcsFilterInject<
            Inc<T>,
            Exc<InProgressTag<T>>> _setupFilter;

        private readonly EcsFilterInject<
            Inc<
                InProgressTag<T>,
                SlotTagPoolsComponent<T>,
                SlotValuePoolsComponent<T>>,
            Exc<T>> _removeFilter;

        private readonly EcsFilterInject<
            Inc<
                T,
                InProgressTag<T>,
                SlotTagPoolsComponent<T>,
                SlotValuePoolsComponent<T>,
                EventRefreshSlot<T>>> _reloadSlotFilter;

        private readonly EcsFilterInject<
            Inc<
                EventRefreshSlot<T>
            >> _eventRefreshFilter;

        private readonly EcsPoolInject<EventRefreshSlot<T>> _eventReloadSlotPool;
        private readonly EcsPoolInject<InProgressTag<T>> _progressPool;
        private readonly EcsPoolInject<SlotTagPoolsComponent<T>> _slotTagPoolsPool;
        private readonly EcsPoolInject<SlotValuePoolsComponent<T>> _slotValuePoolsPool;
        private readonly EcsPoolInject<T> _slotPool;
        private readonly ComponentPools _pools;

        public SlotSystem(EcsWorld world) => _world = world;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _reloadSlotFilter.Value)
            {
                var slotPools = _slotValuePoolsPool.Value.Get(entity);
                slotPools.DelAndStartRecalculate(entity);
                slotPools.Clear();

                var tagPools = _slotTagPoolsPool.Value.Get(entity);
                foreach (var pool in tagPools.pools)
                    pool.Del(entity);

                tagPools.pools.Clear();

                SetupSlotValues(entity);
            }

            foreach (var i in _eventRefreshFilter.Value)
                _eventReloadSlotPool.Value.Del(i);

            foreach (var i in _setupFilter.Value)
                SetupSlot(i);

            foreach (var i in _removeFilter.Value)
                RemoveSlot(i);
        }

        private void SetupSlot(int entity)
        {
            _progressPool.Value.Add(entity);
            _slotValuePoolsPool.Value.Add(entity);
            _slotTagPoolsPool.Value.Add(entity);
            SetupSlotValues(entity);
        }

        private void SetupSlotValues(int entity)
        {
            var slot = _slotPool.Value.Get(entity);
            var values = slot.values;
            var slotPools = _slotValuePoolsPool.Value.Get(entity);

            foreach (var data in values)
            {
                var slotValueComponentType = SlotValueTypes.SlotValueType<T>(data.valueEnum);
                var valuePool = _world.GetPoolByType(slotValueComponentType);
                var slotValue = (ISlotValue)slotValueComponentType.Default();
                slotValue.value = data.value;
                valuePool.AddRaw(entity, slotValue);

                var eventPool = ValuePoolsUtility.GetStartRecalculatePool(_pools, data.valueEnum);
                eventPool.AddIfNotExist(entity);

                slotPools.AddPools(valuePool, eventPool);
            }

            var tags = slot.tags;
            var tagPools = _slotTagPoolsPool.Value.Get(entity);

            foreach (var data in tags)
            {
                var slotTagType = SlotValueTypes.SlotTagType(data);
                var pool = _world.GetPoolByType(slotTagType);

                if (!pool.Has(entity))
                    pool.AddDefault(entity);

                tagPools.pools.Add(pool);
            }
        }

        private void RemoveSlot(int entity)
        {
            _progressPool.Value.Del(entity);
            _slotValuePoolsPool.Value.Get(entity).DelAndStartRecalculate(entity);
            _slotValuePoolsPool.Value.Del(entity);

            foreach (var pool in _slotTagPoolsPool.Value.Get(entity).pools)
                pool.Del(entity);

            _slotTagPoolsPool.Value.Del(entity);
        }
    }
}