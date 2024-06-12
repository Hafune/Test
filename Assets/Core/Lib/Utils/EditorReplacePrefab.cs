using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core.Lib
{
    [SelectionBase]
    public class EditorReplacePrefab : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
#if UNITY_EDITOR
        private bool _wasReplaced;

        private void OnValidate()
        {
            if (PrefabUtility.IsPartOfPrefabAsset(gameObject) || Application.isPlaying || _wasReplaced)
                return;

            EditorApplication.delayCall += Call;
        }

        private void Call()
        {
            if (!_prefab)
                return;

            var objectTransform = transform;
            DestroyImmediate(this);
            var pref = Replace(objectTransform, _prefab, true);
            _wasReplaced = true;
            Selection.objects = Selection.objects.Concat(new[] { pref.gameObject }).ToArray();
            EditorUtility.SetDirty(pref);
        }

        public static GameObject Replace(Transform transform, GameObject _prefab, bool writeUndo)
        {
            var pref = (GameObject)PrefabUtility.InstantiatePrefab(_prefab, transform.parent);
            pref.transform.position = transform.position;
            pref.transform.rotation = transform.rotation;
            pref.transform.localScale = transform.localScale;

            int index = transform.GetSiblingIndex();
            pref.transform.SetSiblingIndex(index);

            if (writeUndo)
            {
                Undo.DestroyObjectImmediate(transform.gameObject);
                Undo.RegisterCreatedObjectUndo(pref, "before replacing by EditorPrefabReplace");
            }
            else
            {
                DestroyImmediate(transform.gameObject);
            }

            EditorUtility.SetDirty(pref.transform.parent.gameObject);

            return pref;
        }
#endif
    }
}