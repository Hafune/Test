using Core.Components;
using UnityEngine;
using Voody.UniLeo.Lite;

[DisallowMultipleComponent]
public class ReceiverAreaProvider : MonoProvider<ReceiverAreaComponent>
{
    private void OnValidate() =>
        value.area = value.area is ReceiverArea ? value.area : GetComponentInChildren<ReceiverArea>(true);
}