#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Lib;
using UnityEditor;
using UnityEngine;
using VInspector;

public class EditorFindMeshInObject : MonoBehaviour
{
    private static readonly Dictionary<Mesh, Data> MashCache = new();

    [SerializeField] private bool _includeInactive;
    [SerializeField] private List<Data> _meshes = new();

    [Button, ContextMenu(nameof(SortByCount))]
    private void SortByCount() => _meshes = _meshes.OrderByDescending(i => i.count).ToList();

    [Button, ContextMenu(nameof(SortByVertex))]
    private void SortByVertex() => _meshes = _meshes.OrderByDescending(i => i.vertexCount).ToList();

    [Button, ContextMenu(nameof(Select))]
    private void Select()
    {
        Selection.objects = _meshes
            .Where(i => i.vertexCount > 6000)
            .Select(i => i.go)
            .ToArray();
    }

    [Button, ContextMenu(nameof(Find))]
    private void Find()
    {
        _meshes.Clear();
        MashCache.Clear();

        foreach (var meshFilter in GetComponentsInChildren<MeshFilter>(_includeInactive))
        {
            if (!meshFilter.sharedMesh)
                continue;

            if (MashCache.TryGetValue(meshFilter.sharedMesh, out var data))
            {
                data.count++;
                continue;
            }

            MashCache.Add(meshFilter.sharedMesh, new Data
            {
                count = 1,
                mesh = meshFilter.sharedMesh,
                vertexCount = meshFilter.sharedMesh.vertexCount,
                go = meshFilter.gameObject
            });
        }

        MashCache.Values.ForEach(d => d.fontName = $"vert {d.vertexCount}, count {d.count}, | {d.go.name}");
        _meshes = MashCache.Values.OrderByDescending(i => i.vertexCount).ToList();
    }

    [Serializable]
    private class Data
    {
        [HideInInspector] public string fontName;
        public Mesh mesh;
        public GameObject go;
        public int count;
        public int vertexCount;
    }
}
#endif