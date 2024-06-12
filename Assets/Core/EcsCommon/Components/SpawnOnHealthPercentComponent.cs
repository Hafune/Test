using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct SpawnOnHealthPercentComponent
    {
        [SerializeField] public SpawnOnHealthPercentData[] spawnDatas;
        [HideInInspector] public float lastPercent;
    }
}