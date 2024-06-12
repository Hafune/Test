using BehaviorDesigner.Runtime.Tasks;
using Core.Components;
using Leopotam.EcsLite;

namespace Core.BehaviorTree
{
    [TaskCategory("Actions")]
    public class IsDead : AbstractEntityCondition
    {
        private EcsPool<EventDeath> _eventDiedPool;

        public override void OnAwake()
        {
            base.OnAwake();
            _eventDiedPool = GetPool<EventDeath>();
        }

        public override TaskStatus OnUpdate() => _eventDiedPool.Has(RawEntity) ? TaskStatus.Success : TaskStatus.Failure;
    }
}