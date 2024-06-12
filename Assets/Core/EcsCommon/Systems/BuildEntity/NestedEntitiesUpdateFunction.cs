using Core.Components;
using Leopotam.EcsLite;
using Reflex;

namespace Core.Systems
{
    public class NestedEntitiesUpdateFunction
    {
        private EcsWorld _world;
        private EcsPool<NestedEntitiesComponent> _nestedEntitiesPool;

        public NestedEntitiesUpdateFunction(Context context)
        {
            _world = context.Resolve<EcsWorld>();
            _nestedEntitiesPool = _world.GetPool<NestedEntitiesComponent>();
        }

        public void AddNested(NestedEntitiesComponent nestedComponent, int entity)
        {
            nestedComponent.AddNestedEntity(_world.PackEntityWithWorld(entity));
            nestedComponent.SetupComponents(entity);

            if (!_nestedEntitiesPool.Has(entity))
                return;

            var selfNestedComponent = _nestedEntitiesPool.Get(entity);
            selfNestedComponent.CopySavedComponentsFrom(nestedComponent);
            selfNestedComponent.ForEachEntities(e => AddNested(selfNestedComponent, e));
        }
    }
}