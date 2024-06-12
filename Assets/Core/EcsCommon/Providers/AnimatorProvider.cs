using Core.Components;
using Voody.UniLeo.Lite;
using UnityEngine;

[DisallowMultipleComponent]
public class AnimatorProvider : MonoProvider<AnimatorComponent>
{
    private void OnValidate()
    {
        value.animator = value.animator ? value.animator : GetComponentInChildren<Animator>();
    }
}