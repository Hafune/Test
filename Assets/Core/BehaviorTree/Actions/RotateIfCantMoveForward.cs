using BehaviorDesigner.Runtime.Tasks;
using Core.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.BehaviorTree
{
    [TaskCategory("Actions")]
    public class RotateIfCantMoveForward : AbstractEntityAction
    {
        [SerializeField] private float _duration;
        private float _currentDuration;
        private EcsPool<MoveDirectionComponent> _moveDirectionPool;
        private EcsPool<TouchWallTag> _touchWallPool;

        public override void OnAwake()
        {
            base.OnAwake();
            _moveDirectionPool = World.GetPool<MoveDirectionComponent>();
            _touchWallPool = World.GetPool<TouchWallTag>();
        }

        public override void OnStart() => _currentDuration = 0;

        public override TaskStatus OnUpdate()
        {
            _currentDuration += Time.deltaTime;
            var task = _currentDuration >= _duration ? TaskStatus.Success : TaskStatus.Running;

            if (!_touchWallPool.Has(RawEntity))
                return task;

            ref var moveComponent = ref _moveDirectionPool.Get(RawEntity);
            moveComponent.direction.x = -moveComponent.direction.x;

            return task;
        }
    }
}