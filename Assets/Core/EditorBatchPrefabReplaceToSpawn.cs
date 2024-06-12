#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using Core.Lib;
using Lib;
using UnityEditor;
using UnityEngine;

public class EditorBatchPrefabReplaceToSpawn : AbstractEditorBatchExecute
{
    [SerializeField] private SpawnParticleRuntime _spawnPrefab;
    
    protected override IEnumerator ExecuteProtected(Transform[] suitable, Action next, Action cancel)
    {
        foreach (var i in suitable)
        {
            foreach (Transform t in i)
                yield return ForChild(t);

            EditorUtility.SetDirty(i);
        }
        
        yield return null;

        next?.Invoke();
    }

    private IEnumerator ForChild(Transform t)
    {
        if (!PrefabUtility.IsPartOfAnyPrefab(t))
            yield break;

        foreach (Transform t1 in t)
            yield return ForChild(t1);

        if (t.GetComponentInParent<Animator>())
            yield break;
        
        bool isActive = t.gameObject.activeSelf;

        List<Transform> list = new();

        if (t.childCount != 0)
            t.ForEachSelfChildren<Transform>(list.Add);

        foreach (var child in list)
            child.SetParent(null);

        var prefab = PrefabUtility.GetCorrespondingObjectFromOriginalSource(t);
        var n = t.name;
        var instance = EditorReplacePrefab.Replace(t, _spawnPrefab.gameObject, false);
        instance.name = n;
        instance.SetActive(isActive);
        instance.GetComponent<SpawnParticleRuntime>().prefab = prefab;

        foreach (var child in list)
            child.SetParent(instance.transform);
    }
}
#endif