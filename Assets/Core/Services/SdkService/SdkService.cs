using System;
using System.Collections;
#if YANDEX_GAMES
using Agava.YandexGames;
#endif
#if VK_GAMES
using Agava.VKGames;
#endif
using Core.EcsCommon.ValueComponents;
using Core.Lib;
using Core.Services;
using Reflex;
using UnityEngine;

namespace Core
{
    public class SdkService : IInitializableService
    {
        public Flags flags = new();

        private Action<string> _onSuccessLoadPlayerData;
        private Action _onRewardedCallback;
        private Action<string> _onErrorCallback;
        private DateTime _lastLoadTime = new(2020, 1, 1);
        private DateTime _lastSaveTime = new(2020, 1, 1);
        private TimeScaleService _timeScaleService;
        private AudioSourceService _audioSourceService;
        private Context _context;
        private float _delaySecond = 5;
        private string _lastPlayerData = "{}";
        private bool _adOpened;
        private bool _rewardSuccess;
        private Action _playAdCallback;
        private float _lastSaveLeaderboardTime;
        private const string Closed = "Closed";

        public bool IsInitialized { get; private set; }

        public void InitializeService(Context context)
        {
            _audioSourceService = context.Resolve<AudioSourceService>();
            _timeScaleService = context.Resolve<TimeScaleService>();
        }

        public IEnumerator Initialize(Action callback)
        {
            IEnumerator enumerator = null;

            void SetInitialized()
            {
                IsInitialized = true;
                callback?.Invoke();
            }

            try
            {
#if YANDEX_GAMES
                enumerator = YandexGamesSdk.Initialize(() =>
                {
                    YandexGamesSdk.GetConsoleFlags(value =>
                    {
                        flags = JsonUtility.FromJson<Flags>(value);

                        if (flags.IsActualDomain != "true" || !YandexGamesSdk.IsRunningOnYandex)
                            Debug.Log("Ошибка окружения");
                        else
                            SetInitialized();
                    });
                });
#elif VK_GAMES
                if (!VKGamesSdk.IsRunningOnVk)
                    Debug.Log("Ошибка окружения");
                else
                    enumerator = VKGamesSdk.Initialize(SetInitialized, SetInitialized);
#else
                SetInitialized();
#endif
            }
            catch (Exception e)
            {
                Debug.LogWarning("SDK INIT ERROR");
                Debug.LogWarning(e.Message);
#if UNITY_EDITOR
                if (e.Message == "")
                    SetInitialized();
                else
                    throw;
#else
            SetInitialized();
#endif
            }

            if (enumerator is not null)
                yield return enumerator;
        }

        public void LoadPlayerData(Action<string> onSuccess, Action<string> onError)
        {
            _onSuccessLoadPlayerData = onSuccess;
            DateTime time = DateTime.Now;

            if ((time - _lastLoadTime).TotalSeconds < _delaySecond)
            {
                onSuccess(_lastPlayerData);
                return;
            }

            _lastLoadTime = time;

            try
            {
#if YANDEX_GAMES
                if (!PlayerAccount.IsAuthorized)
                    throw new Exception("Player not authorized");

                PlayerAccount.GetCloudSaveData(Success, onError);
#elif VK_GAMES
                if (!VKGamesSdk.Initialized)
                    throw new Exception("Player not authorized");

                Storage.GetCloudSaveData("data", Success, () => onError("Error"));
#else
                onError("No selected sdk defenition");
#endif
            }
            catch (Exception e)
            {
                Debug.LogWarning("CLOUD LOAD ERROR");
                Debug.LogWarning(e.Message);
                onError(e.Message);
#if UNITY_EDITOR
                if (e.Message == "")
                    onError(e.Message);
                else
                    throw;
#else
            onError(e.Message);
#endif
            }
        }

        public void SavePlayerData(string data, Action onSuccess, Action<string> onError)
        {
            _lastPlayerData = data;
            var time = DateTime.Now;

            if ((time - _lastSaveTime).TotalSeconds < _delaySecond)
            {
                onSuccess();
                return;
            }

            _lastSaveTime = time;

            try
            {
#if YANDEX_GAMES
                if (!PlayerAccount.IsAuthorized)
                    throw new Exception("Player not authorized");

                PlayerAccount.SetCloudSaveData(data, onSuccess, onError);
#elif VK_GAMES
                if (!VKGamesSdk.Initialized)
                    throw new Exception("Player not authorized");

                Storage.SetCloudSaveData("data", data, onSuccess, () => onError("Error"));
#else
                onError("No selected sdk defenition");
#endif
            }
            catch (Exception e)
            {
                Debug.LogWarning("CLOUD SAVE ERROR");
                Debug.LogWarning(e.Message);
                onError(e.Message);
            }
        }

        public string GetLocale()
        {
#if YANDEX_GAMES && !UNITY_EDITOR
        return YandexGamesSdk.Environment.i18n.lang;
#endif
            return MyEnumUtility<LocalizationService.MyLocales>.Name((int)LocalizationService.MyLocales.ru);
        }

        public void PlayAd(Action callback)
        {
            if (_adOpened || _playAdCallback != null)
            {
                callback?.Invoke();
                return;
            }

            _playAdCallback = callback;

            try
            {
#if YANDEX_GAMES
                Agava.YandexGames.InterstitialAd.Show(
                    AdBegin,
                    _ => AdEnded(),
                    _ => AdEnded(),
                    AdEnded
                );
#elif VK_GAMES
                if (Application.isMobilePlatform)
                    Interstitial.Show();

                AdEnded();
#else
                callback?.Invoke();
#endif
            }
            catch (Exception e)
            {
                Debug.Log("ADD SHOW ERROR");
                Debug.Log(e.Message);
                AdEnded();
            }
        }

        public void PlayVideoAd(Action onRewardedCallback, Action<string> onErrorCallback)
        {
            if (_onRewardedCallback != null)
                return;

            _onRewardedCallback = onRewardedCallback;
            _onErrorCallback = onErrorCallback;
            _rewardSuccess = false;

            try
            {
#if YANDEX_GAMES
                VideoAd.Show(AdBegin, VideoAdSuccess, () => VideoAdErrorOrClose(Closed), VideoAdErrorOrClose);
#elif VK_GAMES
                AdBegin();
                VideoAd.Show(() =>
                {
                    VideoAdSuccess();
                    VideoAdErrorOrClose(Closed);
                }, () => VideoAdErrorOrClose(Closed));
#else
                onRewardedCallback?.Invoke();
                _onRewardedCallback = null;
#endif
            }
            catch (Exception e)
            {
                VideoAdErrorOrClose(e.Message);
            }
        }

        public void SaveLeaderboard(int score)
        {
            if (_lastSaveLeaderboardTime - Time.time < 1)
                return;

            _lastSaveLeaderboardTime = Time.time;
#if YANDEX_GAMES
            Agava.YandexGames.Leaderboard.SetScore("score", score);
#endif
        }

        private void VideoAdErrorOrClose(string error)
        {
            _adOpened = false;
            _audioSourceService.AdStopped();
            _timeScaleService.Resume();

            if (_rewardSuccess)
                _onRewardedCallback?.Invoke();
            else
                _onErrorCallback?.Invoke(error);

            _onRewardedCallback = null;
            _onErrorCallback = null;
        }

        private void VideoAdSuccess() => _rewardSuccess = true;

        private void Success(string playerData)
        {
            _lastPlayerData = playerData;
            _onSuccessLoadPlayerData(_lastPlayerData);
        }

        private void AdBegin()
        {
            _adOpened = true;
            _timeScaleService.Pause();
            _audioSourceService.AdPlaying();
        }

        private void AdEnded()
        {
            if (_adOpened)
            {
                _adOpened = false;
                _timeScaleService.Resume();
                _audioSourceService.AdStopped();
                _playAdCallback?.Invoke();
                _playAdCallback = null;
                return;
            }

            _playAdCallback?.Invoke();
            _playAdCallback = null;
        }

        public class Flags
        {
            public string IsActualDomain = "false";
            public string StartSettingsLevel = "1";
            public string DrawingDistance = "0";
            public string RenderScale = "1";
            public string Shadows = "false";
            public string SSAO = "false";
        }
    }
}