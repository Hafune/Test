using System;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct RigidbodyComponent : IEcsAutoReset<RigidbodyComponent>
    {
        public Rigidbody rigidbody;

        public void AutoReset(ref RigidbodyComponent c)
        {
            if (!c.rigidbody)
                return;

            c.rigidbody.velocity = Vector3.zero;
        }
    }
}