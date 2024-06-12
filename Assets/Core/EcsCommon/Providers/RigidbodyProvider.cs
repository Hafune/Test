using Core.Components;
using Voody.UniLeo.Lite;
using UnityEngine;

[DisallowMultipleComponent]
public class RigidbodyProvider : MonoProvider<RigidbodyComponent>
{
    private void OnValidate() => value.rigidbody = value.rigidbody ? value.rigidbody : GetComponent<Rigidbody>();
}