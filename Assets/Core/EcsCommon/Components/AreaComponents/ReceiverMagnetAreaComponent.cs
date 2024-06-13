using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct ReceiverMagnetAreaComponent : IAreaComponent
    {
        [field: SerializeField] public AbstractArea area { get; set; }
    }
}