using System;
using Core.Services;
using Reflex;
using UnityEngine;

namespace Core.Lib
{
    public class TaskDialog : MonoBehaviour, IMyTask
    {
        [SerializeField] private DialogData _dialog;
        private DialogService _dialogService;
        private Action<IMyTask> _onComplete;

        public bool InProgress { get; private set; }

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            if (!_dialog)
            {
                Debug.LogError("Диалог не задан");
                onComplete?.Invoke(this);
                return;
            }

            InProgress = true;
            _onComplete = onComplete;
            _dialogService = context.Resolve<DialogService>();
            _dialogService.OnComplete += OnCompleteDialog;
            _dialogService.RunDialog(_dialog);
        }

        private void OnCompleteDialog()
        {
            _dialogService.OnComplete -= OnCompleteDialog;
            InProgress = false;
            _onComplete?.Invoke(this);
        }
    }
}