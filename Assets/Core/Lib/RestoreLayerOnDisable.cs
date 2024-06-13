using UnityEngine;

namespace Core.Lib
{
    public class RestoreLayerOnDisable : MonoBehaviour
    {
        private int _startLayer;

        private void Awake() => _startLayer = gameObject.layer;

        private void OnDisable() => gameObject.layer = _startLayer;
    }
}