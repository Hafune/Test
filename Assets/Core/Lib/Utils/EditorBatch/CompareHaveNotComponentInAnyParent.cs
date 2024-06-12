using System;
using UnityEngine;

public class CompareHaveNotComponentInAnyParent : AbstractCompare
{
    [SerializeField] private Component _manualSetComponent;
    public string AssemblyQualifiedName;
    private Type type;

    private void OnValidate()
    {
        if (!_manualSetComponent)
            return;

        AssemblyQualifiedName = _manualSetComponent!.GetType().AssemblyQualifiedName;
        _manualSetComponent = null;
        type = null;
    }

    public override bool Compare(GameObject go) =>
        go.GetComponentInParent(type ??= Type.GetType(AssemblyQualifiedName)) != null;
}