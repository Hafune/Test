using System;
using Core.Services;
using Reflex;

namespace Core.Lib
{
    public class TaskSequenceWithDisableInputs : TaskSequence
    {
        private Action<IMyTask> _onComplete;
        private PlayerStateService _playerStateService;

        public override void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            _playerStateService = context.Resolve<PlayerStateService>();
            _playerStateService.PauseInputs();

            _onComplete = onComplete;
            base.Begin(context, payload, Callback);
        }

        private void Callback(IMyTask myTask)
        {
            _playerStateService.ResumeInputs();
            _onComplete?.Invoke(this);
        }
    }
}