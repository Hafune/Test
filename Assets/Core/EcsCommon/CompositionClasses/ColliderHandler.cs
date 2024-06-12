using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public class ColliderHandler
    {
        [SerializeField] public Collider2D collider;
        [SerializeField] private bool _defaultIsActive = true;

        public void ResetToDefault() => collider.enabled = _defaultIsActive;
    }
}