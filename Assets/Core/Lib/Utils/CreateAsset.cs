#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class CreateAsset
{
    public static void Create(Object obj, string path)
    {
        // Create a simple material asset
        AssetDatabase.CreateAsset(obj, path);//"Assets/MyMaterial.mat"

        // Print the path of the created asset
        Debug.Log(AssetDatabase.GetAssetPath(obj));
        
        // AssetDatabase.CreateAsset(obj, fileName);
        // AssetDatabase.SaveAssets();
        // AssetDatabase.Refresh();
    }
}
#endif