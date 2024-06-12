#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Lib;
using UnityEditor;
using UnityEngine;
using VInspector;

public class EditorFindTexturesInObject : MonoBehaviour
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

        foreach (var meshRenderer in GetComponentsInChildren<MeshRenderer>(_includeInactive))
        {
            foreach (var material in meshRenderer.sharedMaterials)
            {
                if (!material || !material.mainTexture)
                    continue;

                if (MashCache.TryGetValue(material.mainTexture, out var data))
                {
                    data.count++;
                    continue;
                }

                MashCache.Add(material.mainTexture, new Data
                {
                    fontName = material.mainTexture.name,
                    count = 1,
                    texture = material.mainTexture,
                    go = meshRenderer.gameObject
                });
                
                // var shader = material.shader;
                // for (int i = 0; i < ShaderUtil.GetPropertyCount(shader); i++)
                // {
                //     if (ShaderUtil.GetPropertyType(shader, i) == ShaderUtil.ShaderPropertyType.TexEnv)
                //     {
                //         var texture = material.GetTexture(ShaderUtil.GetPropertyName(shader, i));
                //
                //         if (!texture)
                //             continue;
                //
                //         if (MashCache.TryGetValue(texture, out var data))
                //         {
                //             data.count++;
                //             continue;
                //         }
                //
                //         MashCache.Add(texture, new Data
                //         {
                //             fontName = texture.name,
                //             count = 1,
                //             texture = texture,
                //             go = meshRenderer.gameObject
                //         });
                //     }
                // }
            }
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