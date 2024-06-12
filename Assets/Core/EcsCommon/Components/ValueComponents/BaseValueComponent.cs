using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct BaseValueComponent<T> : IBaseValue where T : struct, IValue
    {
        [field: SerializeField] public float baseValue { get; set; }
    }
}