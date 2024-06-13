using Core.Components;
using UnityEngine;
using Voody.UniLeo.Lite;

[DisallowMultipleComponent]
public class ReceiverMagnetAreaProvider : MonoProvider<ReceiverMagnetAreaComponent>
{
    private void OnValidate() =>
        value.area = value.area is ReceiverMagnetArea ? value.area : GetComponentInChildren<ReceiverMagnetArea>(true);
}