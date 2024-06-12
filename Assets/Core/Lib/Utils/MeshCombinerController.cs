using System.Collections.Generic;
using System.Linq;
using Lib;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.U2D;

namespace Core.Lib
{
    public class MeshCombinerController : MonoBehaviour
    {
        [SerializeField] private MeshCombinerForGroups _template;
        [SerializeField] private GameObject _root;
        [SerializeField] private Vector3 _chunkSize;
#if UNITY_EDITOR
        [SerializeField] private bool _refresh;
        [SerializeField] private List<Material> _materialsWithShadows;

        private Dictionary<Vector3Int, List<MeshRenderer>> _chunkMeshes = new();
        private List<Dictionary<int, List<GameObject>>> _chunkObjects = new();
        private Dictionary<int, List<GameObject>> _materialObjects = new();
        private Dictionary<int, int> _materialVertexes = new();

        private void OnValidate()
        {
            if (gameObject.scene.name is null)
                return;

            EditorApplication.delayCall += Call;
        }

        private void Call()
        {
            if (!_refresh)
                return;

            var materialsWithShadowsIds = _materialsWithShadows.Select(i => i.GetInstanceID());
            _refresh = false;
            _chunkMeshes.Clear();
            _materialObjects.Clear();
            _materialVertexes.Clear();
            _chunkObjects.Clear();

            var arr = new List<GameObject>(transform.childCount);
            foreach (Transform child in transform)
                arr.Add(child.gameObject);

            foreach (var ga in arr)
                DestroyImmediate(ga);

            _root.GetComponentsInChildren<MeshRenderer>().ForEach(i =>
            {
                // if (i.shadowCastingMode != ShadowCastingMode.On)
                //     return;
                
                var filter = i.GetComponent<MeshFilter>();

                if (!i.enabled || !filter || filter.sharedMesh is null)
                    return;

                var center = i.bounds.center;
                center.z += _chunkSize.z / 2;
                var rCell = center.Divide(_chunkSize);
                var cell = new Vector3Int(
                    Mathf.FloorToInt(rCell.x),
                    Mathf.FloorToInt(rCell.y),
                    Mathf.FloorToInt(rCell.z));

                if (!_chunkMeshes.ContainsKey(cell))
                    _chunkMeshes[cell] = new();

                _chunkMeshes[cell].Add(i);
            });

            foreach (var chunk in _chunkMeshes.Values)
            {
                _materialObjects.Clear();
                _materialVertexes.Clear();
                _chunkObjects.Add(new());
                var _chunkPositionMap = _chunkObjects[^1];
                foreach (var i in chunk)
                {
                    var filter = i.GetComponent<MeshFilter>();

                    int id = i.sharedMaterial.GetInstanceID();
                    
                    if (!_chunkPositionMap.ContainsKey(id))
                        _chunkPositionMap[id] = new();

                    if (!_materialObjects.ContainsKey(id))
                    {
                        _materialObjects[id] = _chunkPositionMap[id];
                        _materialVertexes[id] = default;
                    }

                    int vertexCount = filter.sharedMesh.vertexCount;

                    if (vertexCount + _materialVertexes[id] < 65000)
                    {
                        _materialObjects[id].Add(i.gameObject);
                        _materialVertexes[id] += vertexCount;
                    }
                    else
                    {
                        _materialObjects.Remove(id);
                        _materialVertexes.Remove(id);
                        _chunkPositionMap[id].Add(i.gameObject);
                    }
                }
            }

            arr.Clear();
            foreach (var _chunkPositionMap in _chunkObjects)
                _chunkPositionMap.Values.ForEach(list =>
                {
                    if (list.Count == 0)
                        return;

                    var group = Instantiate(_template);
                    group.Group.Add(group.gameObject);
                    group.Group.AddRange(list);
                    var meshRenderer = list[0].GetComponent<MeshRenderer>();
                    group.gameObject.name = meshRenderer.sharedMaterial.name;
                    arr.Add(group.gameObject);

                    if (!materialsWithShadowsIds.Contains(meshRenderer.sharedMaterial.GetInstanceID()))
                        group.GetComponent<MeshRenderer>().shadowCastingMode = ShadowCastingMode.Off;
                });

            foreach (var child in arr.OrderBy(i => i.name))
                child.transform.SetParent(transform, false);

            transform.position = _root.transform.position;
            transform.rotation = _root.transform.rotation;
            transform.localScale = _root.transform.lossyScale;

            EditorUtility.SetDirty(this);
        }
#endif

        public void Awake()
        {
            GetComponentsInChildren<MeshCombinerForGroups>().ForEach(i => i.CombineMeshes(_root.transform, true));
        }
    }
}