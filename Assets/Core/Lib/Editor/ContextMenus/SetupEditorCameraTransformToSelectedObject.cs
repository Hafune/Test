using UnityEditor;
using UnityEngine;

namespace Core
{
    public static class SetupEditorCameraTransformToSelectedObject
    {
        [MenuItem("Auto/Setup editor camera transform to selected", false, 1)]
        private static void Execute()
        {
            var view = SceneView.currentDrawingSceneView ?? SceneView.lastActiveSceneView;
            if (view != null && Selection.activeObject is GameObject go && go.TryGetComponent<Camera>(out var c))
            {
                c.transform.position = view.pivot;
                c.transform.rotation = view.rotation;
            }
        }
    }
}