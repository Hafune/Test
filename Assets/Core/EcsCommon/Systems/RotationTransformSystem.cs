using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using UnityEngine;

namespace Core.Systems
{
    public class RotationTransformSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveDirectionComponent, TransformComponent, AngularSpeedValueComponent>> _filter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                UpdateEntity(i);
        }

        private void UpdateEntity(int entity)
        {
            var direction = _pools.MoveDirection.Get(entity).direction;
            
            if (direction == Vector2.zero)
                return;
            
            var transform = _pools.Transform.Get(entity).transform;
            var angularSpeed = _pools.AngularSpeedValue.Get(entity).value;

            transform.rotation = transform.rotation.RotateTowards(direction.ToVector3XZ(),
                angularSpeed * Time.deltaTime);
        }
    }

    public class RotationTransform2DSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DirectionComponent, TransformComponent, AngularSpeedValueComponent>> _filter;

        private readonly EcsPoolInject<AngularSpeedValueComponent> _angularSpeedValuePool;
        private readonly EcsPoolInject<DirectionComponent> _directionPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var directionComponent = _directionPool.Value.Get(i);
                var direction = directionComponent.direction;
                var transform = directionComponent.transform;

                var angularSpeed = _angularSpeedValuePool.Value.Get(i).value;
                
                transform.rotation = MyQuaternionExtensions.RotateTowards2D(transform.up,direction,
                    angularSpeed * Time.deltaTime);
            }
        }
    }
}