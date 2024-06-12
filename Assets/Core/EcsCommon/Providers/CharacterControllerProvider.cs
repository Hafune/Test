using Core.Components;
using UnityEngine;
using Voody.UniLeo.Lite;

[DisallowMultipleComponent]
[RequireComponent(typeof(CharacterController))]
public class CharacterControllerProvider : MonoProvider<CharacterControllerComponent>
{
    private void OnValidate() => value.characterController =
        value.characterController ? value.characterController : GetComponent<CharacterController>();
}