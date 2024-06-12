using Core.Services;
using UnityEngine;

namespace Core.Lib
{
    public class AudioSourceClientPlayOneShotRandom : AbstractEffect
    {
        [SerializeField] private GameObject _audioSourcesRoot;
        [SerializeField] private bool _playOnEnable = true;
        private AudioSource[] _audioSources;
        private AudioSourceService _audioSourceService;
        private int _lastIndex;

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
            var index = Random.Range(0, _audioSources.Length);
            if (_audioSources.Length > 1 && _lastIndex == index)
                index = ++index % _audioSources.Length;

            _lastIndex = index;
            _audioSourceService.PlayOneShot(_audioSources[index], transform.position);
        }
    }
}