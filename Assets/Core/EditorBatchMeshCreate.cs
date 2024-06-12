#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using Core.Lib;
using Lib;
using UnityEditor;
using UnityEngine;

public class EditorBatchMeshCreate : AbstractEditorBatchExecute
{
    private string dir = "Assets/Prefabs/Level/MeshPrefabs";
    private static Glossary<MeshFilter> _prefabs = new();

    protected override IEnumerator ExecuteProtected(Transform[] suitable, Action callback, Action cancel)
    {
        _prefabs.Clear();
        if (_prefabs.Length == 0)
        {
            var guids = AssetDatabase.FindAssets("t:prefab", new[] { dir });
            foreach (var guid in guids)
            {
                var value = AssetDatabase.LoadAssetAtPath<MeshFilter>(AssetDatabase.GUIDToAssetPath(guid));

                if (!value)
                    continue;

                _prefabs.Add(value.sharedMesh.GetInstanceID(), value);
            }
        }

        foreach (var i in suitable)
        {
            foreach (Transform t in i)
                ForChild(t);

            // yield return null;
            EditorUtility.SetDirty(i);
        }

        yield return null;

        callback?.Invoke();
    }

    private void ForChild(Transform t)
    {
        if (PrefabUtility.IsPartOfAnyPrefab(t))
            return;

        foreach (Transform t1 in t)
            ForChild(t1);

        if (!t.TryGetComponent<MeshFilter>(out var meshFilter) || !meshFilter.sharedMesh || t.TryGetComponent<MonoBehaviour>(out _))
            return;

        var mesh = meshFilter.sharedMesh;
        MeshFilter prefab = null;

        foreach (var e in _prefabs)
        {
            if (e.Key != mesh.GetInstanceID())
                continue;

            prefab = e.Value;
            break;
        }

        bool isActive = t.gameObject.activeSelf;

        if (prefab)
        {
            Debug.Log("Уже создан " + prefab.name);
        }
        else
        {
            List<Transform> list = new();

            if (t.childCount != 0)
                t.ForEachSelfChildren<Transform>(list.Add);

            foreach (var child in list)
                child.SetParent(null);

            var mat = mesh;

            string localPath = dir + "/" + (mat == null ? "empty" : mat.name) + ".prefab";
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
            var path = t.gameObject.GetPath();
            var root = t.root;

            t.gameObject.SetActive(true);

            PrefabUtility.SaveAsPrefabAsset(t.gameObject, localPath, out var success);
            Debug.Log(localPath);
            var original = AssetDatabase.LoadAssetAtPath<MeshFilter>(localPath);
            _prefabs.Add(original.sharedMesh.GetInstanceID(), original);
            Debug.Log("Новый " + t.name);

            AssetDatabase.ImportAsset(localPath);

            if (!success)
                throw new Exception("не сохранило");

            var inst = root.Find(path);
            inst.gameObject.SetActive(isActive);

            foreach (var child in list)
                child.SetParent(inst.transform);
        }
    }
}
#endif