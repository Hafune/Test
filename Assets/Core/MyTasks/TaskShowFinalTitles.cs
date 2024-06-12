using System;
using Core.Services;
using Core.Views;
using Reflex;
using UnityEngine;

namespace Core
{
    public class TaskShowFinalTitles : MonoBehaviour, IMyTask
    {
        private Action<IMyTask> _onComplete;
        private PlayerStateService _playerStateService;
        public bool InProgress => false;

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            _onComplete = onComplete;
            _playerStateService = context.Resolve<PlayerStateService>();
            var view = context.Resolve<TitlesView>();
            view.Show(OnComplete);
        }

        private void OnComplete()
        {
            _playerStateService.PlayerCompleteGame();
            _onComplete?.Invoke(this);
        }
    }
}