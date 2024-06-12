using System;
using UnityEngine;

public class CompareComponent : AbstractCompare
{
    [SerializeField] private Component _manualSetComponent;
    public string AssemblyQualifiedName;

    private void OnValidate()
    {
        if (!_manualSetComponent)
            return;

        AssemblyQualifiedName = _manualSetComponent!.GetType().AssemblyQualifiedName;
        _manualSetComponent = null;
    }

    public override bool Compare(GameObject go) => go.GetComponent(Type.GetType(AssemblyQualifiedName)) != null;
}