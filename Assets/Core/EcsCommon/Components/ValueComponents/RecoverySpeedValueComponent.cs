using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct RecoverySpeedValueComponent<T> : IValue where T : struct, IValue
    {
        [field: SerializeField] public float value { get; set; }
    }
}