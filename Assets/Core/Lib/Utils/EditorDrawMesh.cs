using System;
using UnityEngine;

namespace Core.Lib
{
    public class EditorDrawMesh
    {
        private Action<Camera> OnDraw;
        private bool _enabled;

        public void EnableDraw(Action<Camera> onDraw)
        {
            OnDraw = onDraw;

            if (_enabled)
                return;

            _enabled = true;

            if (UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset == null) // built-in
                Camera.onPreCull += DrawMesh;
            else // SRP
                UnityEngine.Rendering.RenderPipelineManager.beginCameraRendering += DrawMesh;
        }

        public void DisableDraw()
        {
            _enabled = false;

            if (UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset == null) // built-in
                Camera.onPreCull -= DrawMesh;
            else // SRP
                UnityEngine.Rendering.RenderPipelineManager.beginCameraRendering -= DrawMesh;
        }

        private void DrawMesh(UnityEngine.Rendering.ScriptableRenderContext context, Camera cam) => DrawMesh(cam);

        private void DrawMesh(Camera cam) => OnDraw?.Invoke(cam);
    }
}