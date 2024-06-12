using UnityEngine;
using Core;
#if UNITY_EDITOR
using Core.Lib;
using Core.Tasks;
using UnityEditor;
using Voody.UniLeo.Lite;

namespace Lib
{
    [InitializeOnLoad]
    public class HierarchyTaskHelper
    {
        private static string path = "Assets/Core/Lib/MyTasks/Icons/";

        private static Texture2D imytask = AssetDatabase.LoadAssetAtPath<Texture2D>(path + "imytask.png");
        private static Texture2D sequence = AssetDatabase.LoadAssetAtPath<Texture2D>(path + "sequence.png");
        private static Texture2D parallel = AssetDatabase.LoadAssetAtPath<Texture2D>(path + "parallel.png");

        private static Texture2D wait = AssetDatabase.LoadAssetAtPath<Texture2D>(path + "wait.png");
        private static Texture2D changeParent = AssetDatabase.LoadAssetAtPath<Texture2D>(path + "changeParent.png");
        private static Texture2D trigger = AssetDatabase.LoadAssetAtPath<Texture2D>(path + "trigger.png");
        private static Texture2D spawner = AssetDatabase.LoadAssetAtPath<Texture2D>(path + "spawner.png");

        private static Texture2D activate = AssetDatabase.LoadAssetAtPath<Texture2D>(path + "activate.png");

        private static Texture2D triggerActivate =
            AssetDatabase.LoadAssetAtPath<Texture2D>(path + "triggerActivate.png");

        private static Texture2D entity = AssetDatabase.LoadAssetAtPath<Texture2D>(path + "entity.png");

        static HierarchyTaskHelper()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
        }

        private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (obj is null)
                return;

            TryDrawTask(obj, selectionRect);
            TryDrawTrigger(obj, selectionRect);
            TryDrawEntity(obj, selectionRect);
        }

        private static void TryDrawTask(GameObject obj, Rect selectionRect)
        {
            var current = obj.GetComponent<IMyTask>();

            if (current is null)
                return;

            var icon = current switch
            {
                TaskSequence => sequence,
                TaskSequenceIfConditionValid => sequence,
                TaskParallel => parallel,
                TaskAwaitSecond => wait,
                TaskWaitEntityDeath => wait,
                TaskChangeParent => changeParent,
                TaskSpawnEntity => spawner,
                SpawnPrefabTask => spawner,
                TaskSetActiveChildren => activate,
                TaskSetActive => activate,
                _ => imytask
            };

            if (icon is not null)
                GUI.DrawTexture(new Rect(selectionRect.x - 30f, selectionRect.y, 16f, 16f), icon);

            var task = obj.GetComponentInChildren<IMyTask>();

            if (task is not { InProgress: true })
                return;

            EditorGUI.DrawRect(selectionRect, new Color(0.2f, 0.6f, 0.1f, .25f));
        }

        private static void TryDrawTrigger(GameObject obj, Rect selectionRect)
        {
            var current = obj.GetComponent<ITrigger>();

            if (current is null)
                return;

            var icon = current switch
            {
                Trigger => trigger,
                TriggerActiveChildren => triggerActivate,
                _ => null
            };

            if (icon is null)
                return;

            GUI.DrawTexture(new Rect(selectionRect.x - 30f, selectionRect.y, 16f, 16f), icon);
        }

        private static void TryDrawEntity(GameObject obj, Rect selectionRect)
        {
            var e = obj.GetComponent<ConvertToEntity>();

            if (e is null)
                return;

            GUI.DrawTexture(new Rect(selectionRect.x - 30f, selectionRect.y, 16f, 16f), entity);
        }
    }
}
#endif