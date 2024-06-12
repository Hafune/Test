using Core.Components;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Systems
{
    public class MoveFunction
    {
        private readonly EcsPool<MoveDirectionComponent> _inputDirectionPool;
        private readonly EcsPool<MoveSpeedValueComponent> _movementSpeedValuePool;
        private readonly EcsPool<RigidbodyComponent> _rigidbodyPool;
        
        private readonly RotateToDesiredDirectionFunction _rotateToDesiredDirectionFunction;

        public MoveFunction(Context context)
        {
            var world = context.Resolve<EcsWorld>();

            _inputDirectionPool = world.GetPool<MoveDirectionComponent>();
            _movementSpeedValuePool = world.GetPool<MoveSpeedValueComponent>();
            _rigidbodyPool = world.GetPool<RigidbodyComponent>();
            _rotateToDesiredDirectionFunction = new(context);
        }

        public void UpdateEntity(int entity)
        {
            var inputDirection = _inputDirectionPool.Get(entity).direction;

            var direction = new Vector2(inputDirection.x.Sign(), 0);
            var rigidbody = _rigidbodyPool.Get(entity).rigidbody;
            float speed = _movementSpeedValuePool.Get(entity).value;
            
            var desiredVelocity = direction * speed + (direction.x == 0
                ? new Vector2(0, rigidbody.velocity.y)
                : rigidbody.velocity);
            desiredVelocity.x = Mathf.Clamp(desiredVelocity.x, -speed, speed);
            rigidbody.velocity = desiredVelocity;

            _rotateToDesiredDirectionFunction.UpdateEntity(inputDirection, rigidbody.transform);
        }
    }
}