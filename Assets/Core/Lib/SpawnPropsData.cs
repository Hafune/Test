using System;
using System.Linq;
using Lib;
using Unity.VisualScripting;
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.AddressableAssets;
using VInspector;

namespace Core.Lib
{
    [ExecuteInEditMode]
    public class SpawnPropsData : MonoBehaviour
    {
        [SerializeField] private float[] _values;
        [SerializeField] private GameObject[] _prefabs;
        [SerializeField] private int[] _prefabUsageCounts;

#if UNITY_EDITOR
        private string _dir = "Assets/Prefabs/Level/MeshPrefabs";
        private readonly Glossary<MeshFilter> MeshPrefabs = new();
        [SerializeField] public AssetReference Art;
        private GameObject _prefabView;
        private Transform _props;

        [Button]
        private void Refresh()
        {
            if (this.IsDestroyed() || !enabled)
                return;

            OnDisable();
            OnEnable();
        }

        private void OnEnable()
        {
            if (Application.isPlaying)
                return;

            if (PrefabUtility.IsPartOfPrefabAsset(gameObject) || string.IsNullOrEmpty(Art?.AssetGUID))
                return;

            _props ??= AssetDatabase.LoadAssetAtPath<Transform>(AssetDatabase.GUIDToAssetPath(Art.AssetGUID));

            if (this.IsDestroyed() || !_props)
                return;

            _prefabView = SpawnPrefabTask.InstantiatePrefabDontSave(_props, transform);
        }

        private void OnDisable()
        {
            if (Application.isPlaying)
                return;

            if (_prefabView)
                DestroyImmediate(_prefabView);

            transform.DestroyChildren();

            _prefabView = null;
        }

        [Button]
        public void SerializeProps()
        {
            MeshPrefabs.Clear();

            if (!_props)
                _props = AssetDatabase.LoadAssetAtPath<Transform>(AssetDatabase.GUIDToAssetPath(Art.AssetGUID));

            var guids = AssetDatabase.FindAssets("t:prefab", new[] { _dir });

            foreach (var guid in guids)
            {
                var value = AssetDatabase.LoadAssetAtPath<MeshFilter>(AssetDatabase.GUIDToAssetPath(guid));

                if (!value)
                    continue;

                MeshPrefabs.Add(value.sharedMesh.GetInstanceID(), value);
            }
            
            if (transform.childCount != 1)
            {
                Debug.LogWarning(nameof(SpawnPropsData)+ " childCount != 1");
                Refresh();
                
                if (transform.childCount != 1)
                    throw new Exception(nameof(SpawnPropsData)+ " childCount != 1");
            }

            foreach (Transform child in transform)
                PrefabUtility.UnpackPrefabInstance(child.gameObject, PrefabUnpackMode.OutermostRoot,
                    InteractionMode.UserAction);

            var filters = GetComponentsInChildren<MeshFilter>()
                .Where(f => f.sharedMesh && !PrefabUtility.IsPartOfAnyPrefab(f.gameObject))
                .OrderBy(f => f.sharedMesh.GetInstanceID()).ToArray();

            List<int> prefabUsageCount = new();
            List<float> values = new();
            List<GameObject> prefabs = new();
            var go = new GameObject();
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;

            int lastMeshId = 0;
            int index = -1;
            bool isFirst = true;
            foreach (var filter in filters)
            {
                filter.transform.parent = transform;
                var pos = filter.transform.localPosition;
                var rot = filter.transform.localRotation;
                var scale = filter.transform.localScale;

                values.Add(pos.x);
                values.Add(pos.y);
                values.Add(pos.z);
                values.Add(rot.x);
                values.Add(rot.y);
                values.Add(rot.z);
                values.Add(rot.w);
                values.Add(scale.x);
                values.Add(scale.y);
                values.Add(scale.z);

                if (isFirst || lastMeshId != filter.sharedMesh.GetInstanceID())
                {
                    isFirst = false;
                    lastMeshId = filter.sharedMesh.GetInstanceID();
                    prefabUsageCount.Add(0);
                    var prefab = MeshPrefabs.GetValue(filter.sharedMesh.GetInstanceID()).gameObject;
                    prefabs.Add(prefab);
                    index++;
                }

                prefabUsageCount[index]++;
            }

            var gameObjects = GetComponentsInChildren<Transform>()
                .Where(f => PrefabUtility.IsAnyPrefabInstanceRoot(f.gameObject))
                .Select(f => f.gameObject)
                .OrderBy(f => f.GetInstanceID()).ToArray();

            GameObject lastPrefab = null;
            foreach (var prefab in gameObjects)
            {
                prefab.transform.parent = transform;
                var pos = prefab.transform.localPosition;
                var rot = prefab.transform.localRotation;
                var scale = prefab.transform.localScale;

                values.Add(pos.x);
                values.Add(pos.y);
                values.Add(pos.z);
                values.Add(rot.x);
                values.Add(rot.y);
                values.Add(rot.z);
                values.Add(rot.w);
                values.Add(scale.x);
                values.Add(scale.y);
                values.Add(scale.z);

                var prefabOriginal = PrefabUtility.GetCorrespondingObjectFromOriginalSource(prefab);

                if (lastPrefab != prefabOriginal)
                {
                    lastPrefab = prefabOriginal;
                    prefabUsageCount.Add(0);
                    prefabs.Add(prefabOriginal);
                    index++;
                }

                prefabUsageCount[index]++;
            }

            DestroyImmediate(go);
            _values = values.Select(v => v == -0 ? 0 : v).ToArray();
            _prefabs = prefabs.ToArray();
            _prefabUsageCounts = prefabUsageCount.ToArray();
            Refresh();
        }

        [Button]
        private void Clear() => transform.DestroyChildren(true);
#endif

        public void Start()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
#endif
            Spawn();
        }

        [Button]
        private void Spawn()
        {
            int index = 0;
            int prefabIndex = 0;
            for (int i = 0, iMax = _values.Length; i < iMax; i += 10)
            {
                var pos = new Vector3(
                    _values[i + 0],
                    _values[i + 1],
                    _values[i + 2]);
                var rot = new Quaternion(
                    _values[i + 3],
                    _values[i + 4],
                    _values[i + 5],
                    _values[i + 6]);
                var scale = new Vector3(
                    _values[i + 7],
                    _values[i + 8],
                    _values[i + 9]);

                var go = Instantiate(_prefabs[prefabIndex], transform);
                go.transform.SetLocalPositionAndRotation(pos, rot);
                go.transform.localScale = scale;
                go.hideFlags = HideFlags.DontSave;

                index++;
                if (index >= _prefabUsageCounts[prefabIndex])
                {
                    index = 0;
                    prefabIndex++;
                }
            }
        }
    }
}