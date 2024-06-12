using BehaviorDesigner.Runtime.Tasks;

namespace Core.BehaviorTree
{
    [TaskCategory("Actions")]
    public class TargetInAttackRange : AbstractEntityCondition
    {
        // private EcsPool<TargetInAreaTag<NpcAction1Component>> _pool;
        //
        // public override void OnAwake()
        // {
        //     base.OnAwake();
        //     _pool = GetPool<TargetInAreaTag<NpcAction1Component>>();
        // }
        //
        // public override TaskStatus OnUpdate() => _pool.Has(RawEntity) ? TaskStatus.Success : TaskStatus.Failure;
    }
}