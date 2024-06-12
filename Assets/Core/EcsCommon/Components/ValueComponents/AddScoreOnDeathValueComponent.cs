using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct AddScoreOnDeathValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }
}