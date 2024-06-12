using BehaviorDesigner.Runtime.Tasks;
using Core.Components;
using Leopotam.EcsLite;

namespace Core.BehaviorTree
{
    [TaskCategory("Actions")]
    public class TargetInAgroRange : AbstractEntityCondition
    {
        private EcsPool<TargetInAgroRangeTag> _pool;

        public override void OnAwake()
        {
            base.OnAwake();
            _pool = GetPool<TargetInAgroRangeTag>();
        }

        public override TaskStatus OnUpdate() => _pool.Has(RawEntity) ? TaskStatus.Success : TaskStatus.Failure;
    }
}