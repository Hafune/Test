using System;
using BehaviorDesigner.Runtime;

namespace Core.BehaviorTree.Shared
{
    [Serializable]
    public class SharedTriggerCounter2D : SharedVariable<TriggerCounter2D>
    {
        public static implicit operator SharedTriggerCounter2D(TriggerCounter2D value) => new() { Value = value };
    }
}