using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct FreezingComponent
    {
        public float duration;
        [NonSerialized] public Vector2 lastVelocity;
        [NonSerialized] public float originGravityScale;
        [NonSerialized] public float originAnimatorSpeed;
    }
}