using Core.Components;
using EPOOutline;
using UnityEngine;
using Voody.UniLeo.Lite;

[DisallowMultipleComponent]
public class OutlinableProvider : MonoProvider<OutlinableComponent>
{
    private void OnValidate() => value.outlinable = value.outlinable ? value.outlinable : GetComponent<Outlinable>();
}