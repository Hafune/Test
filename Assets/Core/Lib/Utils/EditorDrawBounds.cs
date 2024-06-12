namespace Core.Lib.Utils
{
    using UnityEngine;

    public class EditorDrawBounds : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            hideFlags = HideFlags.DontSave;
            var mesh_renderer = GetComponent<MeshRenderer>();

            if (!mesh_renderer)
                return;

            Gizmos.DrawWireCube(mesh_renderer.bounds.center, mesh_renderer.bounds.size);
            Gizmos.DrawWireSphere(mesh_renderer.bounds.center, 0.3f);
        }
    }
}