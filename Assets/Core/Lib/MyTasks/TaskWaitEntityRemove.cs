using System;
using Reflex;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Core.Tasks
{
    public class TaskWaitEntityRemove : MonoBehaviour, IMyTask
    {
        public bool InProgress { get; private set; }
        private Action<IMyTask> _onComplete;

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            InProgress = true;
            _onComplete = onComplete;
            payload.Get<ConvertToEntity>().OnEntityWasDeleted += EntityRemoved;
        }

        private void EntityRemoved(ConvertToEntity entityRef)
        {
            entityRef.OnEntityWasDeleted -= EntityRemoved;
            InProgress = false;
            _onComplete?.Invoke(this);
        }
    }
}