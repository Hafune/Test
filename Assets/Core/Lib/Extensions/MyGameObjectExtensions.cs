using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Lib
{
    public static class MyGameObjectExtensions
    {
        public static string GetPath(this GameObject obj)
        {
            string path = obj.name;

            while (obj.transform.parent != null && obj.transform.parent.parent != null)
            {
                obj = obj.transform.parent.gameObject;
                path = obj.name + "/" + path;
            }

            return path;
        }
        
        public static void Rename(this GameObject go, string name)
        {
#if UNITY_EDITOR
            if (PrefabUtility.IsPartOfPrefabAsset(go))
                return;

            go.name = name;

            EditorUtility.SetDirty(go);
#endif
        }
    }
}