using System.Collections;
using Core.Services;
using UnityEngine;
using VInspector;

namespace Core.Lib
{
    public class SpawnEffect : AbstractEffect
    {
        [SerializeField] private Vector3 _offset;
        [SerializeField] private bool _quaternionIdentity;
        [SerializeField] private Transform _prefab;
        [SerializeField] private bool _attachEffect;
        [SerializeField] private bool _spawnOnEnable;

        private UnityComponentPool<Transform> _effectPool;
        private Coroutine _updateCoroutine;

        private void Awake() =>
            _effectPool = Context.Resolve<PoolService>().DontDisposablePool.GetPullByPrefab(_prefab);

        private void OnEnable()
        {
            if (_spawnOnEnable)
                Execute();
        }

        private void OnDisable()
        {
            if (_updateCoroutine is not null)
                StopCoroutine(_updateCoroutine);

            _updateCoroutine = null;
        }

        public override void Execute()
        {
            _effectPool ??= Context.Resolve<PoolService>().DontDisposablePool.GetPullByPrefab(_prefab);

            var eff = _effectPool.GetObject(transform.position + _offset,
                _quaternionIdentity ? Quaternion.identity : transform.rotation);

            if (!_attachEffect)
                return;

            _updateCoroutine = StartCoroutine(UpdateCoroutine(eff));
        }

        private IEnumerator UpdateCoroutine(Transform eff)
        {
            while (eff!.gameObject.activeSelf)
            {
                eff!.transform.position = transform.position;
                yield return null;
            }

            yield return null;
            _updateCoroutine = null;
        }

#if UNITY_EDITOR
        [SerializeField, Range(0f, 1f)] private float _editorTimeScale = 1;
        [Button]
        private void PlayParticle()
        {
            var instance = Instantiate(
                _prefab,
                transform.position,
                _quaternionIdentity ? Quaternion.identity : transform.rotation,
                transform.root
            ).gameObject;
            instance.transform.position = transform.position + _offset;
            instance.hideFlags = HideFlags.DontSave;

            foreach (var system in instance.GetComponentsInChildren<ParticleSystem>(true))
                system.gameObject.AddComponent<EditorParticlePlayer>().SetTimeScale(_editorTimeScale);
        }
#endif
    }
}