using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct DamageAreaComponent : IAreaComponent
    {
        [field: SerializeField] public AbstractArea area { get; set; }
    }
}