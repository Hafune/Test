using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Reflex;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Core.Systems
{
    //Пересмотреть порядок учёта сущностей как вложенных, скан может привести к получению чужой сущности 
    public class ScanHierarchyForNestedEntitiesSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TransformComponent, NestedEntitiesComponent, EventScanHierarchyForNestedEntities>>
            _filter;

        private readonly EcsFilterInject<Inc<EventScanHierarchyForNestedEntities>> _eventFilter;

        private readonly EcsPoolInject<EventScanHierarchyForNestedEntities> _eventPool;
        private readonly EcsPoolInject<NestedEntitiesComponent> _nestedEntitiesPool;
        private readonly EcsPoolInject<TransformComponent> _transformPool;

        private readonly NestedEntitiesUpdateFunction _updateFunction;

        public ScanHierarchyForNestedEntitiesSystem(Context context) =>
            _updateFunction = new NestedEntitiesUpdateFunction(context);

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var transform = _transformPool.Value.Get(i).transform;
                var nestedComponent = _nestedEntitiesPool.Value.Get(i);

                AddNested(transform, nestedComponent);
            }

            foreach (var i in _eventFilter.Value)
                _eventPool.Value.Del(i);
        }

        private void AddNested(
            Transform transform,
            NestedEntitiesComponent nestedComponent
        )
        {
            foreach (Transform child in transform)
            {
                if (!child.TryGetComponent(out ConvertToEntity c) || c.RawEntity == -1)
                {
                    AddNested(child, nestedComponent);
                    continue;
                }

                _updateFunction.AddNested(nestedComponent, c.RawEntity);
            }
        }
    }
}