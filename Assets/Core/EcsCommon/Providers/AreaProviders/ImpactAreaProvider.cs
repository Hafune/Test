using Core.Components;
using UnityEngine;
using Voody.UniLeo.Lite;

[DisallowMultipleComponent]
public class ImpactAreaProvider : MonoProvider<ImpactAreaComponent>
{
    private void OnValidate() => value.area =
        value.area is ImpactArea ? value.area : GetComponentInChildren<ImpactArea>(true);
}