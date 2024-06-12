using System;
using Lib;
using UnityEngine;
using UnityEngine.Rendering;
using VInspector;

namespace Core
{
    public class TestDrawMesh : MonoBehaviour
    {
        [SerializeField] private Vector3 _size;
        [SerializeField] private GameObject _prefab;

        private Mesh _mesh;
        private Material _material;
        private RenderParams _params;
        private Matrix4x4[][] _matrix;

        [Button]
        private void Refresh()
        {
            foreach (var t in transform.GetSelfChildrenTransforms())
                Destroy(t.gameObject);

            Start();
        }

        [Button]
        private void Toggle()
        {
            enabled = !enabled;

            if (!enabled)
                return;

            foreach (var t in transform.GetSelfChildrenTransforms())
                Destroy(t.gameObject);
        }

        private void Awake()
        {
            _mesh = _prefab.GetComponent<MeshFilter>().mesh;
            _material = _prefab.GetComponent<MeshRenderer>().sharedMaterial;
            _params = new(_material);
            _params.receiveShadows = true;
            _params.shadowCastingMode = ShadowCastingMode.On;
        }

        private void Start()
        {
            int total = (int)(_size.x * _size.y * _size.z);
            int index = 0;
            _matrix = new Matrix4x4[(int)Math.Ceiling(total / 1024f)][];

            for (int i = 0; i < total; i += 1023)
                _matrix[index++] = new Matrix4x4[Math.Min(1023, total - i)];

            int rootIndex = 0;
            int childIndex = 0;
            for (int x = 0; x < _size.x; x++)
            for (int y = 0; y < _size.y; y++)
            for (int z = 0; z < _size.z; z++)
            {
                var o = Instantiate(_prefab, new Vector3(x, y, z), Quaternion.identity, transform);
                _matrix[rootIndex][childIndex++] = o.transform.localToWorldMatrix;

                if (childIndex != _matrix[rootIndex].Length)
                    continue;

                childIndex = 0;
                rootIndex++;
            }

            enabled = false;
        }

        private void Update()
        {
            for (int i = 0, iMax = _matrix.Length; i < iMax; i++)
            {
                Graphics.RenderMeshInstanced(
                    in _params,
                    _mesh,
                    0,
                    _matrix[i]
                );
            }
        }
    }
}