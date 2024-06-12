using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using UnityEngine;

namespace Core.Systems
{
    // public class RotationTransformSystem : IEcsRunSystem
    // {
    //     private readonly EcsFilterInject<Inc<DirectionComponent, TransformComponent, AngularSpeedValueComponent>> _filter;
    //
    //     private readonly EcsPoolInject<AngularSpeedValueComponent> _angularSpeedValuePool;
    //     private readonly EcsPoolInject<TransformComponent> _transformPool;
    //     private readonly EcsPoolInject<DirectionComponent> _directionPool;
    //
    //     public void Run(IEcsSystems systems)
    //     {
    //         foreach (var i in _filter.Value)
    //         {
    //             var direction = _directionPool.Value.Get(i).direction;
    //
    //             if (direction.x == 0 && direction.z == 0)
    //                 continue;
    //
    //             direction.y = 0;
    //             var transform = _transformPool.Value.Get(i).transform;
    //             var angularSpeed = _angularSpeedValuePool.Value.Get(i).value;
    //
    //             transform.rotation = transform.rotation.RotateTowards(direction,
    //                 angularSpeed * Time.deltaTime);
    //         }
    //     }
    // }

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