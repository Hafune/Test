using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct ImpactAreaComponent : IAreaComponent
    {
        [field: SerializeField] public AbstractArea area { get; set; }
    }
}