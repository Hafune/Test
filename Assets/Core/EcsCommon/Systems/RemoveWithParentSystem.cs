using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class RemoveWithParentSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ParentComponent
            >,
            Exc<InProgressTag<ParentComponent>>> _filter;
        
        private readonly EcsFilterInject<
            Inc<
                InProgressTag<ParentComponent>
            >,
            Exc<ParentComponent>> _progressFilter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                _pools.InProgressParent.Add(i);
            
            foreach (var i in _progressFilter.Value)
                _pools.EventRemoveEntity.AddIfNotExist(i);
        }
    }
}