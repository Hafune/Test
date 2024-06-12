#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VInspector;

public class EditorFindMaterialsInObject : MonoBehaviour
{
    private static readonly Dictionary<Shader, Data> ShaderCache = new();
    private static readonly HashSet<Material> MaterialsCache = new();

    [SerializeField] private LayerMask _ignoreLayers;
    [SerializeField] private bool _includeInactive;
    [SerializeField] private List<Data> _renderers = new();

    private void OnValidate()
    {
        var data = _renderers.FirstOrDefault(i => i.selectAll);

        if (data is null)
            return;

        data.selectAll = false;
        var materials = data.meshRenderer.SelectMany(m => m.sharedMaterials).ToArray();
        Debug.Log(string.Join(',',materials[0].enabledKeywords));
        Selection.objects = materials;
    }

    [Button]
    private void Find()
    {
        _renderers.Clear();
        ShaderCache.Clear();
        MaterialsCache.Clear();

        foreach (var meshRenderer in GetComponentsInChildren<MeshRenderer>(_includeInactive)
                     .Where(i => i.enabled && _ignoreLayers != (_ignoreLayers | (1 << i.gameObject.layer)))
                )
        {
            if (meshRenderer.enabled)
                FindMaterial(meshRenderer);
        }

        _renderers = ShaderCache.Values.OrderBy(i => i.fontName).ToList();
    }

    private void FindMaterial(MeshRenderer meshRenderer)
    {
        var materials = meshRenderer.sharedMaterials;
        foreach (var currentMaterial in materials)
        {
            if (currentMaterial is null)
                continue;

            if (ShaderCache.TryGetValue(currentMaterial.shader, out var data))
            {
                if (MaterialsCache.Contains(currentMaterial))
                    continue;

                data.meshRenderer.Add(meshRenderer);
                MaterialsCache.Add(currentMaterial);
            }
            else
            {
                ShaderCache.Add(currentMaterial.shader,
                    new Data
                    {
                        fontName = currentMaterial.shader.name,
                        meshRenderer = new List<MeshRenderer> { meshRenderer }
                    });
                MaterialsCache.Add(currentMaterial);
            }
        }
    }

    [Serializable]
    private class Data
    {
        [HideInInspector] public string fontName;
        public bool selectAll;
        public List<MeshRenderer> meshRenderer = new();
    }
}
#endif