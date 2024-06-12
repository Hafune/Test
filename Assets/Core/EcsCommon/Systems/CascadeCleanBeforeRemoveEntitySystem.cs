using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class CascadeCleanBeforeRemoveEntitySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                NodeComponent,
                EventRemoveEntity
            >> _nodeFilter;

        private readonly EcsFilterInject<
            Inc<
                ParentComponent,
                EventRemoveEntity
            >> _parentFilter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var nodeEntity in _nodeFilter.Value)
            {
                var children = _pools.Node.Get(nodeEntity).children;

                foreach (var childEntity in children)
                    _pools.Parent.Del(childEntity);

                children.Clear();
            }

            foreach (var i in _parentFilter.Value)
            {
                var parentRef = _pools.Parent.Get(i);
                parentRef.OnRemove?.Invoke(parentRef.entity, i);
                _pools.Node.Get(parentRef.entity).children.Remove(i);
            }
        }
    }
}