using System;
using JetBrains.Annotations;
using Reflex;

namespace Core
{
    public class TaskParallelCore : IMyTask
    {
        private IMyTask[] _tasks;
        private Payload[] _payloads;
        private int _completedCount;

        [CanBeNull] private Action<IMyTask> _onComplete;

        public bool InProgress { get; private set; }

        public TaskParallelCore(IMyTask[] tasks)
        {
            _tasks = tasks;
            _payloads = new Payload[_tasks.Length];
        }

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            try
            {
                if (InProgress)
                    throw new Exception("Новый вызов таски до её завершения");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            InProgress = true;
            _onComplete = onComplete;
            _completedCount = 0;

            for (int i = 0, iMax = _tasks.Length; i < iMax; i++)
                _tasks[i].Begin(context, _payloads[i] = payload.Copy(), Complete);
        }

        private void Complete(IMyTask task)
        {
            if (++_completedCount < _tasks.Length)
                return;

            InProgress = false;

            foreach (var payload in _payloads)
                payload?.Dispose();
            
            _onComplete?.Invoke(this);
        }
    }
}