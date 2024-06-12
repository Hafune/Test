#if UNITY_EDITOR
using System.Reflection;
using UnityEditor.SceneManagement;

namespace Core.Lib.Utils
{
    public static class EditorFunctions
    {
        private static PropertyInfo autoSaveProperty;
        private static object instanceValue;

        public static void SetAutoSave(bool initialPrefabsAutoSaveValue)
        {
            if (autoSaveProperty != null && instanceValue != null)
            {
                if (autoSaveProperty != null)
                    autoSaveProperty.SetValue(instanceValue, initialPrefabsAutoSaveValue);
                
                return;
            }

            var stageNavigatorManagerType =
                typeof(PrefabStage).Assembly.GetType("UnityEditor.SceneManagement.StageNavigationManager");

            var instanceProperty = stageNavigatorManagerType.GetProperty("instance",
                BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);

            if (instanceProperty == null)
                return;

            instanceValue = instanceProperty.GetValue(instanceProperty);

            if (instanceValue == null)
                return;

            autoSaveProperty ??=
                instanceProperty.PropertyType.GetProperty("autoSave", BindingFlags.Instance | BindingFlags.NonPublic);

            if (autoSaveProperty != null)
                autoSaveProperty.SetValue(instanceValue, initialPrefabsAutoSaveValue);

            //var savePrefabMethod =
            //    typeof(PrefabStage).GetMethod("SavePrefab", BindingFlags.NonPublic | BindingFlags.Instance)!;
            //savePrefabMethod.Invoke(prefabStage, null);
        }
    }
}
#endif