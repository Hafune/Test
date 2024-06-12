using Core.Components;
using Core.Generated;
using Core.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Reflex;

namespace Core.Systems
{
    public class BuildEntitySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<EventBuildEntity>> _filter;

        private readonly EcsPoolInject<EventBuildEntity> _eventBuildPool;
        private readonly EcsPoolInject<ParentComponent> _ownerPool;
        private readonly UnityComponentMappedPool _mappedPool;
        private readonly ComponentPools _pools;

        public BuildEntitySystem(Context context)
        {
            _pools = context.Resolve<ComponentPools>();
            _mappedPool = context.Resolve<PoolService>().ScenePool.BuildMappedPull();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                UpdateEntity(i);
        }

        private void UpdateEntity(int entity)
        {
            var list = _eventBuildPool.Value.Get(entity).list;
            var children = _pools.Node.GetOrInitialize(entity).children;

            for (int i = 0, iMax = list.Count; i < iMax; i++)
            {
                var data = list.Items[i];
                int child = data.BuildEntity(_mappedPool.GetComponentByPrefab);
                ref var parent = ref _pools.Parent.Add(child);
                parent.entity = entity;
                parent.OnRemove = data.OnRemove;
                children.Add(child);
                data.OnBuild?.Invoke(entity, child);
            }
            
            _eventBuildPool.Value.Del(entity);
        }
    }
}