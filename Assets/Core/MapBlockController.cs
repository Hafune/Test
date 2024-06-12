using System.Collections;
using System.Linq;
using JetBrains.Annotations;
using Lib;
using Reflex;
using UnityEngine;
using VInspector;

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
        private MeshRenderer[] _meshRenderers;
        private MeshRenderer[] _minimapRenderers;
        private Light[] _lights;
        private ParticleSystem[] _particles;
        private Waterfall[] _waterfalls;
        private SkinnedMeshRenderer[] _skinnedRenderers;

        private bool _hasContact;
        [CanBeNull] private Coroutine _enableCoroutine;
        [CanBeNull] private Coroutine _disableCoroutine;

        private float _duration = 1.5f;
        private float _maxPerStep;
        private int _totalElements;
        private Context _context;
        private MapConstructorService _mapConstructorService;
        private bool _wasStarted;

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
#endif
        protected override void Construct(Context context) => _context = context;

        private IEnumerator Start()
        {
            yield return null;
            
            var circle = gameObject.AddComponent<CircleCollider2D>();
            circle.isTrigger = true;
            var offset = (max + min) / 2;
            circle.offset = offset;
            var distance = (Vector2)(max - min);
            circle.radius = distance.magnitude / 2;

            _mapConstructorService = _context.Resolve<MapConstructorService>();
            Initialize();
        }

        private void OnDestroy()
        {
            _mapConstructorService.OnMinimapEnable -= EnableMinimapRenderers;
            _mapConstructorService.OnMinimapDisable -= DisableMinimapRenderers;
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

        private void Initialize()
        {
            _mapConstructorService.OnMinimapEnable += EnableMinimapRenderers;
            _mapConstructorService.OnMinimapDisable += DisableMinimapRenderers;

            _wasStarted = true;
            var renderers = GetComponentsInChildren<MeshRenderer>();

            _meshRenderers = renderers
                .Where(i => i.enabled && _ignoreLayers != (_ignoreLayers | (1 << i.gameObject.layer)))
                .ToArray();

            _minimapRenderers = renderers
                .Where(i => i.enabled && _ignoreLayers == (_ignoreLayers | (1 << i.gameObject.layer)))
                .ToArray();

            _skinnedRenderers = GetComponentsInChildren<SkinnedMeshRenderer>().Where(i => i.enabled).ToArray();
            _lights = GetComponentsInChildren<Light>().Where(i => i.enabled).ToArray();
            _particles = GetComponentsInChildren<ParticleSystem>().ToArray();
            _waterfalls = GetComponentsInChildren<Waterfall>().ToArray();

            _totalElements = _meshRenderers.Length
                             + _lights.Length
                             + _particles.Length
                             + _waterfalls.Length
                             + _skinnedRenderers.Length;
            _maxPerStep = _totalElements / _duration;

            if (!_mapConstructorService.MinimapRenderersShouldBeEnable)
                DisableMinimapRenderers();

            if (!_hasContact)
                OnTriggerExit2D(null);
        }

        private void OnTriggerEnter2D(Collider2D _)
        {
            _hasContact = true;

            if (!_wasStarted)
                return;

            if (_disableCoroutine is not null)
            {
                StopCoroutine(_disableCoroutine);
                _disableCoroutine = null;
            }

            if (_enableCoroutine is not null)
                return;

            // _enableCoroutine = StartCoroutine(SetActiveLocation(true));
            SetActiveLocation2(true);
        }

        private void OnTriggerExit2D(Collider2D _)
        {
            _hasContact = false;

            if (!_wasStarted)
                return;

            if (_enableCoroutine is not null)
            {
                StopCoroutine(_enableCoroutine);
                _enableCoroutine = null;
            }

            if (_disableCoroutine is not null)
                return;

            if (gameObject.activeInHierarchy)
                SetActiveLocation2(false);
            // _disableCoroutine = StartCoroutine(SetActiveLocation(false));
        }

        private IEnumerator SetActiveLocation(bool enable)
        {
            int count = 0;
            float maxPerStep = enable ? _maxPerStep : _totalElements + 1;

            foreach (var meshRenderer in _meshRenderers)
            {
                meshRenderer.enabled = enable;
                if (count++ > maxPerStep)
                {
                    count = 0;
                    yield return null;
                }
            }

            foreach (var meshRenderer in _lights)
            {
                meshRenderer.enabled = enable;
                if (count++ > maxPerStep)
                {
                    count = 0;
                    yield return null;
                }
            }

            foreach (var component in _particles)
            {
                component.gameObject.SetActive(enable);
                if (count++ > maxPerStep)
                {
                    count = 0;
                    yield return null;
                }
            }

            foreach (var component in _waterfalls)
            {
                component.gameObject.SetActive(enable);
                if (count++ > maxPerStep)
                {
                    count = 0;
                    yield return null;
                }
            }

            foreach (var component in _skinnedRenderers)
            {
                component.enabled = enable;
                if (count++ > maxPerStep)
                {
                    count = 0;
                    yield return null;
                }
            }

            _disableCoroutine = null;
        }

        private void SetActiveLocation2(bool enable)
        {
            foreach (var meshRenderer in _meshRenderers)
                meshRenderer.enabled = enable;

            foreach (var meshRenderer in _lights)
                meshRenderer.enabled = enable;

            foreach (var component in _particles)
                component.gameObject.SetActive(enable);

            foreach (var component in _waterfalls)
                component.gameObject.SetActive(enable);

            foreach (var component in _skinnedRenderers)
                component.enabled = enable;

            _disableCoroutine = null;
        }
    }
}