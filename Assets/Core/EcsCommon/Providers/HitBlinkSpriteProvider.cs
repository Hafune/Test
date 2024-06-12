using Core.Components;
using UnityEngine;
using Voody.UniLeo.Lite;

[DisallowMultipleComponent]
public class HitBlinkSpriteProvider : MonoProvider<HitEffectSpriteComponent>
{
    private void OnValidate() => value.hitBlink = value.hitBlink ??= GetComponentInChildren<HitBlinkSprite>();
}