using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct CollectableAreaComponent : IAreaComponent
    {
        [field: SerializeField] public AbstractArea area { get; set; }
    }
}