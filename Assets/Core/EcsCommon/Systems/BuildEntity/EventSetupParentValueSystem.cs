using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class EventSetupParentValueSystem<T> : IEcsRunSystem
        where T : struct, IValue
    {
        private readonly EcsFilterInject<Inc<EventSetupParentValue<T>, ParentComponent>> _filter;
        private readonly EcsFilterInject<Inc<EventSetupParentValue<T>>> _eventFilter;

        private readonly EcsPoolInject<EventSetupParentValue<T>> _eventPool;
        private readonly EcsPoolInject<EventValueUpdated<T>> _updatedPool;
        private readonly EcsPoolInject<ParentComponent> _parentPool;
        private readonly EcsPoolInject<T> _pool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                if (!_pool.Value.Has(_parentPool.Value.Get(i).entity))
                    return;

                _pool.Value.GetOrInitialize(i).value = _pool.Value.Get(_parentPool.Value.Get(i).entity).value;
                _updatedPool.Value.GetOrInitialize(i);
            }

            foreach (var i in _eventFilter.Value)
                _eventPool.Value.Del(i);
        }
    }
}