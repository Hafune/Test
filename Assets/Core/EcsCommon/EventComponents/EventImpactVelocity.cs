using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct EventImpactVelocity
    {
        [SerializeField] public bool explosion;
        [SerializeField] public float rightForce;
        [SerializeField] public Transform transform;
    }
}