#if UNITY_EDITOR
using System;
using System.Linq;
using Lib;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
    public static class EditorCollider2DSizeFromMeshCommand
    {
        //https://docs.unity3d.com/ScriptReference/MenuItem.html
        //% (ctrl on Windows and Linux, cmd on macOS), ^ (ctrl on Windows, Linux, and macOS), # (shift), & (alt).
        private static Bounds? bounds;

        [MenuItem("Auto/Set collider size from mesh bounds &f")]
        private static void Execute()
        {
            Debug.Log($"(Alt+F) command");

            if (Selection.count == 1)
                WhenSelectedOnlyCollider();
            else
                WhenSelectedColliderAndRenderer();
        }

        private static void WhenSelectedColliderAndRenderer()
        {
            var col = Selection.gameObjects
                .Select(o => o.GetComponent<Collider2D>())
                .FirstOrDefault(c => c != null);

            if (col == null)
                return;

            var renderers = Selection.gameObjects
                .Select(o => o.GetComponent<SkinnedMeshRenderer>())
                .Where(i => i)
                .ToArray();

            bounds = null;
            foreach (var renderer in renderers)
            {
                renderer.updateWhenOffscreen = true;
                EditorApplication.delayCall += () => DelayUpdate(renderer);
                EditorApplication.delayCall += () => Update(col, bounds!.Value);
            }
        }

        private static void WhenSelectedOnlyCollider()
        {
            var col = Selection.activeGameObject.GetComponent<Collider2D>();

            if (col == null)
                return;

            var renderers = col.transform.root.GetComponentsInChildren<SkinnedMeshRenderer>();

            if (renderers.Length == 0)
                return;

            bounds = null;
            foreach (var v in renderers)
            {
                v.updateWhenOffscreen = true;
                EditorApplication.delayCall += () => DelayUpdate(v);
            }

            EditorApplication.delayCall += () => Update(col, bounds!.Value);
        }

        private static void Update(Collider2D col, Bounds bounds)
        {
            var transform = col.transform;
            Undo.RecordObjects(new Object[] { col.transform, col }, "");
            col.offset = Vector2.zero;
            var ex = bounds.extents;

            switch (col)
            {
                case BoxCollider2D b2d:
                    transform.localScale = bounds.size.Copy(z: 1);
                    b2d.size = Vector2.one;
                    break;
                case CircleCollider2D c2d:
                    transform.localScale = (Vector3.one * Mathf.Max(ex.x, ex.y, ex.z)).Copy(z: 1);
                    c2d.radius = 1;
                    break;
            }

            transform.position = bounds.center.Copy(z: 0);
            EditorUtility.SetDirty(col);
        }

        private static void DelayUpdate(SkinnedMeshRenderer renderer)
        {
            bounds ??= renderer.bounds;
            var b1 = bounds.Value;
            var b2 = renderer.bounds;

            var min = new Vector3(
                Math.Min(b1.min.x, b2.min.x),
                Math.Min(b1.min.y, b2.min.y),
                Math.Min(b1.min.z, b2.min.z));

            var max = new Vector3(
                Math.Max(b1.max.x, b2.max.x),
                Math.Max(b1.max.y, b2.max.y),
                Math.Max(b1.max.z, b2.max.z));

            var b = new Bounds();
            b.min = min;
            b.max = max;
            bounds = b;
            renderer.updateWhenOffscreen = false;
        }
    }
}
#endif