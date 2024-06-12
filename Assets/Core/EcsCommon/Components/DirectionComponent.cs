using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct DirectionComponent
    {
        public Transform transform;
        [NonSerialized] public Vector3 direction;
    }
}