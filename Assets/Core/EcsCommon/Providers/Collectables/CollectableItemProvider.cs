using Core.Components;
using UnityEngine;
using Voody.UniLeo.Lite;

[DisallowMultipleComponent]
public class CollectableItemProvider : MonoProvider<CollectableItemComponent>
{
    private void OnValidate() => value.spriteRenderer =
        value.spriteRenderer ? value.spriteRenderer : GetComponentInChildren<SpriteRenderer>();
}