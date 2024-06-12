using Core.Components;
using UnityEngine;
using Voody.UniLeo.Lite;

[DisallowMultipleComponent]
public class EventImpactVelocityProvider : MonoProvider<EventImpactVelocity>
{
    private void OnValidate() => value.transform = value.transform ? value.transform : transform;
}