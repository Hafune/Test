#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Lib;
using UnityEditor;
using UnityEngine;
using VInspector;

public class EditorFindDependenciesInObject : MonoBehaviour
{
    private static readonly Dictionary<Texture, Data> MashCache = new();

    [SerializeField] private bool _includeInactive;
    [SerializeField] private List<Data> _meshes = new();

    [Button, ContextMenu(nameof(SortByCount))]
    private void SortByCount() => _meshes = _meshes.OrderByDescending(i => i.count).ToList();

    [Button, ContextMenu(nameof(Select))]
    private void Select()
    {
        Selection.objects = _meshes
            .Where(i => i.count < 100)
            .Select(i => i.texture)
            .ToArray();
    }

    [Button, ContextMenu(nameof(Find))]
    private void Find()
    {
        _meshes.Clear();
        MashCache.Clear();

        foreach (var obj in EditorUtility.CollectDependencies(new UnityEngine.Object[] { gameObject }))
        {
            if (obj is not Texture mainTexture)
                continue;

            if (MashCache.TryGetValue(mainTexture, out var data))
            {
                data.count++;
                continue;
            }

            MashCache.Add(mainTexture, new Data
            {
                fontName = mainTexture.name,
                count = 1,
                texture = mainTexture,
                go = gameObject
            });
        }

        MashCache.Values.ForEach(d => d.fontName = $"count {d.count}, | {d.fontName}");
        _meshes = MashCache.Values.OrderBy(i => i.count).ToList();

        Debug.Log("complete");
    }

    [Serializable]
    private class Data
    {
        [HideInInspector] public string fontName;
        public Texture texture;
        public GameObject go;
        public int count;
    }
}

#endif