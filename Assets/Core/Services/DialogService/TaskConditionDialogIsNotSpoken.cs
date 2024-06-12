using System;
using Core.Lib;
using Core.Lib.MyTasks;
using Core.Services;
using Reflex;
using UnityEngine;

namespace Core
{
    public class TaskConditionDialogIsNotSpoken : MonoBehaviour, IMyTask
    {
        [SerializeField] private InstanceUuid _instanceUuid;

        public bool InProgress { get; private set; }

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            payload
                .GetOrInitialize<Conditions>()
                .Set<TaskConditionDialogIsNotSpoken>(
                    !context
                        .Resolve<DialogService>()
                        .DialogIsSpoken(_instanceUuid.uuid)
                );

            onComplete?.Invoke(this);
        }
    }
}