using BehaviorDesigner.Runtime.Tasks;
using Core.BehaviorTree.Shared;
using UnityEngine;

namespace Core.BehaviorTree
{
    [TaskCategory("Actions")]
    public class TriggerIsActive2D : Conditional
    {
        [SerializeField] private SharedTriggerCounter2D _triggerCounter;

        public override TaskStatus OnUpdate() =>
            _triggerCounter.Value?.Count > 0 ? TaskStatus.Success : TaskStatus.Failure;
    }
}