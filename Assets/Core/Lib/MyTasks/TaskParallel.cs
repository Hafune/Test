using System;
using System.Collections.Generic;
using Lib;
using Reflex;
using UnityEngine;
using VInspector;

namespace Core
{
    public class TaskParallel : MonoConstruct, IMyTask
    {
        [SerializeField] private bool runSelf;

        private TaskParallelCore _tasks;
        private Context _context;
        private Payload _payload;

        public bool InProgress => _tasks?.InProgress ?? false;

        private void Awake()
        {
            List<IMyTask> iTasks = new(transform.childCount);
            transform.ForEachSelfChildren<IMyTask>(iTasks.Add);
            _tasks = new(iTasks.ToArray());
        }

        protected override void Construct(Context context) => _context = context;

        private void Start()
        {
            if (runSelf)
                Begin(_context, _payload = Payload.GetPooled(), OnComplete);
        }

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null
        ) =>
            _tasks.Begin(context, payload, onComplete);

        private void OnDestroy() => _payload?.Dispose();

        private void OnComplete(IMyTask task) => _payload?.Dispose();
    }
}