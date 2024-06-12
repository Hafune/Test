#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class CompareIsNotPartOfAnyPrefab : AbstractCompare
{
    public override bool Compare(GameObject go) => !PrefabUtility.IsPartOfAnyPrefab(go);
}

#endif