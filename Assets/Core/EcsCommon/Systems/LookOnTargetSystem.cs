using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class LookOnTargetSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<
            TransformComponent,
            DirectionComponent,
            TargetComponent,
            LookOnTargetComponent
        >> _filter;

        private readonly EcsFilterInject<Inc<TransformComponent>, Exc<
            InProgressTag<ActionDeathComponent>
        >> _targetFilter;

        private readonly EcsPoolInject<DirectionComponent> _directionPool;
        private readonly EcsPoolInject<TargetComponent> _targetPool;
        private readonly EcsPoolInject<TransformComponent> _transformPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                UpdateEntity(i);
        }

        private void UpdateEntity(int entity)
        {
            if (!_targetPool.Value.Get(entity).ecsPackedEntity.Unpack(out _, out var target) ||
                !_targetFilter.Value.HasEntity(target))
                return;

            var position = _directionPool.Value.Get(entity).transform.position;
            var targetPosition = _transformPool.Value.Get(target).transform.position;
            var direction = (targetPosition - position).normalized;

            _directionPool.Value.Get(entity).direction = direction;
        }
    }
}