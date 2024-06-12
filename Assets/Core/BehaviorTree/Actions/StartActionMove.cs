using BehaviorDesigner.Runtime.Tasks;
using Core.Components;
using Core.Generated;
using Core.Systems;
using Leopotam.EcsLite;

namespace Core.BehaviorTree
{
    [TaskCategory("Actions")]
    public class StartActionMove : AbstractEntityAction
    {
        private bool _failed;
        private bool _awaitingResult;
        private EcsPool<MoveDirectionComponent> _moveDirectionPool;
        private EcsPool<EventActionStart<ActionMoveComponent>> _startMovePool;

        public override void OnAwake()
        {
            base.OnAwake();
            _moveDirectionPool = World.GetPool<MoveDirectionComponent>();
            _startMovePool = World.GetPool<EventActionStart<ActionMoveComponent>>();
            IsInstant = false;
        }

        public override void OnStart()
        {
            _failed = false;
            _awaitingResult = true;
            _moveDirectionPool.Get(RawEntity).direction = transform.right;
            _startMovePool.Add(RawEntity);
            SubscribeOnActionStates(ActionEnum.ActionMoveComponent);
        }

        public override TaskStatus OnUpdate()
        {
            if (_failed)
                return TaskStatus.Failure;

            return _awaitingResult ? TaskStatus.Running : TaskStatus.Success;
        }

        public override void OnEnd() => DescribeOnActionState();
        protected override void OnActionStart() => _awaitingResult = false;
        protected override void OnActionStartFailed() => _failed = true;
    }
}