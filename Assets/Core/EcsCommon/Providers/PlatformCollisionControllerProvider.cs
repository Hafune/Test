using Core;
using Core.Components;
using UnityEngine;
using Voody.UniLeo.Lite;

[DisallowMultipleComponent]
public class PlatformCollisionControllerProvider : MonoProvider<PlatformCollisionControllerComponent>
{
    private void OnValidate() => value.platformCollisionController = value.platformCollisionController
        ? value.platformCollisionController
        : GetComponentInChildren<PlatformCollisionController>();
}