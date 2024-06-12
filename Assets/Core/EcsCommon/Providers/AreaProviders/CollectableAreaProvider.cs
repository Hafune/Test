using Core.Components;
using Voody.UniLeo.Lite;
using UnityEngine;

[DisallowMultipleComponent]
public class CollectableAreaProvider : MonoProvider<CollectableAreaComponent>
{
    private void OnValidate() =>
        value.area = value.area is CollectableArea ? value.area : GetComponentInChildren<CollectableArea>(true);
}