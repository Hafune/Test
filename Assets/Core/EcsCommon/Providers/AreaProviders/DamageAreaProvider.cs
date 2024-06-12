using Core.Components;
using Voody.UniLeo.Lite;
using UnityEngine;

[DisallowMultipleComponent]
public class DamageAreaProvider : MonoProvider<DamageAreaComponent>
{
    private void OnValidate() => value.area =
        value.area is DamageArea ? value.area : GetComponentInChildren<DamageArea>(true);
}