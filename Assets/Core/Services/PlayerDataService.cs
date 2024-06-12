using System;
using System.Collections;
using System.Collections.Generic;
using Core.Services;
using Lib;
using Reflex;
using UnityEngine;

namespace Core
{
    public class PlayerDataService : MonoConstruct
    {
        public event Action OnSaveEnd;
        private event Action OnInitialize;
        private ServicesValues _servicesValues = new();

        private Action InitializeCallback;

        private const string key = nameof(ServicesValues);
        private SdkService _sdkController;
        private Context _context;
        private float _lastSaveTime;
        private const float _sdkSaveDelay = 5.25f;
        private const float _saveDelay = 1f;
        private float _currentDelay = 1f;
        private bool _saveAlreadyActive;
        private bool _isInitialized;
        private bool _isInitializeRan;
        private readonly HashSet<ISerializableService> _totalServices = new();
        private readonly HashSet<ISerializableService> _dirtyServices = new();
        private readonly HashSet<ISerializableService> _ignoreResetServices = new();
        private readonly HashSet<ISerializableService> _lazeSaveServices = new();

        protected override void Construct(Context context) => _context = context;

        private void Awake() => enabled = false;

        private void Update()
        {
            if ((_currentDelay -= Time.deltaTime) > 0)
                return;

            _currentDelay = _saveDelay;
            Save();
        }

        public void Initialize(Action callback)
        {
            if (_isInitializeRan)
            {
                callback.Invoke();
                return;
            }

            _lastSaveTime = _sdkSaveDelay;
            _sdkController = _context.Resolve<SdkService>();
            _isInitializeRan = true;

            InitializeCallback = callback;
            _sdkController.LoadPlayerData(OnLoadSuccess, OnLoadError);
        }

        private void Save()
        {
            float awaitSeconds = _lastSaveTime - Time.unscaledTime + _sdkSaveDelay;
            _lastSaveTime = Time.unscaledTime;

            void SaveEnd() => OnSaveEnd?.Invoke();

            if (awaitSeconds > 0)
            {
                if (!_saveAlreadyActive)
                    StartCoroutine(AwaitBeforeSave(awaitSeconds, SaveEnd));
            }
            else
            {
                SaveServicesData(SaveEnd);
            }

            enabled = false;
        }

        public void Reset()
        {
            _servicesValues = new();

            foreach (var service in _ignoreResetServices)
                service.SaveData();

            foreach (var service in _totalServices)
                if (!_ignoreResetServices.Contains(service))
                    service.ReloadData();

            Save();
        }

        public void CallbackWhenReady(Action action)
        {
            if (_isInitialized)
            {
                action.Invoke();
                return;
            }

            OnInitialize += action;
        }

        public void SerializeData(object obj, object data) => _servicesValues.SerializeData(obj, data);

        public void RegisterService(ISerializableService service, bool ignoreReset = false, bool lazySave = false)
        {
            _totalServices.Add(service);

            if (ignoreReset)
                _ignoreResetServices.Add(service);

            if (lazySave)
                _lazeSaveServices.Add(service);
        }

        public void SetDirty(ISerializableService service)
        {
            _dirtyServices.Add(service);
            _currentDelay = _saveDelay;

            if (!_lazeSaveServices.Contains(service))
                enabled = true;
        }

        public T DeserializeData<T>(object obj) where T : new() => _servicesValues.DeserializeData<T>(obj);

        private void SaveServicesData(Action callback)
        {
            int saved = 0;

            foreach (var service in _dirtyServices)
            {
#if UNITY_EDITOR
                Debug.Log("SAVE " + service.GetType().Name);
#endif
                service.SaveData();
                saved++;
            }

            _dirtyServices.Clear();

            if (saved != 0)
                PrivateSave(callback);
            else
                callback?.Invoke();
        }

        private void PrivateSave(Action callback)
        {
            var base64 = ES3Functions.SerializeToBase64(_servicesValues);
            var stringData = JsonUtility.ToJson(new DataWrapper { data = base64 });

            if (!_isInitialized)
            {
                _lastSaveTime = Time.unscaledTime;
                callback?.Invoke();
            }
            else
                _sdkController.SavePlayerData(stringData, () => OnSaveSuccess(callback),
                    errorMessage => OnSaveError(errorMessage, stringData, callback));
        }

        private IEnumerator AwaitBeforeSave(float seconds, Action callback)
        {
            _saveAlreadyActive = true;
            yield return new WaitForSecondsRealtime(seconds);

            SaveServicesData(callback);
            _saveAlreadyActive = false;
        }

        private void OnLoadSuccess(string data = null)
        {
            if (!string.IsNullOrEmpty(data))
            {
                var wrapper = JsonUtility.FromJson<DataWrapper>(data);
                _servicesValues = ES3Functions.DeserializeFromBase64<ServicesValues>(wrapper.data) ?? new();
            }

            _dirtyServices.Clear();

            foreach (var service in _totalServices)
                service.ReloadData();

            InitializeCallback.Invoke();
            _isInitialized = true;
            OnInitialize?.Invoke();
            OnInitialize = default;
        }

        private void OnLoadError(string error)
        {
            OnLoadSuccess(PlayerPrefs.HasKey(key) ? PlayerPrefs.GetString(key) : null);
        }

        private void OnSaveSuccess(Action callback)
        {
            callback?.Invoke();

            _lastSaveTime = Time.unscaledTime;
            Debug.Log("DATA SAVED");
        }

        private void OnSaveError(string errorMessage, string stringData, Action callback)
        {
            Debug.LogWarning(errorMessage);
            PlayerPrefs.SetString(key, stringData);
            PlayerPrefs.Save();
            OnSaveSuccess(callback);
        }

        public class DataWrapper
        {
            public string data;
        }
    }
}