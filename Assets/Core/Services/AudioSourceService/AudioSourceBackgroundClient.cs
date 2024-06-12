using Core.Services;
using UnityEngine;

namespace Core.Lib
{
    public class AudioSourceBackgroundClient : AbstractEffect
    {
        [SerializeField] private GameObject _audioSourceRoot;
        [SerializeField] private float _fadeInTime = 2;
        [SerializeField] private bool _playOnEnable = true;
        private AudioSource _audioSource;
        private AudioSourceService _audioSourceService;

        private void OnValidate()
        {
            if (_audioSourceRoot && _audioSourceRoot.GetComponent<AudioSource>() == null)
                _audioSourceRoot = null;
        }

        private void Awake()
        {
            _audioSourceService = Context.Resolve<AudioSourceService>();
            _audioSource = _audioSourceRoot.GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            if (_playOnEnable)
                Execute();
        }

        public override void Execute() => _audioSourceService.PlayBackground(_audioSource, _fadeInTime);
    }
}