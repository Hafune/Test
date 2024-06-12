using Core.Components;
using UnityEngine;
using Voody.UniLeo.Lite;

[DisallowMultipleComponent]
public class BehaviourActivateAreaProvider : MonoProvider<BehaviourActivateAreaComponent>
{
    private void OnValidate()
    {
        if (!value.collider)
            return;

        if (value.baseRadius == 0)
            value.baseRadius = value.collider.radius;
    }
}