using System;
using JetBrains.Annotations;
using Lib;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core
{
    [Serializable]
    [CreateAssetMenu(menuName = "Game Config/" + nameof(ReferencePath))]
    public class ReferencePath : ScriptableObject
    {
        [field: SerializeField] public string Path { get; private set; } = "";

        [CanBeNull]
        public GameObject Find(Transform transform) => transform.Find(Path)?.gameObject;

        public static implicit operator string(ReferencePath reference) => reference.Path;

#if UNITY_EDITOR
        [SerializeField] private GameObject _prefab;
        [SerializeField, ReadOnly] private GameObject reference;
        [SerializeField, HideInInspector] private bool _hasReference;

        public void SetPrefabPart(GameObject prefab, string path)
        {
            _prefab = prefab;
            Path = path;

            Refresh(_prefab, false);
        }

        private void OnEnable() => PathReferencePrefabPostprocessor.callback += RefreshPost;

        private void OnDisable() => PathReferencePrefabPostprocessor.callback -= RefreshPost;

        private void OnValidate()
        {
            Refresh(_prefab, true);

            if (reference is not null && !reference)
                Debug.LogError("Потеряна ссылка на объект: " + Path, this);

            if (reference is null && !string.IsNullOrEmpty(Path))
                Debug.LogWarning("референс имеет путь но не имеет объекта: " + Path, this);
        }

        private void DelayedRename()
        {
            var newName = "Path_" + reference.name;
            var assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
            AssetDatabase.RenameAsset(assetPath, newName);
            AssetDatabase.SaveAssetIfDirty(this);
        }

        private void RefreshPost(GameObject go) => Refresh(go, false);

        private void Refresh(GameObject go, bool findNearest)
        {
            if (_prefab && go && _prefab != go)
                return;

            Path = Path.Trim();
            GameObject obj = null;

            if (go)
                obj = go.transform.Find(Path)?.gameObject;

            if (!go)
                reference = null;

            if (obj && obj != reference)
                reference = null;

            HierarchyChanged(findNearest);

            if (reference is null || reference.IsDestroyed())
                return;

            var newName = "Path_" + reference.name;

            if (this.IsDestroyed() || name == newName)
                return;

            EditorApplication.delayCall += DelayedRename;
        }

        private void HierarchyChanged(bool findNearest)
        {
            if (!_prefab)
                return;

            if (!reference)
            {
                reference = _prefab.transform.Find(Path)?.gameObject;
                _hasReference = _hasReference || reference;
            }
            else if (!_prefab.transform.Find(Path))
            {
                var nearest = findNearest ? _prefab.transform.FindRecursiveFirst(Path)?.gameObject : null;

                if (nearest)
                    reference = nearest;

                Path = reference.GetPath();
            }

            if (reference is not null || !_hasReference)
                return;

            Debug.LogError($"{nameof(ReferencePath)} Path:\"{Path}\" потеряна", this);
            _hasReference = false;
        }
#endif
    }
}