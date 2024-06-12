using Core.Components;
using UnityEngine;
using Voody.UniLeo.Lite;

[DisallowMultipleComponent]
public class DirectionProvider : MonoProvider<DirectionComponent>
{
    private void OnValidate() => value.transform = value.transform ? value.transform : transform;
}