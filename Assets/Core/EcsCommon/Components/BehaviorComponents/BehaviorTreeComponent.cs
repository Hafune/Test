using System;

namespace Core.Components
{
    [Serializable]
    public struct BehaviorTreeComponent
    {
        public BehaviorDesigner.Runtime.BehaviorTree tree;
    }
}