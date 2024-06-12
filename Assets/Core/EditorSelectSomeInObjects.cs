#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
using UnityEngine;
using VInspector;

public class EditorSelectSomeInObjects : MonoBehaviour
{
    [Button, ContextMenu(nameof(Select))]
    private void Select()
    {
        var root = transform;
        
        // var prefabs = root.GetComponentsInChildren<MonoConstruct>(true)
        //     .Where(w => w)
        //     .Select(s => s.gameObject)
        //     .Where(o => PrefabUtility.IsAnyPrefabInstanceRoot(o) && o.hideFlags == HideFlags.None);
        //
        // var nonPrefabs = root.GetComponentsInChildren<MonoBehaviour>(true)
        //     .Where(w => w && w != this)
        //     .Select(s => s.gameObject)            
        //     .Where(o => !PrefabUtility.IsPartOfAnyPrefab(o) && o.hideFlags == HideFlags.None);

        var particles = root.GetComponentsInChildren<ParticleSystem>(true)
            .Where(w => w && w != this && w.transform.parent.TryGetComponent<MeshFilter>(out _))
            .Select(s => s.transform.parent.gameObject)
            .Distinct()
            .Where(o => !PrefabUtility.IsPartOfAnyPrefab(o) && o.hideFlags == HideFlags.None).ToArray();
        // var particles = GetComponentsInChildren<LODGroup>(true)
        //     .Where(w =>
        //     {
        //         foreach (Transform t in w.transform)
        //             if (t.localPosition != Vector3.zero)
        //                 return false;
        //
        //         return true;
        //     })
        //     .Select(s => s.gameObject)
        //     .Distinct()
        //     .Where(o => !PrefabUtility.IsPartOfAnyPrefab(o) && o.hideFlags == HideFlags.None).ToArray();

        // Selection.objects = prefabs.Concat(nonPrefabs).Concat(particles).Distinct().ToArray();
        Selection.objects = particles.Distinct().ToArray();
    }
}

#endif