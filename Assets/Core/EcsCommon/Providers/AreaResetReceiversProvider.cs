using System;
using Core.Components;
using UnityEngine;
using Voody.UniLeo.Lite;

[DisallowMultipleComponent]
public class AreaResetReceiversProvider : MonoProvider<AreaResetReceiversComponent>
{
#if UNITY_EDITOR
    private void Awake()
    {
        if (!value.area)
            throw new Exception("Область не задана");
    }
#endif
}