using System;
using System.ComponentModel;
using Core.Components;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using Unity.VisualScripting;
using UnityEngine;
#if UNITY_EDITOR
using VInspector.Libs;
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace Voody.UniLeo.Lite
{
    public class ConvertToEntity : MonoConstruct
    {
        [Description("В момент его вызова RawEntity уже не валидна")]
        public Action<ConvertToEntity> OnEntityWasDeleted;

        public Action<ConvertToEntity> OnEntityWasConnected;
        private Action _onWasConnected;

        [Tooltip(
            "Объект не выключается при уничтожении сущности, важно когда активность объекта контролируется через иерархию")]
        [SerializeField]
        private bool _dontDisableGameObject;

        private Action<int, EcsWorld, bool>[] _cache;
        private EcsPool<EventRemoveEntity> _eventRemovePool;
        private EcsWorld _world;
        private int _startLayer;
        private bool _isStarted;

        [Description("Уникальный Id префаба")]
        [field: SerializeField, ReadOnly]
        public int TemplateId { get; private set; }

        public int RawEntity { get; private set; } = -1;
        public Context Context { get; private set; }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (Application.isPlaying)
                return;

            EditorApplication.delayCall += () =>
            {
                try
                {
                    if ((PrefabUtility.GetPrefabAssetType(gameObject) != PrefabAssetType.Regular &&
                         PrefabUtility.GetPrefabAssetType(gameObject) != PrefabAssetType.Variant) ||
                        PrefabUtility.GetPrefabInstanceStatus(gameObject) != PrefabInstanceStatus.NotAPrefab ||
                        PrefabUtility.GetNearestPrefabInstanceRoot(gameObject) != transform.root.gameObject)
                        return;

                    var old = TemplateId;
                    TemplateId = Animator.StringToHash(transform.root.gameObject.GetGuid());

                    if (old != TemplateId)
                        EditorUtility.SetDirty(this);
                }
                catch (Exception _)
                {
                    // ignored
                }
            };
        }
#endif

        protected override void Construct(Context context) => Context = context;

        private void Awake()
        {
            _startLayer = gameObject.layer;
            _world ??= Context.Resolve<EcsWorld>();
            _eventRemovePool = _world.GetPool<EventRemoveEntity>();

            MakeCache();
        }

        private void MakeCache()
        {
            if (_cache != null)
                return;

            var list = gameObject.GetComponents<BaseMonoProvider>();
            _cache = new Action<int, EcsWorld, bool>[list.Length];

            for (int i = 0, iMax = list.Length; i < iMax; i++)
            {
                var entityComponent = list[i];
                _cache[i] = entityComponent.Attach;
                Destroy(entityComponent);
            }
        }

        private void Start()
        {
            _isStarted = true;
            OnEnable();
        }

        private void OnEnable()
        {
            if (!_isStarted)
                return;

            gameObject.layer = _startLayer;
            ConnectToWorld();
        }

        private void OnDisable() => DisconnectFromWorld();

        public void ManualConnection(EcsWorld world = null)
        {
            _world ??= world;
            MakeCache();

#if UNITY_EDITOR
            if (!enabled)
                throw new Exception("Сущность выключена!!");
#endif

            _isStarted = true;
            ConnectToWorld();
        }

        public void RemoveConnectionInfo()
        {
            if (RawEntity == -1)
                return;

            OnEntityWasDeleted?.Invoke(this);
            RawEntity = -1;

            if (!_dontDisableGameObject)
                gameObject.SetActive(false);
        }

        public void RegisterCallWhenEntityReady(Action callback)
        {
            _onWasConnected += callback;

            if (RawEntity != -1)
                callback.Invoke();
        }

        public void UnRegisterCallWhenEntityReady(Action callback) => _onWasConnected -= callback;

        private void DisconnectFromWorld()
        {
            if (RawEntity == -1)
                return;

            _eventRemovePool.AddIfNotExist(RawEntity);
            RemoveConnectionInfo();
        }

        private void ApplyCache(int entity, EcsWorld world)
        {
            for (int i = 0, iMax = _cache.Length; i < iMax; i++)
                _cache[i].Invoke(entity, world, false);
        }

        private void ConnectToWorld()
        {
            if (RawEntity != -1)
                return;

            RawEntity = _world.NewEntity();
            _world.PackEntityWithWorld(RawEntity);

            ApplyCache(RawEntity, _world);
            OnEntityWasConnected?.Invoke(this);
            _onWasConnected?.Invoke();
        }
    }
}