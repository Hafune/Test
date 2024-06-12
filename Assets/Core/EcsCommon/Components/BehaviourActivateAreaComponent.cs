using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct BehaviourActivateAreaComponent
    {
        public CircleCollider2D collider;
        public float baseRadius;
    }
}