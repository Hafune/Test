#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VInspector;

public class EditorFindPrefabsWithMeshRenderer : MonoBehaviour
{
    [SerializeField] private string _assetsPath = "Prefabs";
    [SerializeField] private GameObject[] _prefabs;

    [Button]
    private void FindPrefabsFromPath()
    {
        var guids = AssetDatabase.FindAssets("t:prefab", new[] { "Assets/" + _assetsPath });
        var prefabs = new List<GameObject>();
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var go = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            prefabs.Add(go);
        }

        if (prefabs.Count == 0)
            return;

        _prefabs = prefabs.Where(p =>
        {
            var meshRenderer = p.GetComponentsInChildren<MeshRenderer>();

            if (meshRenderer.All(m => m.sharedMaterial == null))
                return false;

            return true;
        }).ToArray();
    }
}
#endif