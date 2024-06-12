#if UNITY_EDITOR
using System;
using System.Collections;
using System.Linq;
using Lib;
using UnityEditor;
using UnityEngine;

public class EditorBatchMoveScriptsToAnother : AbstractEditorBatchExecute
{
    protected override IEnumerator ExecuteProtected(Transform[] suitable, Action callback, Action cancel)
    {
        foreach (var i in suitable)
        {
            var arts = i.transform.FindRecursiveAll("art", true)
                .Concat(i.transform.FindRecursiveAllStartsWith("art ("))
                .Concat(i.transform.FindRecursiveAll("art_1", true))
                .ToArray();

            foreach (var art in arts)
            {
                if (!art)
                {
                    Debug.LogWarning("не найдено " + i.name);
                    continue;
                }

                GameObject scripts = null;
                string containerName = "scripts";

                foreach (Transform t in art.transform.parent)
                    if (t.name == containerName)
                    {
                        scripts = t.gameObject;
                        break;
                    }

                if (!scripts)
                {
                    scripts = new GameObject(containerName);
                    scripts.transform.parent = art.transform.parent;
                    scripts.transform.localPosition = Vector3.zero;
                }

                bool hasChanges = false;

                foreach (var go in Select(art.gameObject))
                {
                    go.transform.SetParent(scripts.transform);
                    hasChanges = true;
                }

                if (hasChanges)
                {
                    EditorUtility.SetDirty(i);
                    Debug.Log(i.name);
                }
            }
        }

        yield return null;

        callback?.Invoke();
    }

    private GameObject[] Select(GameObject root)
    {
        var prefabs = root.GetComponentsInChildren<MonoConstruct>(true)
            .Where(w => w)
            .Select(s => s.gameObject)
            .Where(o => o.hideFlags == HideFlags.None)
            .Select(o =>
            {
                if (PrefabUtility.IsPartOfAnyPrefab(o))
                    return PrefabUtility.GetNearestPrefabInstanceRoot(o);

                return o;
            });

        var nonPrefabs = root.GetComponentsInChildren<MonoBehaviour>(true)
            .Where(w => w && w != this)
            .Select(s => s.gameObject)
            .Where(o => !PrefabUtility.IsPartOfAnyPrefab(o) && o.hideFlags == HideFlags.None);

        var particles = root.GetComponentsInChildren<ParticleSystem>(true)
            .Where(w => w && w != this)
            .Select(s => s.gameObject)
            .Distinct()
            .Where(o => !PrefabUtility.IsPartOfAnyPrefab(o) && o.hideFlags == HideFlags.None).ToArray();

        return prefabs.Concat(nonPrefabs).Concat(particles).Distinct().ToArray();
    }
}

#endif