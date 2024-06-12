using System;
using Core.Views.MainMenu;
using Reflex;
using UnityEngine;

namespace Core.Views
{
    public abstract class AbstractTaskTutorial<T> : MonoBehaviour, IMyTask
        where T : ITutorial
    {
        private Action<IMyTask> _onComplete;
        public bool InProgress { get; private set; }

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            _onComplete = onComplete;
            InProgress = true;
            context.Resolve<T>().Run(OnComplete);
        }

        private void OnComplete()
        {
            InProgress = false;
            _onComplete?.Invoke(this);
        }
    }
}