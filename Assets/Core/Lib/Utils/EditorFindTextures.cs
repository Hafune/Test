//https://forum.unity.com/threads/how-to-change-png-import-settings-via-script.734834/

#if UNITY_EDITOR
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VInspector;

public class EditorFindTextures : MonoBehaviour
{
    [Button]
    private void FindTextures()
    {
        var guids = AssetDatabase.FindAssets("t:Texture",
            new[] { "Assets/Additionals/Tower", "Assets/Additionals/XBOX BUTTONS - Premium Assets" });

        FilterTextures(guids);
        
        //https://discussions.unity.com/t/get-all-textures-from-a-material/15074/5
        // IEnumerable<Texture> GetTextures(Renderer renderer)
        // {
        //     foreach (Object obj in EditorUtility.CollectDependencies(new UnityEngine.Object[] {renderer}))
        //     {
        //         if (obj is Texture)
        //         {
        //             yield return obj as Texture;
        //         }
        //     }
        // }
    }

    public static void FilterTextures(string[] guids)
    {
        if (guids.Length == 0)
            return;

        string folder = null;
        var a = guids
            .Where(s =>
            {
                var path = AssetDatabase.GUIDToAssetPath(s);

                TextureImporter importer = AssetImporter.GetAtPath(path) as TextureImporter;

                if (importer is null // || importer.crunchedCompression 
                    // || importer.textureShape != TextureImporterShape.Texture2D //||
                    || importer.textureType != TextureImporterType.Sprite
                    // ||
                    // importer.textureCompression != TextureImporterCompression.Uncompressed
                   )
                    return false;

                importer.GetSourceTextureWidthAndHeight(out int w, out int h);

                if (w % 4 == 0 && h % 4 == 0)
                    return false;

                int W = w % 4 == 0 ? w : w + (4 - w % 4);
                int H = h % 4 == 0 ? h : h + (4 - h % 4);

                var dir = Path.GetDirectoryName(path);
                folder ??= dir;

                if (dir != folder)
                    return false;

                var t = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
                Debug.Log(dir, t);

                return true; //importer.maxTextureSize == 2048;
            })
            .Select(AssetDatabase.GUIDToAssetPath)
            .Select(AssetDatabase.LoadAssetAtPath<Texture>).ToArray();

        Selection.objects = a;
    }
}

#endif