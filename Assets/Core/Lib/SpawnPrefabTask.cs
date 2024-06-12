using System;
using Core.Services;
using Lib;
using Reflex;
using Unity.VisualScripting;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using VInspector;

namespace Core.Lib
{
    [ExecuteInEditMode]
    public class SpawnPrefabTask : MonoConstruct, IMyTask
    {
        [SerializeField] private Transform _prefab;
        [SerializeField] private bool _runSelfOnStart;
        [SerializeField] private bool _useDontDestroyPool;
        [SerializeField] private bool _applySpawnerName;
        private Context _context;
        public bool InProgress => false;

        protected override void Construct(Context context) => _context = context;

#if UNITY_EDITOR
        private Transform _lastPrefab;

        public static GameObject InstantiatePrefabDontSave(Transform prefab, Transform parent)
        {
            var instance = PrefabUtility.InstantiatePrefab(prefab, parent);
            var prefabGo = instance.GameObject();
            prefabGo.hideFlags = HideFlags.DontSave;
            prefabGo.transform.localScale = Vector3.one;
            return prefabGo;
        }

        private void OnValidate()
        {
            if (PrefabUtility.IsPartOfPrefabAsset(gameObject) ||
                this.IsDestroyed() ||
                !enabled ||
                EditorApplication.isPlayingOrWillChangePlaymode ||
                Application.isPlaying)
                return;

            if (_lastPrefab != _prefab)
                EditorApplication.delayCall += Refresh;
        }

        [Button]
        private void AutoRename()
        {
            if (_prefab)
                name = _prefab.name + "_spawn";
        }

        private void Refresh()
        {
            if (this.IsDestroyed() ||
                !enabled ||
                EditorApplication.isPlayingOrWillChangePlaymode ||
                Application.isPlaying)
                return;

            OnDisable();
            OnEnable();
        }

        private void OnEnable()
        {
            if (PrefabUtility.IsPartOfPrefabAsset(gameObject))
                return;

            if (this.IsDestroyed() || Application.isPlaying || !_prefab)
                return;

            _lastPrefab = _prefab;

            transform.DestroyChildren();
            InstantiatePrefabDontSave(_prefab, transform);
        }

        private void OnDisable()
        {
            if (Application.isPlaying)
                return;

            transform.DestroyChildren(true);
            _lastPrefab = null;
        }
#endif

        private void Start()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                return;
#endif
            if (_runSelfOnStart)
                Begin(_context, null);
        }

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            var effectPool = _useDontDestroyPool
                ? context.Resolve<PoolService>().DontDisposablePool.GetPullByPrefab(_prefab)
                : context.Resolve<PoolService>().ScenePool.GetPullByPrefab(_prefab);

            var instance = effectPool.GetObject(
                transform.position,
                transform.rotation,
                transform
            );

            instance.localScale = Vector3.one;

            if (_useDontDestroyPool)
            {
                instance.parent = null;
                DontDestroyOnLoad(instance.gameObject);
            }

            if (_applySpawnerName)
                instance.name = name;

            if (!_runSelfOnStart)
                payload.Set(instance);

            onComplete?.Invoke(this);
        }
    }
}