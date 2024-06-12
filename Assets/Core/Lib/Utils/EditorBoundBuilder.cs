using UnityEngine;
#if UNITY_EDITOR
#endif

namespace Core.Lib
{
    [ExecuteInEditMode, SelectionBase, RequireComponent(typeof(BoxCollider))]
    public class EditorBoundBuilder : MonoBehaviour
    {
        [SerializeField] private Vector3 _size;
        [SerializeField] private float _distance;
        [SerializeField] private bool _refresh;

        private float _lastDistance;
        private Vector3 _lastSize;

        private void Update()
        {
            if (Application.isPlaying)
            {
                enabled = false;
                return;
            }

#if UNITY_EDITOR
            if (!_refresh && _lastDistance == _distance && _lastSize == _size)
                return;

            _refresh = false;
            _lastDistance = _distance;
            _lastSize = _size;

            var col = GetComponent<BoxCollider>();
            col.size = _size;
            col.center = new Vector3(0, 0, _size.z / 2 + _distance);
#endif
        }
    }
}