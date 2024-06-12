using System;
using System.Collections;
#if UNITY_WEBGL
using Agava.WebUtility;
#endif
using Core.Lib;
using JetBrains.Annotations;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Services
{
    public class AudioSourceService : MonoConstruct, ISerializableService
    {
        public event Action<float> OnMasterVolumeChanged;
        public event Action<float> OnBackgroundVolumeChanged;
        public event Action<float> OnSFXVolumeChanged;

        private Glossary<AudioSource> _sources = new();

        public float MasterVolume => _serviceData.masterVolume;
        public float BackgroundVolume => _serviceData.backgroundVolume;
        public float SFXVolume => _serviceData.sfxVolume;

        private bool _hasApplicationFocus;
        private bool _inBackground;
        private bool _isAdPlaying;
        private PlayerDataService _playerDataService;
        private Context _context;
        private ServiceData _serviceData = new();
        [CanBeNull] private AudioSource _backgroundOld;
        [CanBeNull] private AudioSource _backgroundCurrent;
        private float _backgroundOldBaseVolume;
        private float _backgroundCurrentBaseVolume;
        private float _backgroundTempScale = 1f;

        public float BackgroundTempScale
        {
            get => _backgroundTempScale;
            set
            {
                _backgroundTempScale = value;
                UpdateBackgroundVolume();
            }
        }

        protected override void Construct(Context context) => _context = context;

        private void Awake()
        {
            _playerDataService = _context.Resolve<PlayerDataService>();
            _playerDataService.RegisterService(this);
        }

#if UNITY_WEBGL
        private void Start()
        {
            WebApplication.InBackgroundChangeEvent += OnInBackgroundChange;

            try
            {
                OnInBackgroundChange(WebApplication.InBackground);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.Message);
            }

            OnApplicationFocus(Application.isFocused);
        }
#endif

        public void SaveData() => _playerDataService.SerializeData(this, _serviceData);

        public void ReloadData()
        {
            _serviceData = _playerDataService.DeserializeData<ServiceData>(this);

            ChangeMasterVolume(_serviceData.masterVolume, false);
            ChangeBackgroundVolume(_serviceData.backgroundVolume, false);
            ChangeSFXVolume(_serviceData.sfxVolume, false);
            UpdateMasterVolume();
        }

        public void ChangeMasterVolume(float percent, bool dirty = true)
        {
            percent = Math.Clamp(percent, 0, 1);
            _serviceData.masterVolume = percent;
            AudioListener.volume = MasterVolume;
            UpdateMasterVolume();
            OnMasterVolumeChanged?.Invoke(percent);

            if (dirty)
                _playerDataService.SetDirty(this);
        }

        public void ChangeBackgroundVolume(float percent, bool dirty = true)
        {
            percent = Math.Clamp(percent, 0, 1);
            _serviceData.backgroundVolume = percent;
            SetupBackgroundsVolume();

            OnBackgroundVolumeChanged?.Invoke(percent);

            if (dirty)
                _playerDataService.SetDirty(this);
        }

        public void ChangeSFXVolume(float percent, bool dirty = true)
        {
            percent = Math.Clamp(percent, 0, 1);
            _serviceData.sfxVolume = percent;
            OnSFXVolumeChanged?.Invoke(percent);

            if (dirty)
                _playerDataService.SetDirty(this);
        }

        public void PlayOneShotUI(AudioSource sourcePrefab) => PlayOneShot(sourcePrefab, Vector3.zero);

        public void PlayOneShot(AudioSource sourcePrefab, Vector3 position)
        {
            if (!_sources.TryGetValue(sourcePrefab.GetInstanceID(), out var source))
            {
                ExtractSources(sourcePrefab.gameObject);
                source = _sources.GetValue(sourcePrefab.GetInstanceID());
            }

            source.volume = _serviceData.sfxVolume * sourcePrefab.volume;
            source.transform.position = position;
            source.PlayOneShot(sourcePrefab.clip);
        }

        public void PlayBackground(AudioSource sourcePrefab, float fadeInTime)
        {
            if (!_sources.TryGetValue(sourcePrefab.GetInstanceID(), out var source))
            {
                ExtractSources(sourcePrefab.gameObject);
                source = _sources.GetValue(sourcePrefab.GetInstanceID());
            }

            ChangeBackground(source, sourcePrefab, fadeInTime);
        }

        public void FadeBackground(float fadeOutTime) => ChangeBackground(null, null, fadeOutTime);

        public void AdPlaying()
        {
            _isAdPlaying = true;
            UpdateMasterVolume();
        }

        public void AdStopped()
        {
            _isAdPlaying = false;
            UpdateMasterVolume();
        }

        private void ExtractSources(GameObject go)
        {
            var templates = go.GetComponents<AudioSource>();
            var news = Instantiate(go, transform).gameObject.GetComponents<AudioSource>();

            for (int i = 0, iMax = templates.Length; i < iMax; i++)
                _sources.Add(templates[i].GetInstanceID(), news[i]);
        }

        private void ChangeBackground(
            [CanBeNull] AudioSource source,
            [CanBeNull] AudioSource sourcePrefab,
            float fadeTime)
        {
            if (_backgroundCurrent == source)
                return;

            _backgroundOld = _backgroundCurrent;
            _backgroundCurrent = source;

            _backgroundOldBaseVolume = _backgroundCurrentBaseVolume;
            _backgroundCurrentBaseVolume = sourcePrefab ? sourcePrefab.volume : 0;

            if (_backgroundCurrent is not null)
            {
                _backgroundCurrent.volume = 0;
                _backgroundCurrent?.Play();
            }

            StartCoroutine(
                FadeOutOldFadeInNew(
                    _backgroundOld,
                    _backgroundOldBaseVolume,
                    _backgroundCurrent,
                    _backgroundCurrentBaseVolume,
                    fadeTime
                ));
        }

        private IEnumerator FadeOutOldFadeInNew(
            AudioSource backgroundOld,
            float backgroundOldBaseVolume,
            AudioSource backgroundCurrent,
            float backgroundCurrentBaseVolume,
            float fadeTime
        )
        {
            if (BackgroundVolume == 0)
            {
                SetupBackgroundsVolume();
                yield break;
            }

            float _oldLastVolume = backgroundOld ? backgroundOld.volume : 0;
            float _currentLastVolume = backgroundCurrent ? backgroundCurrent.volume : 0;

            while (true)
            {
                var oldComplete = false;
                var currentComplete = false;

                if (backgroundOld is not null && backgroundOld.volume > 0 && _oldLastVolume == backgroundOld.volume)
                {
                    backgroundOld.volume -=
                        BackgroundVolume * backgroundOldBaseVolume * Time.unscaledDeltaTime / fadeTime;
                    _oldLastVolume = backgroundOld.volume;
                }
                else
                {
                    oldComplete = true;
                }

                var totalMaxCurrentVolume = BackgroundVolume * backgroundCurrentBaseVolume;

                if (backgroundCurrent is not null && backgroundCurrent.volume < totalMaxCurrentVolume &&
                    _currentLastVolume == backgroundCurrent.volume)
                {
                    backgroundCurrent.volume += totalMaxCurrentVolume * Time.unscaledDeltaTime / fadeTime;
                    _currentLastVolume = backgroundCurrent.volume;
                }
                else
                {
                    currentComplete = true;
                }

                if (!oldComplete || !currentComplete)
                {
                    yield return null;
                    continue;
                }

                SetupBackgroundsVolume();
                yield break;
            }
        }

        private void SetupBackgroundsVolume()
        {
            if (_backgroundOld is not null)
            {
                _backgroundOld.volume = 0;
                _backgroundOld.Stop();
            }

            UpdateBackgroundVolume();
        }

        private void UpdateBackgroundVolume()
        {
            if (_backgroundCurrent is not null)
                _backgroundCurrent.volume = BackgroundVolume * _backgroundCurrentBaseVolume * BackgroundTempScale;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            _hasApplicationFocus = hasFocus;
            UpdateMasterVolume();
        }

        private void OnInBackgroundChange(bool inBackground)
        {
            _inBackground = inBackground;
            UpdateMasterVolume();
        }

        private void UpdateMasterVolume()
        {
            var total = MasterVolume;

            if (_isAdPlaying)
                total = 0;

            if (!_hasApplicationFocus)
                total = 0;

            if (_inBackground)
                total = 0;

            AudioListener.volume = total;
        }

#if UNITY_WEBGL
        private void OnDestroy()
        {
            try
            {
                WebApplication.InBackgroundChangeEvent -= OnInBackgroundChange;
            }
            catch (Exception)
            {
                // ignored
            }
        }
#endif

        private class ServiceData
        {
            public float masterVolume = .5f;
            public float backgroundVolume = 1;
            public float sfxVolume = 1;
        }
    }
}