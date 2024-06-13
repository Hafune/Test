using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct MagnetAreaComponent : IAreaComponent
    {
        [field: SerializeField] public AbstractArea area { get; set; }
    }
}