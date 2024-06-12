using System;
using System.Collections.Generic;
using UnityEngine;
using VInspector;

namespace Core
{
    [Serializable]
    public struct MeshMatrices
    {
        [SerializeField] private Matrix4x4[] _matrixContainers;
        [SerializeField] private Mesh _mesh;
        [SerializeField] private Material _material;
        private RenderParams _params;

        internal void Bake(Mesh mesh, MeshRenderer meshRenderer, List<Matrix4x4> matrices)
        {
            _mesh = mesh;
            _material = meshRenderer.sharedMaterial;
            _params = new(_material);
            _params.receiveShadows = meshRenderer.receiveShadows;
            _params.shadowCastingMode = meshRenderer.shadowCastingMode;
            _params.layer = meshRenderer.transform.gameObject.layer;
            _matrixContainers = matrices.ToArray();
        }

        internal void Update()
        {
            Span<Matrix4x4> span = _matrixContainers;
            for (int i = 0, iMax = span.Length; i < iMax; i++)
            {
                Graphics.RenderMesh(
                    in _params,
                    _mesh,
                    0,
                    span[i]
                );
            }
        }
    }
}