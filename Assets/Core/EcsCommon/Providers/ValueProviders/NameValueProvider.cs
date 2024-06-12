using Core.Components;
using UnityEngine;
using Voody.UniLeo.Lite;

[DisallowMultipleComponent]
public class NameValueProvider : MonoProvider<NameValueComponent>
{
    private void OnValidate() => value.value = string.IsNullOrEmpty(value.value) ? gameObject.name : value.value;
}