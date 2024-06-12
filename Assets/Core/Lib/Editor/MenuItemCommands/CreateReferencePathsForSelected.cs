using System;
using System.Reflection;
using Lib;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Core
{
    public static class CreateReferencePathsForSelected
    {
        [MenuItem("Auto/Create reference paths for selected", false, 1)]
        private static void CreateMany()
        {
            //https://discussions.unity.com/t/how-to-get-path-from-the-current-opened-folder-in-the-project-window-in-unity-editor/226209
            var projectWindowUtilType = typeof(ProjectWindowUtil);
            var getActiveFolderPath =
                projectWindowUtilType.GetMethod("GetActiveFolderPath", BindingFlags.Static | BindingFlags.NonPublic)!;
            var obj = getActiveFolderPath.Invoke(null, Array.Empty<object>());
            var pathToCurrentFolder = obj.ToString();

            foreach (var go in Selection.gameObjects)
                CreateReferencePath(go, pathToCurrentFolder);
        }

        private static void CreateReferencePath(GameObject go, string pathToCurrentFolder)
        {
            var stage = PrefabStageUtility.GetCurrentPrefabStage();
            
            if (!stage)
                return;
            
            var refPath = ScriptableObject.CreateInstance<ReferencePath>();
            refPath.SetPrefabPart(AssetDatabase.LoadAssetAtPath<GameObject>(stage.assetPath), go.GetPath());
            AssetDatabase.CreateAsset(refPath, pathToCurrentFolder + "/Path_" + go.name + ".asset");
        }
    }
}