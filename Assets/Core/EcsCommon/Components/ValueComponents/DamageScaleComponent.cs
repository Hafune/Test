using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct DamageScaleComponent
    {
        [field: SerializeField] public float value { get; set; }
    }
}