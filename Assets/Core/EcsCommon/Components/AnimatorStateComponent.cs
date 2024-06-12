using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct AnimatorStateComponent
    {
        public PhysicsMaterial2D floatMaterial;
        public PhysicsMaterial2D groundMaterial;
    }
}