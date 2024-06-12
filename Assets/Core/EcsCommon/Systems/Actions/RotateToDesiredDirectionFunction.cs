using Core.Components;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Systems
{
    public class RotateToDesiredDirectionFunction
    {
        private readonly EcsPool<MoveDirectionComponent> _inputDirectionPool;
        private readonly EcsPool<TransformComponent> _transformPool;

        public RotateToDesiredDirectionFunction(Context context)
        {
            var world = context.Resolve<EcsWorld>();

            _inputDirectionPool = world.GetPool<MoveDirectionComponent>();
            _transformPool = world.GetPool<TransformComponent>();
        }

        public void UpdateEntity(int entity)
        {
            var inputDirection = _inputDirectionPool.Get(entity).direction;
            var transform = _transformPool.Get(entity).transform;

            UpdateEntity(inputDirection, transform);
        }

        public void UpdateEntity(Vector2 inputDirection, Transform transform)
        {
            if (inputDirection.x == 0)
                return;

            var sign = inputDirection.x.Sign();
            var angle = sign < 0 ? 180 : 0;

            if (transform.right.x.Sign() != sign)
                transform.eulerAngles = new Vector3(0, angle, 0);
        }
    }
}