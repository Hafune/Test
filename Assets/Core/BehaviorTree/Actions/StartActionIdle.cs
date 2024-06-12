using BehaviorDesigner.Runtime.Tasks;
using Core.Components;
using Core.Generated;
using Core.Systems;
using Leopotam.EcsLite;

namespace Core.BehaviorTree
{
    [TaskCategory("Actions")]
    public class StartActionIdle : AbstractEntityAction
    {
        private bool _awaitingResult;
        private EcsPool<EventActionStart<ActionIdleComponent>> _startPool;
        private bool _failed;

        public override void OnAwake()
        {
            base.OnAwake();
            _startPool = World.GetPool<EventActionStart<ActionIdleComponent>>();
            IsInstant = false;
        }

        public override void OnStart()
        {
            _awaitingResult = true;
            _startPool.Add(RawEntity);
            _failed = false;
            SubscribeOnActionStates(ActionEnum.ActionIdleComponent);
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