using System;
using Core.Services;
using Reflex;
using UnityEngine;
using VInspector;

namespace Core.Lib
{
    [Obsolete("Использовать " + nameof(TaskSequenceWithDisableInputs))]
    public class TaskSetActivePlayerInputs : MonoBehaviour, IMyTask
    {
        [SerializeField] private bool _active;
        public bool InProgress => false;

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            if (_active)
                context.Resolve<PlayerStateService>().ResumeInputs();
            else
                context.Resolve<PlayerStateService>().PauseInputs();

            onComplete?.Invoke(this);
        }

        [Button]
        private void Rename() => name = $"Player_Inputs_{(_active ? "ON" : "OFF")}";
    }
}