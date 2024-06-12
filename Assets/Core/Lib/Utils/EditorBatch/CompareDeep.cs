#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Lib;
using UnityEngine;

[ExecuteInEditMode]
public class CompareDeep : AbstractCompare
{
    public void MakeFiltersForChildren(GameObject go)
    {
        var mainFilter = GetComponent<EditorBatchFilter>();

        foreach (var child in transform.GetSelfChildrenTransforms())
            DestroyImmediate(child.gameObject);

        foreach (Transform child in go.transform)
        {
            var filterGo = new GameObject();
            filterGo.transform.SetParent(transform);
            var filter = filterGo.AddComponent<EditorBatchFilter>();
            filter._addDeepCompare = true;
            filter._addCompareName = mainFilter._addCompareName;
            filter._addCompareChildCount = mainFilter._addCompareChildCount;
            filter._addCompareComponentCount = mainFilter._addCompareComponentCount;
            filter._addIgnorePrefabs = mainFilter._addIgnorePrefabs;
            filter._reference = child.gameObject;
        }
    }

    public override bool Compare(GameObject go)
    {
        var filters = new List<EditorBatchFilter>();

        transform.ForEachSelfChildren<EditorBatchFilter>(filters.Add);

        var childs = new List<GameObject>();

        foreach (Transform t in go.transform)
            childs.Add(t.gameObject);

        if (filters.Count != childs.Count)
            return false;

        for (int i = 0, iMax = filters.Count; i < iMax; i++)
            if (!filters[i].GetComponents<AbstractCompare>().All(c => c.Compare(childs[i])))
                return false;

        return true;
    }

    private void OnDestroy()
    {
        foreach (var child in transform.GetSelfChildrenTransforms())
            DestroyImmediate(child.gameObject);
    }
}
#endif