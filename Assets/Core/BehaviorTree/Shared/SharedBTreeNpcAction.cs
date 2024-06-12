using System;
using BehaviorDesigner.Runtime;
using Core.BehaviorTree.Scripts;

namespace Core.BehaviorTree.Shared
{
    [Serializable]
    public class SharedBTreeNpcAction : SharedVariable<BTreeNpcAction>
    {
        public static implicit operator SharedBTreeNpcAction(BTreeNpcAction value) => new() { Value = value };
    }
}