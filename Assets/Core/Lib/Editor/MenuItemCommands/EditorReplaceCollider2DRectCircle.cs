#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
    public static class EditorReplaceCollider2DRectCircle
    {
        [MenuItem("Auto/Swap circle and box colliders &r")]
        private static void Execute2()
        {
            Debug.Log($"(Alt+R) command");

            var col = Selection.gameObjects
                .Select(o => o.GetComponent<Collider2D>())
                .FirstOrDefault(c => c != null);

            if (col == null)
                return;

            Undo.RecordObject(col, "");
            Collider2D newCollider;
            var enable = col.enabled;

            switch (col)
            {
                case BoxCollider2D:
                    newCollider = Undo.AddComponent<CircleCollider2D>(col.gameObject);
                    break;
                case CircleCollider2D:
                    newCollider = Undo.AddComponent<BoxCollider2D>(col.gameObject);
                    break;
                default:
                    throw new Exception("Swap object not found");
            }

            var go = col.gameObject;
            newCollider.enabled = enable;
            Undo.DestroyObjectImmediate(col);
            EditorUtility.SetDirty(go);
        }
    }
}
#endif