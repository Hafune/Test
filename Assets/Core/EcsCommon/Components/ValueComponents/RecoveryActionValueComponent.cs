using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct RecoveryActionValueComponent<T> : IValue where T : struct, IActionComponent
    {
        [field: SerializeField] public float value { get; set; }
    }
}