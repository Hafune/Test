using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct DropOnDeathComponent
    {
        [field: SerializeField] public DropGroup[] data { get; private set; }
        [field: SerializeField] public Transform spawnPoint { get; private set; }
    }
}