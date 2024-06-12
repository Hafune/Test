using System;
using System.Collections;
using Core.Services;
using Lib;
using Reflex;
using UnityEngine;
using VInspector;

namespace Core
{
    public class TaskTutorialInstance : MonoConstruct, IMyTask
    {
        [field: SerializeField, ReadOnly] public string InstanceUuid { get; private set; }
        [SerializeField] private bool _runSelfOnStart;
        [SerializeField] private MonoBehaviour task;
        private IMyTask _task;
        private TutorialsService _service;
        private Context _context;
        private Payload _payload;

        public bool InProgress { get; private set; }

        private void OnValidate()
        {
            if (task is not IMyTask)
                task = null;
        }

        protected override void Construct(Context context) => _context = context;

#if UNITY_EDITOR
        [Button]
        private void RegenerateInstanceUuid() => InstanceUuid = Guid.NewGuid().ToString();
#endif

        private void Awake()
        {
            _service = _context.Resolve<TutorialsService>();
            _task = task as IMyTask;
        }

        private IEnumerator Start()
        {
            if (!_runSelfOnStart)
                yield break;

            var playerStateService = _context.Resolve<PlayerStateService>();
            while (!playerStateService.HasPlayerCharacter)
                yield return null;

            if (!_service.IsTutorialCompleted(InstanceUuid))
                Begin(_context, _payload = Payload.GetPooled(), OnComplete);
        }

        private void OnEnable()
        {
            _service.OnChange += ReloadData;
            ReloadData();
        }

        private void OnDisable() => _service.OnChange -= ReloadData;

        private void ReloadData()
        {
            if (_service.IsTutorialCompleted(InstanceUuid))
                gameObject.SetActive(false);
        }

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            if (_service.IsTutorialCompleted(InstanceUuid))
            {
                onComplete?.Invoke(this);
                return;
            }

            InProgress = true;
            _task.Begin(context, payload, _ =>
            {
                _service.SetTutorialComplete(InstanceUuid);
                InProgress = false;
                onComplete?.Invoke(this);
            });
        }

        private void OnDestroy() => _payload?.Dispose();

        private void OnComplete(IMyTask _) => _payload?.Dispose();
    }
}