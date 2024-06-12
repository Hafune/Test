using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class EventSetupParentComponentSystem<T> : IEcsRunSystem
        where T : struct
    {
        private readonly EcsFilterInject<Inc<EventSetupParentComponent<T>, ParentComponent>> _filter;
        private readonly EcsFilterInject<Inc<EventSetupParentComponent<T>>> _eventFilter;

        private readonly EcsPoolInject<EventSetupParentComponent<T>> _eventPool;
        private readonly EcsPoolInject<ParentComponent> _parentPool;
        private readonly EcsPoolInject<T> _pool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                _pool.Value.Copy(_parentPool.Value.Get(i).entity, i);

            foreach (var i in _eventFilter.Value)
                _eventPool.Value.Del(i);
        }
    }
}