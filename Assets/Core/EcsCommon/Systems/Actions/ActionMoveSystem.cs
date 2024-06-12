using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using UnityEngine;

namespace Core.Systems
{
    public class ActionMoveSystem : AbstractActionSystem<ActionMoveComponent>, IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ActionMoveComponent,
                EventActionStart<ActionMoveComponent>,
                ActionCurrentComponent,
                MoveSpeedValueComponent,
                MoveDirectionComponent,
                RigidbodyComponent
            >,
            Exc<InProgressTag<ActionMoveComponent>>> _activateFilter;

        private readonly EcsFilterInject<
            Inc<
                InProgressTag<ActionMoveComponent>
            >> _progressFilter;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _activateFilter.Value)
            {
                if (!_actionPool.Value.Get(i).logic.CheckConditionLogic(i))
                    continue;

                BeginActionProgress(i);
                ref var current = ref _actionCurrentPool.Value.Get(i);
                current.canBeCanceled = true;
            }

            foreach (var i in _progressFilter.Value)
                UpdateEntity(i);

            CleanEventStart();
        }

        private void UpdateEntity(int entity)
        {
            var inputDirection = _pools.MoveDirection.Get(entity).direction;
            var rigidbody = _pools.Rigidbody.Get(entity).rigidbody;
            float speed = _pools.MoveSpeedValue.Get(entity).value;

            var desiredVelocity = inputDirection * speed;
            rigidbody.velocity = desiredVelocity.ToVector3XZ();
        }

        public override void Cancel(int entity)
        {
            base.Cancel(entity);

            _pools.MoveDirection.Get(entity).direction = Vector2.zero;
            var rigidbody = _pools.Rigidbody.Get(entity).rigidbody;
            rigidbody.velocity = Vector2.zero;
        }
    }
}