#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using Core.Lib;
using Lib;
using UnityEditor;
using UnityEngine;

public class EditorBatchParticleReplace : AbstractEditorBatchExecute
{
    private string dir = "Assets/Prefabs/Level/Details/Props/New Folder";
    private static List<ValueTuple<int, ParticleSystem>> _particleSystems = new();

    protected override IEnumerator ExecuteProtected(Transform[] suitable, Action callback, Action cancel)
    {
        if (_particleSystems.Count == 0)
        {
            // var guids = AssetDatabase.FindAssets("t:prefab", new[] { dir });
            // foreach (var guid in guids)
            // {
            //     var prefabParticle = AssetDatabase.LoadAssetAtPath<ParticleSystem>(AssetDatabase.GUIDToAssetPath(guid));
            //
            //     if (!prefabParticle)
            //         continue;
            //
            //     _particleSystems.Add(new ValueTuple<int, ParticleSystem> { Item1 = 0, Item2 = prefabParticle });
            // }
        }

        foreach (var i in suitable)
        {
            foreach (Transform t in i)
                yield return ForChild(t);

            // yield return null;
            EditorUtility.SetDirty(i);
        }

        _particleSystems.Sort((a, b) => b.Item1.CompareTo(a.Item1));;
        
        yield return null;

        callback?.Invoke();
    }

    private IEnumerator ForChild(Transform t)
    {
        if (PrefabUtility.IsPartOfAnyPrefab(t))
            yield break;

        foreach (Transform t1 in t)
            yield return ForChild(t1);

        if (!t.TryGetComponent<ParticleSystem>(out var particle))
            yield break;

        ParticleSystem prefab = null;

        for (int i = 0, iMax = _particleSystems.Count; i < iMax; i++)
        {
            var prefabParticle = _particleSystems[i].Item2;
            if (!CompareParticle.Compare(prefabParticle, particle))
                continue;

            if (particle.gameObject.GetComponentCount() != prefabParticle.gameObject.GetComponentCount())
            {
                Debug.LogWarning(particle.gameObject.GetPath() + " Разное количество компонентов", particle.gameObject);
                yield break;
            }

            prefab = prefabParticle;
            _particleSystems[i] = new ValueTuple<int, ParticleSystem>
                { Item1 = _particleSystems[i].Item1 + 1, Item2 = prefabParticle };

            break;
        }

        bool isActive = t.gameObject.activeSelf;

        if (prefab)
        {
            List<Transform> list = new();

            if (t.childCount != 0)
                t.ForEachSelfChildren<Transform>(list.Add);

            foreach (var child in list)
                child.SetParent(null);

            var n = t.name;
            var instance = EditorReplacePrefab.Replace(t, prefab.gameObject, false);
            instance.name = n;
            instance.SetActive(isActive);

            foreach (var child in list)
                child.SetParent(instance.transform);
        }
        else
        {
            List<Transform> list = new();

            if (t.childCount != 0)
                t.ForEachSelfChildren<Transform>(list.Add);

            foreach (var child in list)
                child.SetParent(null);

            var mat = particle.GetComponent<ParticleSystemRenderer>().sharedMaterial;

            string localPath = dir + "/" + (mat == null ? "empty" : mat.name) + ".prefab";
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
            var path = t.gameObject.GetPath();
            var root = t.root;

            t.gameObject.SetActive(true);
            var p = PrefabUtility.SaveAsPrefabAssetAndConnect(t.gameObject, localPath,
                InteractionMode.AutomatedAction, out var success);

            AssetDatabase.ImportAsset(localPath);
            _particleSystems.Add(new ValueTuple<int, ParticleSystem>
                { Item1 = 0, Item2 = p.GetComponent<ParticleSystem>() });

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