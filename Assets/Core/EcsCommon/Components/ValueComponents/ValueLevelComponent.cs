using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct ValueLevelComponent<T> : IValueLevel where T : struct, IValue
    {
        [field: SerializeField] public float valuePerLevel { get; set; }

        [field: SerializeField] public float level { get; set; }
    }
}