using System.Collections.Generic;
using System.Linq;
using Lib;
using UnityEngine;
using VInspector;

namespace Core
{
    public class MeshInstanceRenderersController : MonoBehaviour
    {
        [SerializeField] private MeshInstanceRenderer _prefab;
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

            foreach (var (mesh, container) in _containers)
            {
                var instanceRenderer = Instantiate(_prefab, Vector3.zero, Quaternion.identity, transform);
                instanceRenderer.Bake(mesh, container.meshRenderer, container.matrix);
                instanceRenderer.enabled = true;
            }

            _root.SetActive(false);
        }

        private class MatrixContainer
        {
            public MeshRenderer meshRenderer;
            public List<Matrix4x4> matrix = new();
        }
    }
}