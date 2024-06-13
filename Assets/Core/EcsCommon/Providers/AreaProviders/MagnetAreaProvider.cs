using Core.Components;
using UnityEngine;
using Voody.UniLeo.Lite;

[DisallowMultipleComponent]
public class MagnetAreaProvider : MonoProvider<MagnetAreaComponent>
{
    private void OnValidate() =>
        value.area = value.area is MagnetArea ? value.area : GetComponentInChildren<MagnetArea>(true);
}