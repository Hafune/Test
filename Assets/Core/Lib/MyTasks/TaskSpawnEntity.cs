using System;
using Core.Lib;
using Core.Services;
using Lib;
using Reflex;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using VInspector;
using Voody.UniLeo.Lite;

namespace Core.Tasks
{
    [ExecuteInEditMode]
    public class TaskSpawnEntity : MonoBehaviour, IMyTask
    {
        [SerializeField] private bool _waitRemove;
        [SerializeField] private ConvertToEntity _prefab;

        private Action<IMyTask> _onComplete;
        private UnityComponentPool<ConvertToEntity> _pool;

        public bool InProgress { get; private set; }
        
#if UNITY_EDITOR
        private ConvertToEntity _lastPrefab;

        private void OnValidate()
        {
            if (PrefabUtility.IsPartOfPrefabAsset(gameObject))
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
            if (this.IsDestroyed() || !enabled)
                return;

            OnDisable();
            OnEnable();
        }

        private void OnEnable()
        {
            if (PrefabUtility.IsPartOfPrefabAsset(gameObject))
                return;

            _lastPrefab = _prefab;

            if (this.IsDestroyed() || Application.isPlaying || !_prefab)
                return;

            transform.DestroyChildren();
            SpawnPrefabTask.InstantiatePrefabDontSave(_prefab.transform, transform);
        }

        private void OnDisable()
        {
            transform.DestroyChildren();
            _lastPrefab = null;
        }
#endif

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
#if UNITY_EDITOR
            if (InProgress)
                throw new Exception("Новый вызов таски до её завершения");
#endif

            _pool ??= context.Resolve<PoolService>().ScenePool.GetPullByPrefab(_prefab);
            _onComplete = onComplete;
            InProgress = _waitRemove;

            var entityRef = _pool.GetObject(transform.position, transform.rotation);
            entityRef.ManualConnection();
            
            payload.Set(entityRef);

            if (_waitRemove)
                entityRef.OnEntityWasDeleted += EntityRemoved;
            else
                onComplete?.Invoke(this);
        }

        private void EntityRemoved(ConvertToEntity entityRef)
        {
            entityRef.OnEntityWasDeleted -= EntityRemoved;

            InProgress = false;
            _onComplete?.Invoke(this);
        }
    }
}