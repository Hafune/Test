using System;
using Core.Services;
using Reflex;
using UnityEngine;

namespace Core.Views
{
    public class TaskPlayerRestoreConsumables : MonoBehaviour, IMyTask
    {
        public bool InProgress { get; private set; }

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            context.Resolve<PlayerStateService>().RestoreConsumables();
            onComplete?.Invoke(this);
        }
    }
}