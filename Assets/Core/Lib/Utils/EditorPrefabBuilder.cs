using System.Collections.Generic;
using Lib;
using UnityEngine;
#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
#endif

namespace Core.Lib
{
    [SelectionBase]
    public class EditorPrefabBuilder : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _prefabs;
        [SerializeField] private Vector3Int _size;
        [SerializeField] private Vector3 _step = Vector3.one;   
        [SerializeField] private bool _refresh;
        [SerializeField] private BoxCollider _box;
        [SerializeField] private Vector3 _centerOffset;

        private Vector3Int _lastSize;
        private Vector3 _lastStep;
        private bool _justAdded = true;
#if UNITY_EDITOR

        private void OnValidate()
        {
            if (gameObject.scene.name is null)
                return;

            EditorApplication.delayCall += Call;
        }

        private void Call()
        {
            if (_lastSize == _size && _lastStep == _step && !_refresh)
                return;

            _lastSize = _size;
            _lastStep = _step;
            _refresh = false;

            if (_justAdded)
            {
                _justAdded = false;
                return;
            }

            transform.DetachChildren();

            var position = transform.position;
            var rotation = transform.rotation;
            var scale = transform.localScale;

            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;

            var minX = Math.Min(0, _size.x);
            var maxX = Math.Max(0, _size.x);
            var minY = Math.Min(0, _size.y);
            var maxY = Math.Max(0, _size.y);
            var minZ = Math.Min(0, _size.z);
            var maxZ = Math.Max(0, _size.z);

            var center = Vector3.zero;
            int count = 0;

            for (int x = minX; x < maxX; x++)
            for (int y = minY; y < maxY; y++)
            for (int z = minZ; z < maxZ; z++)
            {
                int index = x + y + z;
                var cen = new Vector3(x * _step.x, y * _step.y, z * _step.z);
                center += cen;
                count++;
                var totalPosition = position + cen;
                var item = _prefabs[index % _prefabs.Count];

                if (ReferenceEquals(item, null))
                    continue;

                var pref = (GameObject)PrefabUtility.InstantiatePrefab(item, transform);

                try
                {
                    pref ??= Instantiate(item, transform);
                    pref.transform.position = totalPosition;

                    if (_box != null && pref.TryGetComponent<Collider>(out var c))
                    {
                        DestroyImmediate(c);
                        EditorUtility.SetDirty(pref);
                    }
                }
                catch (Exception e)
                {
                    Debug.LogWarning(e);
                }
            }

            transform.rotation = rotation;
            transform.localScale = scale;

            if (_box == null)
                return;

            var total = new Vector3(maxX - minX, maxY - minY, maxZ - minZ).Multiply(_step);
            _box.center = center / count + _centerOffset;
            _box.size = total;
        }
#endif
    }
}