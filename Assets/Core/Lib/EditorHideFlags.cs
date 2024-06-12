#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using VInspector;

namespace Core.Lib
{
    public class EditorHideFlags : MonoBehaviour
    {
        [SerializeField, ReadOnly] private HideFlags _currentHideFlags;
        [SerializeField] private HideFlags _hideFlags;

        private void OnValidate() => _currentHideFlags = gameObject.hideFlags;

        [Button]
        private void Change()
        {
            gameObject.hideFlags = _hideFlags;
            EditorUtility.SetDirty(gameObject);
        }
    }
}
#endif