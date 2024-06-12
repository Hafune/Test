using System;
using BehaviorDesigner.Runtime;
using Core.Lib;

namespace Core.BehaviorTree.Shared
{
    [Serializable]
    public class SharedSyncActiveStateArray : SharedVariable<SyncActiveState[]>
    {
        public static implicit operator SharedSyncActiveStateArray(SyncActiveState[] value) => new() { Value = value };
    }
}