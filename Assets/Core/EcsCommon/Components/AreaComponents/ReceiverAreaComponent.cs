using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct ReceiverAreaComponent : IAreaComponent
    {
        [field: SerializeField] public AbstractArea area { get; set; }
    }
}