using System;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

namespace Core
{
    public class MeshInstanceRenderer : MonoBehaviour
    {
        [SerializeField] private MatrixContainer[] _matrixContainers;
        [SerializeField] private Mesh _mesh;
        [SerializeField] private Material _material;
        private RenderParams _params;
        private int _maxSize = 1023;
        private List<Matrix4x4> _matrix;

        internal void Bake(Mesh mesh, MeshRenderer meshRenderer, List<Matrix4x4> matrix)
        {
            _matrix = matrix;
            _mesh = mesh;
            _material = meshRenderer.sharedMaterial;
            _params = new(_material);
            _params.receiveShadows = meshRenderer.receiveShadows;
            _params.shadowCastingMode = meshRenderer.shadowCastingMode;
            _params.layer = meshRenderer.transform.gameObject.layer;
            Bake();
        }

        [Button]
        private void Bake()
        {
            float total = _matrix.Count;
            _matrixContainers = new MatrixContainer[(int)Math.Ceiling(total / _maxSize)];

            int index = 0;
            for (int i = 0; i < total; i += _maxSize)
                _matrixContainers[index++].matrix = new Matrix4x4[(int)Math.Min(_maxSize, total - i)];

            int rootIndex = 0;
            int childIndex = 0;
            for (int i = 0; i < total; i++)
            {
                _matrixContainers[rootIndex].matrix[childIndex++] = _matrix[i];

                if (childIndex != _matrixContainers[rootIndex].matrix.Length)
                    continue;

                childIndex = 0;
                rootIndex++;
            }
        }

        private void Update()
        {
            // for (int i = 0, iMax = _matrixContainers.Length; i < iMax; i++)
            // {
            //     Graphics.RenderMeshInstanced(
            //         in _params,
            //         _mesh,
            //         0,
            //         _matrixContainers[i].matrix
            //     );
            //     // Graphics.DrawMeshInstanced(
            //     //     _mesh,
            //     //     0,
            //     //     _material,
            //     //     _matrixContainers[i].matrix
            //     // );
            // }
            foreach (var container in _matrixContainers)
            foreach (var matrix4X4 in container.matrix)
            {
                Graphics.RenderMesh(
                    in _params,
                    _mesh,
                    0,
                    matrix4X4
                );
            }
        }

        [Serializable]
        private struct MatrixContainer
        {
            public Matrix4x4[] matrix;
        }
    }
}