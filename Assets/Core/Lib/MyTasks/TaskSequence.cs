using System;
using System.Collections.Generic;
using Lib;
using Reflex;
using UnityEngine;
using VInspector;

namespace Core
{
    public class TaskSequence : MonoConstruct, IMyTask
    {
        [SerializeField] [Min(1)] internal int _repeatCount = 1;
        [SerializeField] private bool runSelfOnStart;
        [SerializeField] private bool _includeInactive;

        private TaskSequenceCore _tasks;
        private Context _context;
        private Payload _payload;

        public bool InProgress => _tasks?.InProgress ?? false;

        private void Awake()
        {
            List<IMyTask> iTasks = new(transform.childCount * _repeatCount);
            _repeatCount.RepeatTimes(() =>
                transform.ForEachSelfChildren<IMyTask>(task => iTasks.Add(task), _includeInactive));
            _tasks = new(iTasks.ToArray());

#if UNITY_EDITOR
            if (_includeInactive)
                transform.ForEachSelfChildren<Transform>(child =>
                {
                    if (!child.gameObject.activeSelf &&
                        child.gameObject.TryGetComponent<IMyTask>(out _))
                        Debug.LogWarning($"Есть выключенные таски {child.name}", child);
                }, true);
#endif
        }

        protected override void Construct(Context context) => _context = context;

        private void Start()
        {
            if (runSelfOnStart)
                Begin(_context, _payload = Payload.GetPooled(), OnComplete);
        }

        public virtual void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
            =>
                _tasks.Begin(context, payload, onComplete);

        private void OnDestroy() => _payload?.Dispose();

        private void OnComplete(IMyTask task) => _payload?.Dispose();
    }
}