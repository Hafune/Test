#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VInspector;

public class EditorFindPrefabsWithCustom : MonoBehaviour
{
    [SerializeField] private string _assetsPath = "Prefabs";
    [SerializeField] private GameObject[] _prefabs;

    [Button]
    private void FindPrefabsFromPath()
    {
        var guids = AssetDatabase.FindAssets("t:prefab", new[] { "Assets/" + _assetsPath });
        var prefabs = new List<ParticleSystemRenderer>();
        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var go = AssetDatabase.LoadAssetAtPath<ParticleSystemRenderer>(path);
            prefabs.Add(go);
        }

        if (prefabs.Count == 0)
            return;

        _prefabs = prefabs
            .Where(p => p && !p.enabled)
            .Where(p => p.gameObject.TryGetComponent<ParticleSystem>(out _))
            .Where(p => !PrefabUtility.IsPartOfVariantPrefab(p))
            .Select(p => p.gameObject)
            .OrderBy(p => p.name)
            .ToArray();
    }

    [Button]
    private void Select() => Selection.objects = _prefabs;
}
#endif