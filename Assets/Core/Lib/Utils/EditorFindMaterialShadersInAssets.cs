#if UNITY_EDITOR
using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using VInspector;

public class EditorFindMaterialShadersInAssets : MonoBehaviour
{
    private static readonly int MetallicGlossMapId = Shader.PropertyToID("_MetallicGlossMap");

    [SerializeField] private Material _example;
    [SerializeField] private string _shaderName;
    [SerializeField] private string _path = "Additionals/Tower";

    [Button]
    private void ExtractName() => _shaderName = _example ? _example.shader.name : "";

    [Button]
    private void FindMaterials()
    {
        if (string.IsNullOrEmpty(_shaderName))
            return;

        var guids = AssetDatabase.FindAssets("t:Material", new[] { $"Assets/{_path}" });

        if (guids.Length == 0)
            return;

        var materials = guids
            .Select(s =>
            {
                var mat = AssetDatabase.LoadAssetAtPath<Material>(AssetDatabase.GUIDToAssetPath(s));

                if (!mat.shader.name.Contains(_shaderName))
                    return null;

                // if (!mat.IsKeywordEnabled("_EMISSION"))
                //     return null;

                // if (mat.GetColor("_EmissionColor") != Color.black)
                //     return null;

                if (mat.GetFloat("_Surface") == 1)
                    return null;

                // if (mat.GetTexture("_EmissionMap") is not null)
                //     return null;

                if (mat.GetTexture("_BaseMap") is null)
                    return null;

                if (mat.GetTexture("_BumpMap") is not null)
                    return null;

                // if (mat.GetTexture("_BumpMap") is not null)
                //     return null;

                return mat;
            })
            .NotNull().ToArray();

        // foreach (var shaderKeyword in a[0].enabledKeywords)
        //     Debug.Log(shaderKeyword);
        Selection.objects = materials;
    }
}
#endif