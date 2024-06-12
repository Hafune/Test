#if UNITY_EDITOR
using System.Linq;
using UnityEditor;

namespace Core
{
    public static class EditorSomeCommands
    {
        [MenuItem("Auto/Sort gameObjects by name")]
        private static void Setting()
        {
            if (!Selection.activeGameObject)
                return;

            int index = Selection.gameObjects.Min(i => i.transform.GetSiblingIndex());
            foreach (var go in Selection.gameObjects.OrderBy(i => i.name))
            {
                go.transform.SetSiblingIndex(index++);
                EditorUtility.SetDirty(go);
            }
        }
    }
}
#endif