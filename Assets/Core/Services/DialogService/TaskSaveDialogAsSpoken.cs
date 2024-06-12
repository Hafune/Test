using System;
using Core.Lib;
using Core.Services;
using Reflex;
using UnityEngine;

namespace Core
{
    public class TaskSaveDialogAsSpoken : MonoBehaviour, IMyTask
    {
        [SerializeField] private InstanceUuid _instanceUuid;
        public bool InProgress { get; private set; }

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            context
                .Resolve<DialogService>()
                .SaveDialogAsSpoken(_instanceUuid.uuid);
            onComplete?.Invoke(this);
        }
    }
}