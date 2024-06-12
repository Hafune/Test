using System;
using System.Collections.Generic;
using System.Linq;
using Lib;
using UnityEngine;
using VInspector;

namespace Core
{
    public class MeshRenderersController : MonoBehaviour
    {
        [SerializeField] private MeshMatrices[] _matrices;
        [SerializeField] private GameObject _root;
        private Dictionary<Mesh, MatrixContainer> _containers = new();

        [Button]
        [ButtonSize(22)]
        private void Bake()
        {
            foreach (var t in transform.GetSelfChildrenTransforms())
                Destroy(t.gameObject);

            _containers.Clear();

            var meshRenderers = _root
                .GetComponentsInChildren<MeshRenderer>()
                .Where(r => r.enabled)
                .ToArray();

            foreach (var meshRenderer in meshRenderers)
            {
                var filter = meshRenderer.GetComponent<MeshFilter>();

                if (!_containers.ContainsKey(filter.sharedMesh))
                {
                    var c = new MatrixContainer();
                    c.meshRenderer = meshRenderer;
                    c.matrix.Add(filter.transform.localToWorldMatrix);
                    _containers.Add(filter.sharedMesh, c);
                }
                else
                {
                    _containers[filter.sharedMesh].matrix.Add(filter.transform.localToWorldMatrix);
                }
            }

            _matrices = new MeshMatrices[_containers.Count];
            int index = 0;
            foreach (var (mesh, container) in _containers)
            {
                var instanceRenderer = new MeshMatrices();
                instanceRenderer.Bake(mesh, container.meshRenderer, container.matrix);
                _matrices[index++] = instanceRenderer;
            }

            _root.SetActive(false);
            enabled = true;
        }

        public void Update()
        {
            Span<MeshMatrices> span = _matrices;
            for (int i = 0, iMax = span.Length; i < iMax; i++)
                span[i].Update();
        }

        private class MatrixContainer
        {
            public MeshRenderer meshRenderer;
            public List<Matrix4x4> matrix = new();
        }
    }
}