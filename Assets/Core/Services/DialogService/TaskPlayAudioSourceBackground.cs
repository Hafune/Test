using System;
using Core.Services;
using Reflex;
using UnityEngine;

namespace Core.Lib
{
    public class TaskPlayAudioSourceBackground : MonoBehaviour, IMyTask
    {
        [SerializeField] private GameObject _audioSourceRoot;
        [SerializeField] private float _fadeInTime = 2;

        public bool InProgress { get; private set; }

        private void OnValidate()
        {
            if (_audioSourceRoot && _audioSourceRoot.GetComponent<AudioSource>() == null)
                _audioSourceRoot = null;
        }

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            context.Resolve<AudioSourceService>()
                .PlayBackground(_audioSourceRoot.GetComponent<AudioSource>(), _fadeInTime);
                
            onComplete?.Invoke(this);
        }
    }
}