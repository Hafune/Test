using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Core.BehaviorTree
{
    [TaskCategory("Actions")]
    public class DisableTask : AbstractEntityAction
    {
        [SerializeField] private Task Task;

        public override void OnStart() => Task.Disabled = true;
    }
}