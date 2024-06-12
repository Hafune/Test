using System;
using UnityEngine;

namespace Core.Lib
{
    [Serializable]
    public struct Culling2D
    {
        public Row[] rows;

        private Vector2Int _size;
        private Vector2Int _position;

        public void Initialize()
        {
            Glossary<UsageCounter> objects = new();

            foreach (var row in rows.AsSpan())
            foreach (var chunk in row.chunks)
            foreach (var go in chunk.objects.AsSpan())
                if (objects.TryGetValue(go.GetInstanceID(), out var counter))
                    chunk.counters.Add(counter);
                else
                {
                    var c = new UsageCounter { go = go };
                    objects.Add(go.GetInstanceID(), c);
                    chunk.counters.Add(c);
                }

            DisableAll();
        }

        private void DisableAll()
        {
            foreach (var row in rows.AsSpan())
            foreach (var chunk in row.chunks.AsSpan())
            foreach (var counter in chunk.counters)
            {
                counter.go.SetActive(false);
                counter.count = 0;
            }
            // foreach (var go in chunk.objects)
            //     go.SetActive(false);

            _size = Vector2Int.zero;
            _position = Vector2Int.zero;
        }

        public void MoveTo(Vector2Int position, Vector2Int size)
        {
            if (_position == position && _size == size)
                return;

            var posVert = new Vector2Int(_position.x, position.y);
            var sizeVert = new Vector2Int(_size.x, size.y);
            MoveToYPrivate(posVert, sizeVert);

            var posHor = new Vector2Int(position.x, _position.y);
            var sizeHor = new Vector2Int(size.x, _size.y);
            MoveToXPrivate(posHor, sizeHor);

            _size = size;
            _position = position;
        }

        private void MoveToYPrivate(Vector2Int position, Vector2Int size)
        {
            UpdateY(
                _position,
                _size,
                position,
                size,
                -1
            );

            UpdateY(
                position,
                size,
                _position,
                _size,
                1
            );

            _size = size;
            _position = position;
        }

        private void MoveToXPrivate(Vector2Int position, Vector2Int size)
        {
            UpdateX(
                _position,
                _size,
                position,
                size,
                -1
            );

            UpdateX(
                position,
                size,
                _position,
                _size,
                1
            );

            _size = size;
            _position = position;
        }

        private void UpdateY(
            Vector2Int pos1, Vector2Int size1,
            Vector2Int pos2, Vector2Int size2,
            int count)
        {
            var r = rows.AsSpan();
            var (rangeY1, rangeY2, _) = CalcRanges(pos1.y, size1.y, pos2.y, size2.y);
            var (_, _, centerX) = CalcRanges(pos1.x, size1.x, pos2.x, size2.x);

            ChangeRowState(
                r[CutRange(rangeY1, rows.Length)],
                centerX,
                count);

            ChangeRowState(
                r[CutRange(rangeY2, rows.Length)],
                centerX,
                count);
        }

        private void UpdateX(
            Vector2Int pos1, Vector2Int size1,
            Vector2Int pos2, Vector2Int size2,
            int count)
        {
            var r = rows.AsSpan();
            var (_, _, centerY) = CalcRanges(pos1.y, size1.y, pos2.y, size2.y);
            var (rangeX1, rangeX2, _) = CalcRanges(pos1.x, size1.x, pos2.x, size2.x);

            var centerRows = r[CutRange(centerY, rows.Length)];

            ChangeRowState(
                centerRows,
                rangeX1,
                count);

            ChangeRowState(
                centerRows,
                rangeX2,
                count);
        }

        private void ChangeRowState(Span<Row> rowsSpan, Range range, int count)
        {
            foreach (var row in rowsSpan)
                ChangeState(row.chunks, range, count);
        }

        private void ChangeState(Span<Chunk> chunks, Range range, int count)
        {
            var r = CutRange(range, chunks.Length);

            foreach (var chunk in chunks[r])
            foreach (var counter in chunk.counters)
            {
                counter.count += count;
                counter.go.SetActive(counter.count != 0);
            }
        }

        private Range CutRange(Range range, int max)
        {
            var start = Math.Min(range.Start.Value, max);
            var end = Math.Min(range.End.Value, max);
            return new Range(Math.Max(start, 0), Math.Max(end, 0));
        }

        private (Range, Range, Range) CalcRanges(int start1, int length1, int start2, int length2)
        {
            int leftStart = Math.Max(start1, 0);
            int leftEnd = Math.Max(Math.Min(start2, start1 + length1), leftStart);
            int rightStart = Math.Max(start2 + length2, leftStart);
            int rightEnd = Math.Max(rightStart, start1 + length1);

            return (
                new Range(leftStart, leftEnd),
                new Range(rightStart, rightEnd),
                new Range(leftEnd, rightStart)
            );
        }

        [Serializable]
        public struct Row
        {
            public Chunk[] chunks;
        }

        [Serializable]
        public struct Chunk
        {
            public GameObject[] objects;
            public MyList<UsageCounter> counters;
        }

        public class UsageCounter
        {
            public GameObject go;
            public int count;
        }
    }
}

/*
using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Core.Lib;
using Lib;
using Reflex;
using UnityEngine;
using VInspector;
using Voody.UniLeo.Lite;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Core
{
    public class MapBlockController : MonoConstruct
    {
        [SerializeField] public PolygonCollider2D _confiner;
        [SerializeField] public Vector3 min;
        [SerializeField] public Vector3 max;
        [SerializeField] private LayerMask _ignoreLayers;
        [SerializeField] private float _maxDistance = 30;
        [SerializeField] private float _ignoreMeshBiggerThan = 20;

        [SerializeField] private Vector2 _bottomLeft;
        [SerializeField] private Vector2Int _cellSize;
        [SerializeField] private Culling2D _culling;
        [SerializeField] private MeshRenderer[] _minimapRenderers;

        private float _viewDistanceScale = 5;
        private Vector2 _offset = Vector2.up * 12;
        private Context _context;
        private MapConstructorService _mapConstructorService;
        private Transform _cameraTransform;
        private CinemachineVirtualCamera _virtualCamera;
        private float _cameraDistance;
        private Vector2 _viewSize;

#if UNITY_EDITOR
        [Button]
        public void UpdateBounds()
        {
            min = Vector3.positiveInfinity;
            max = Vector3.negativeInfinity;

            foreach (var child in GetComponentsInChildren<MeshRenderer>().Where(i => i.enabled))
            {
                var bounds = child.bounds;
                if (bounds.size.magnitude < _ignoreMeshBiggerThan || bounds.min.z - transform.position.z > _maxDistance)
                    continue;

                min = Vector3.Min(bounds.min, min);
                max = Vector3.Max(bounds.max, max);
            }

            min -= transform.position;
            max -= transform.position;

            if (transform.localScale.x <= 0)
                return;

            min /= transform.localScale.x;
            max /= transform.localScale.x;
        }

        [Button]
        public void UpdateConfiner()
        {
            var points = new Vector2[4];
            points[0] = min;
            points[1] = min.Copy(x: max.x);
            points[2] = max;
            points[3] = max.Copy(x: min.x);

            _confiner.SetPath(0, points);
        }

        [Button]
        private void CalculateCulling()
        {
            Dictionary<Vector2Int, List<GameObject>> _cells = new();

            var rootPosition = transform.position;
            transform.position = Vector3.zero;

            var parents = new HashSet<Transform>();
            Dictionary<Renderer, Bounds> bounds = new();

            GetComponentsInChildren<MeshRenderer>()
                .Where(i =>
                {
                    var filter = i.GetComponent<MeshFilter>();
                    return filter && filter.sharedMesh is not null;
                })
                .Select(t => t as Renderer)
                .Concat(
                    GetComponentsInChildren<SkinnedMeshRenderer>()
                        .Select(t => t as Renderer)
                )
                .Where(i =>
                {
                    parents.Add(i.transform);

                    bounds.Add(i, i.bounds);
                    return i.hideFlags == HideFlags.None &&
                           _ignoreLayers != (_ignoreLayers | (1 << i.gameObject.layer)) &&
                           !i.TryGetComponent<ConvertToEntity>(out _);
                })
                .ToArray()
                .Where(i =>
                {
                    var t = i.transform.parent;
                    bool hasParent = false;
                    while (t)
                    {
                        if (parents.Contains(t))
                        {
                            var ren = t.GetComponent<Renderer>();
                            var bound = bounds[ren];
                            bound.Encapsulate(i.bounds);
                            bounds[ren] = bound;
                            hasParent = true;
                        }

                        t = t.parent;
                    }

                    return hasParent == false;
                })
                .ToArray()
                .ForEach(i =>
                {
                    var b = bounds[i];

                    int x0 = (int)Math.Floor(b.min.x / _cellSize.x);
                    int x1 = (int)Math.Ceiling(b.max.x / _cellSize.x);
                    int y0 = (int)Math.Floor(b.min.y / _cellSize.y);
                    int y1 = (int)Math.Ceiling(b.max.y / _cellSize.y);

                    for (int x = x0; x <= x1; x++)
                    for (int y = y0; y <= y1; y++)
                    {
                        var cell = new Vector2Int(
                            Mathf.FloorToInt(x),
                            Mathf.FloorToInt(y));

                        if (!_cells.TryGetValue(cell, out var objects))
                            _cells[cell] = objects = new();

                        objects.Add(i.gameObject);
                    }
                });

            var isEmpty = _cells.Keys.Count == 0;

            var left = isEmpty ? 0 : _cells.Keys.Min(i => i.x);
            var right = isEmpty ? 0 : _cells.Keys.Max(i => i.x);
            var bottom = isEmpty ? 0 : _cells.Keys.Min(i => i.y);
            var top = isEmpty ? 0 : _cells.Keys.Max(i => i.y);

            for (int x = left; x <= right; x++)
            for (int y = bottom; y <= top; y++)
                if (!_cells.ContainsKey(new Vector2Int(x, y)))
                    _cells[new Vector2Int(x, y)] = new List<GameObject>();

            _bottomLeft = new Vector2(left, bottom);

            _culling.rows = new Culling2D.Row[top - bottom];
            var rows = _culling.rows;

            for (int y = 0, iMax = rows.Length; y < iMax; y++)
            {
                var chunks = _cells
                    .Where(v => v.Key.y - bottom == y)
                    .OrderBy(v => v.Key.x)
                    .Select(v => v.Value.ToArray())
                    .ToArray();

                rows[y] = new Culling2D.Row();
                ref var row = ref rows[y];
                row.chunks = new Culling2D.Chunk[chunks.Length];

                for (int x = 0, xMax = row.chunks.Length; x < xMax; x++)
                    row.chunks[x].objects = chunks[x];
            }

            transform.position = rootPosition;

            var renderers = GetComponentsInChildren<MeshRenderer>();
            _minimapRenderers = renderers
                .Where(i => i.enabled && _ignoreLayers == (_ignoreLayers | (1 << i.gameObject.layer)))
                .ToArray();

            EditorUtility.SetDirty(this);
        }
#endif
        protected override void Construct(Context context) => _context = context;

        private void Awake()
        {
            var circle = gameObject.AddComponent<CircleCollider2D>();
            circle.isTrigger = true;
            var offset = (max + min) / 2;
            circle.offset = offset;
            var distance = (Vector2)(max - min);
            circle.radius = distance.magnitude / 2;

            _mapConstructorService = _context.Resolve<MapConstructorService>();
            _virtualCamera = _context.Resolve<CinemachineVirtualCamera>();
            _cameraTransform = _virtualCamera.transform;
            _cameraDistance = _virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance;
            _viewSize = _cameraDistance * _viewDistanceScale * new Vector2(.6f, 1f);
            _culling.Initialize();

            _mapConstructorService.OnMinimapEnableSignal += EnableMinimapRenderers;
            _mapConstructorService.OnMinimapDisableSignal += DisableMinimapRenderers;

            if (!_mapConstructorService.MinimapRenderersShouldBeEnable)
                DisableMinimapRenderers();
        }

        private void OnDestroy()
        {
            _mapConstructorService.OnMinimapEnableSignal -= EnableMinimapRenderers;
            _mapConstructorService.OnMinimapDisableSignal -= DisableMinimapRenderers;
        }

        private void EnableMinimapRenderers()
        {
            foreach (var meshRenderer in _minimapRenderers)
                meshRenderer.enabled = true;
        }

        private void DisableMinimapRenderers()
        {
            foreach (var meshRenderer in _minimapRenderers)
                meshRenderer.enabled = false;
        }

        private void FixedUpdate()
        {
            var p = (Vector2)(_cameraTransform.position - transform.position) + _offset;
            var size = new Vector2(_viewSize.x, _viewSize.y) / _cellSize;
            var sizeInt = new Vector2Int((int)size.x, (int)size.y);
            p -= _viewSize / 2;
            p += _cellSize / 2;
            p /= _cellSize;
            p -= _bottomLeft;

            _culling.MoveTo(new Vector2Int((int)Math.Round(p.x), (int)Math.Round(p.y)), sizeInt);
        }
    }
}
*/