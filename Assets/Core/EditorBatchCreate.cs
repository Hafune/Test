#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Core.Lib;
using Lib;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class EditorBatchCreate : AbstractEditorBatchExecute
{
    private string dir = "Assets/Prefabs/Level/Arts";
    private static List<GameObject> prefabs = new();

    protected override IEnumerator ExecuteProtected(Transform[] suitable, Action callback, Action cancel)
    {
        prefabs.Clear();

        if (prefabs.Count == 0)
        {
            var guids = AssetDatabase.FindAssets("t:prefab", new[] { dir });
            foreach (var guid in guids)
            {
                var value = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid));

                if (!value)
                    continue;

                prefabs.Add(value);
            }
        }

        foreach (var i in suitable)
        {
            var arts = i.transform.FindRecursiveAll("art", true)
                .Concat(i.transform.FindRecursiveAllStartsWith("art ("))
                .Concat(i.transform.FindRecursiveAll("art_1", true))
                .ToArray();

            arts = arts.Where(a =>
                {
                    Transform parent = a.parent;
                    while (parent)
                    {
                        if (arts.Contains(parent))
                            return false;

                        parent = parent.parent;
                    }

                    return true;
                })
                .ToArray();

            foreach (var a in arts)
            {
                if (!a.TryGetComponent<SpawnPropsData>(out var data))
                    data = a.gameObject.AddComponent<SpawnPropsData>();

                if (!string.IsNullOrEmpty(data.Art?.AssetGUID))
                    continue;

                data.Art = new AssetReference(AssetDatabase
                    .GUIDFromAssetPath(AssetDatabase.GetAssetPath(ForChild(a).gameObject)).ToString());
            }

            // yield return null;
            EditorUtility.SetDirty(i);
        }

        yield return null;

        callback?.Invoke();
    }

    private GameObject ForChild(Transform t)
    {
        var artName = t.root.name + "__" + t.gameObject.GetPath();
        artName = Regex.Replace(artName, "/", "__");

        var _p = prefabs.FirstOrDefault(i => i.name == artName);
        if (_p)
            return _p;

        string localPath = dir + "/" + artName + ".prefab";
        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);

        var obj = new GameObject();
        obj.transform.parent = t.parent;
        obj.transform.localPosition = t.localPosition;
        obj.transform.localRotation = t.localRotation;
        obj.transform.localScale = t.localScale;

        List<Transform> children = new();
        foreach (Transform child in t)
            children.Add(child);

        foreach (Transform child in children)
            child.parent = obj.transform;

        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;

        PrefabUtility.SaveAsPrefabAsset(obj, localPath, out _);

        DestroyImmediate(obj);

        var p = AssetDatabase.LoadAssetAtPath<GameObject>(localPath);
        prefabs.Add(p);
        return p;
    }
}
#endif