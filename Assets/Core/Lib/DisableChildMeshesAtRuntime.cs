using UnityEngine;

namespace Core.Lib
{
    public class DisableChildMeshesAtRuntime : MonoBehaviour
    {
        void Start()
        {
            var renderers = GetComponentsInChildren<Renderer>();

            for (int i = 0, count = renderers.Length; i < count; i++)
                renderers[i].enabled = false;
        }
    }
}