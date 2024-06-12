#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class ComparePrefab : AbstractCompare
{
    [SerializeField] private GameObject prefab;

    public override bool Compare(GameObject go) => Compare(prefab,go);

    public static bool Compare(GameObject go1, GameObject go2) =>
        PrefabUtility.GetCorrespondingObjectFromOriginalSource(go1) ==
        PrefabUtility.GetCorrespondingObjectFromOriginalSource(go2);
}
#endif