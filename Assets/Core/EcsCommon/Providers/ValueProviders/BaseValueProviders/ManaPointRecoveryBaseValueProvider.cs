using Core.Components;
using UnityEngine;
using Voody.UniLeo.Lite;

[DisallowMultipleComponent]
public class ManaPointRecoveryBaseValueProvider :
    MonoProvider<BaseValueComponent<RecoverySpeedValueComponent<ManaPointValueComponent>>>
{
}