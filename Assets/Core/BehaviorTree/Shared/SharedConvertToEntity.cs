using System;
using BehaviorDesigner.Runtime;
using Voody.UniLeo.Lite;

namespace Core.BehaviorTree.Shared
{
    [Serializable]
    public class SharedConvertToEntity : SharedVariable<ConvertToEntity>
    {
        public static implicit operator SharedConvertToEntity(ConvertToEntity value) => new() { Value = value };
    }
}