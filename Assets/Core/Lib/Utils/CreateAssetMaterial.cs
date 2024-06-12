#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class CreateAssetMaterial : MonoBehaviour
{
    [SerializeField] private Object _prefabForPath;
    [SerializeField] private string _filename;

    [ContextMenu("Create")]
    private void Create() => Create(GetComponent<MeshRenderer>().material);

    public void Create(Material material)
    {
        var filename = string.IsNullOrEmpty(_filename)
            ? material.name.Replace("Instance", "").Replace(" ()", "").Trim()
            : _filename;
        string path = AssetDatabase.GetAssetPath(_prefabForPath);
        path = path.Remove(path.LastIndexOf('/')) + "/" + filename + ".mat";

        CreateAsset.Create(material, path);
    }
}
#endif