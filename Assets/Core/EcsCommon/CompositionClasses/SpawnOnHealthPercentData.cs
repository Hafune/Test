using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public class SpawnOnHealthPercentData
    {
        [SerializeField, Range(0f, 1f)] public float percent;
        [SerializeField] public List<Transform> prefabs;
    }
}