using System;
using JetBrains.Annotations;
using Reflex;

namespace Core
{
    public class TaskSequenceCore : IMyTask
    {
        private Payload _payload;
        private IMyTask[] _tasks;
        private Context _context;
        private int _index;

        [CanBeNull] private Action<IMyTask> _onComplete;

        public bool InProgress { get; private set; }

        public TaskSequenceCore(IMyTask[] tasks) => _tasks = tasks;

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            if (InProgress)
                throw new Exception("Новый вызов таски до её завершения");

            _payload = payload;
            InProgress = true;
            _context = context;
            _onComplete = onComplete;
            _index = -1;

            Next();
        }

        private void Complete(IMyTask task) => Next();

        private void Next()
        {
            if (_tasks.Length > ++_index)
                _tasks[_index].Begin(_context, _payload, Complete);
            else
            {
                InProgress = false;
                _onComplete?.Invoke(this);
            }
        }
    }
}