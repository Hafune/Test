using System;
using BehaviorDesigner.Runtime;
using Core.Systems;

namespace Core.BehaviorTree.Shared
{
    [Serializable]
    public class SharedAbstractActionEntityLogic : SharedVariable<AbstractActionEntityLogic>
    {
        public static implicit operator SharedAbstractActionEntityLogic(AbstractActionEntityLogic value) => new() { Value = value };
    }
}