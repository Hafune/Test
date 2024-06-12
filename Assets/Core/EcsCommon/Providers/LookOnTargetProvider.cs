using Core.Components;
using UnityEngine;
using Voody.UniLeo.Lite;

[DisallowMultipleComponent]
public class LookOnTargetProvider : MonoProvider<LookOnTargetComponent>
{
    private void OnValidate() => value.transform = value.transform ? value.transform : transform;
}