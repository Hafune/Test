using System;
using Core.Services;
using Reflex;
using UnityEngine;

namespace Core.Lib
{
    public class TaskSequenceWithNewAudioLevel : TaskSequence
    {
        [SerializeField] private float _volume = .2f;
        private Action<IMyTask> _onComplete;
        private AudioSourceService _audioService;
        private float _oldVolume;

        public override void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            _audioService = context.Resolve<AudioSourceService>();
            _oldVolume = _audioService.BackgroundTempScale;
            _audioService.BackgroundTempScale = _volume;

            _onComplete = onComplete;
            base.Begin(context, payload, Callback);
        }

        private void Callback(IMyTask myTask)
        {
            _audioService.BackgroundTempScale = _oldVolume;
            _onComplete?.Invoke(this);
        }
    }
}