#if UNITY_EDITOR
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Core.Lib
{
    public class EditorCombinedMeshReplaceByName : AbstractEditorBatchExecute
    {
        protected override IEnumerator ExecuteProtected(Transform[] suitable, Action callback, Action cancel)
        {
            foreach (var t in suitable)
            {
                if (t.IsDestroyed())
                    continue;
                
                var meshFilter = t.GetComponent<MeshFilter>();

                if (!meshFilter.sharedMesh.name.StartsWith("Combined Mesh"))
                    continue;

                try
                {
                    // var oName = t.name.Split(" ")[0];
                    // var guid = AssetDatabase.FindAssets($"t:Mesh {oName}")[0];
                    // var mesh = AssetDatabase.LoadAssetAtPath<Mesh>(AssetDatabase.GUIDToAssetPath(guid));
                    //
                    // if (!mesh)
                    //     continue;
                    //
                    // meshFilter.sharedMesh = mesh;
                    EditorUtility.SetDirty(t);
                    DestroyImmediate(t.gameObject);
                }
                catch (Exception e)
                {
                    // Debug.LogError("Ошибка замены меша " + t.name + " message: " + e.Message, t);
                    Debug.LogError(e.Message);
                }
            }

            yield return null;

            callback?.Invoke();
        }
    }
}
#endif