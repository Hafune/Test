using BehaviorDesigner.Runtime;
using Core.Components;
using UnityEngine;
using Voody.UniLeo.Lite;

[DisallowMultipleComponent,RequireComponent(typeof(BehaviorTree))]
public class BehaviorTreeProvider : MonoProvider<BehaviorTreeComponent>
{
    private void OnValidate() => value.tree = value.tree ? value.tree : GetComponent<BehaviorTree>();
}