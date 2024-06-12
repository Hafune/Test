using Core.Services;
using UnityEngine;

namespace Core.Lib
{
    public class AudioSourceClientPlayOneShotAll : AbstractEffect
    {
        [SerializeField] private GameObject _audioSourcesRoot;
        [SerializeField] private bool _playOnEnable = true;
        private AudioSource[] _audioSources;
        private AudioSourceService _audioSourceService;

        private void OnValidate()
        {
            if (_audioSourcesRoot && _audioSourcesRoot.GetComponent<AudioSource>() == null)
                _audioSourcesRoot = null;
        }

        private void Awake()
        {
            _audioSourceService = Context.Resolve<AudioSourceService>();
            _audioSources = _audioSourcesRoot.GetComponents<AudioSource>();
        }

        private void OnEnable()
        {
            if (_playOnEnable)
                Execute();
        }

        public override void Execute()
        {
            for (int i = 0, iMax = _audioSources.Length; i < iMax; i++)
                _audioSourceService.PlayOneShot(_audioSources[i], transform.position);
        }
    }
}