using Core.Components;
using Leopotam.EcsLite;
using Lib;
using Reflex;

namespace Core.Systems
{
    public class NestedSystemFunction<T> where T : struct
    {
        private readonly EcsFilter _filter;

        private readonly EcsPool<NestedEntitiesComponent> _nestedEntityPool;
        private readonly EcsPool<T> _pool;

        public NestedSystemFunction(Context context)
        {
            var world = context.Resolve<EcsWorld>();
            _filter = world.Filter<NestedEntitiesComponent>().End();
            _nestedEntityPool = world.GetPool<NestedEntitiesComponent>();
            _pool = world.GetPool<T>();
        }

        public void SendEvent(int e, bool skip = true)
        {
            if (!skip)
                _pool.AddIfNotExist(e);

            if (!_filter.HasEntity(e))
                return;

            _nestedEntityPool.Get(e).ForEachEntities(SendEventSkipFalse);
        }
        
        public void Add(int e) => SaveRecursive(e);

        public void Remove(int e) => ClearRecursive(e);
        
        private void SendEventSkipFalse(int entity) => SendEvent(entity, false);

        private void SaveRecursive(int e)
        {
            if (!_filter.HasEntity(e))
                return;

            var nested = _nestedEntityPool.Get(e);
            nested.AddComponent(default, _pool);
            nested.ForEachEntities(SaveRecursive);
        }

        private void ClearRecursive(int e)
        {
            if (!_filter.HasEntity(e))
                return;

            var nested = _nestedEntityPool.Get(e);
            nested.RemoveComponent(_pool);
            nested.ForEachEntities(ClearRecursive);
        }
    }
}