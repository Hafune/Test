using System.Collections.Generic;
using System.Linq;
using Lib;
using UnityEngine;
using VInspector;

namespace Core
{
    public class MeshInstanceRenderersChunkController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private MeshInstanceRenderer _prefab;
        [SerializeField] private GameObject _root;
        [SerializeField] private Vector2 _viewportSize = new(1, 1);
        [SerializeField] private Vector2 _chunkSize = new(8, 5);

        private Dictionary<Vector2Int, Dictionary<Mesh, MatrixContainer>> _chunks = new();
        private Dictionary<Vector2Int, List<MeshInstanceRenderer>> _cells = new();

        [Button]
        [ButtonSize(22)]
        private void Bake()
        {
            foreach (var t in transform.GetSelfChildrenTransforms())
                Destroy(t.gameObject);

            _chunks.Clear();
            _cells.Clear();

            var meshRenderers = _root
                .GetComponentsInChildren<MeshRenderer>()
                .Where(r => r.enabled)
                // .Where(r => r.sharedMaterial.enableInstancing)
                .ToArray();

            foreach (var meshRenderer in meshRenderers)
            {
                var chunkPosition = CalculateChunkPosition(meshRenderer.transform.position);

                if (_chunks.ContainsKey(chunkPosition))
                    continue;

                _chunks.Add(chunkPosition, new Dictionary<Mesh, MatrixContainer>());
                _cells.Add(chunkPosition, new());
            }

            foreach (var meshRenderer in meshRenderers)
            {
                var filter = meshRenderer.GetComponent<MeshFilter>();
                var _containers = _chunks[CalculateChunkPosition(meshRenderer.transform.position)];

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

            foreach (var (pos, _containers) in _chunks)
            foreach (var (mesh, container) in _containers)
            {
                var instanceRenderer = Instantiate(_prefab, Vector3.zero, Quaternion.identity, transform);
                instanceRenderer.Bake(mesh, container.meshRenderer, container.matrix);
                instanceRenderer.enabled = true;
                instanceRenderer.gameObject.SetActive(false);

                _cells[pos].Add(instanceRenderer);
            }

            _root.SetActive(false);
            Debug.Log("================");
        }

        [Button]
        [ButtonSize(22)]
        private void UpdateVisibility()
        {
            foreach (var cell in _cells.Values)
            foreach (var instanceRenderer in cell)
                instanceRenderer.gameObject.SetActive(false);

            var min = CalculateChunkPosition(_camera.transform.position - (Vector3)(_chunkSize * _viewportSize / 2));
            var max = CalculateChunkPosition(_camera.transform.position + (Vector3)(_chunkSize * _viewportSize / 2));

            for (int x = min.x; x <= max.x; x++)
            for (int y = min.y; y <= max.y; y++)
            {
                if (!_cells.TryGetValue(new Vector2Int(x, y), out var c))
                    continue;

                foreach (var instanceRenderer in c)
                    instanceRenderer.gameObject.SetActive(true);
            }
        }

        private Vector2Int CalculateChunkPosition(Vector3 worldPosition) => new((int)(worldPosition.x / _chunkSize.x),
            (int)(worldPosition.y / _chunkSize.y));

        private class MatrixContainer
        {
            public MeshRenderer meshRenderer;
            public List<Matrix4x4> matrix = new();
        }
    }
}