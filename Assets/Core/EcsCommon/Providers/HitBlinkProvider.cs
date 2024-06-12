using Core.Components;
using UnityEngine;
using Voody.UniLeo.Lite;

[RequireComponent(typeof(HitBlink))]
public class HitBlinkProvider : MonoProvider<HitBlinkComponent>
{
    private void OnValidate() => value.hitBlink = value.hitBlink ??= GetComponentInChildren<HitBlink>();
}