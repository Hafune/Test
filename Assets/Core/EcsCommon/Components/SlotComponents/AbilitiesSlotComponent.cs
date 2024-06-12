using System;
using Core.Generated;
using Core.Systems;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct AbilitiesSlotComponent : ISlotData
    {
        [field: SerializeField] public ValueData[] values { get; set; }
        [field: SerializeField] public SlotTagEnum[] tags { get; set; }
    }
}