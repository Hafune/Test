using System;
using System.Collections.Generic;
using Core.Services;
using Reflex;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class SceneCheckpointsService : IInitializableService, ISerializableService
    {
        public event Action OnChecked;
        private ServiceData _serviceData = new();
        private PlayerDataService _playerDataService;
        private AddressableService _addressableService;
        private PlayerStateService _playerStateService;

        public void InitializeService(Context context)
        {
            _playerDataService = context.Resolve<PlayerDataService>();
            _playerDataService.RegisterService(this);

            _addressableService = context.Resolve<AddressableService>();
            _playerStateService = context.Resolve<PlayerStateService>();
            _playerStateService.OnResetLevelProgress += () =>
            {
                _serviceData = new();
                _playerDataService.SetDirty(this);
            };
        }

        public void SaveCheckpoint(string uuid, Vector2 position)
        {
            _serviceData.checkpointSceneName = SceneManager.GetActiveScene().name;
            _serviceData.checkpointPosition = position;

            if (!_serviceData.scenes.TryGetValue(_serviceData.checkpointSceneName, out var data))
                _serviceData.scenes[_serviceData.checkpointSceneName] = data = new();

            _playerStateService.RestoreConsumables();

            data.checkpoints.Add(uuid);
            _playerDataService.SetDirty(this);
            OnChecked?.Invoke();
        }

        public void SetPositionAsCheckpointIfEmpty(in string sceneName, Vector2 position)
        {
            if (_serviceData.checkpointSceneName == sceneName)
                return;

            SetPositionAsCheckpoint(in sceneName, position);
        }

        public void SetPositionAsCheckpoint(in string sceneName, Vector2 position)
        {
            _serviceData.checkpointSceneName = sceneName;
            _serviceData.checkpointPosition = position;
            _playerDataService.SetDirty(this);
            OnChecked?.Invoke();
        }

        public bool HasCheckpoint(string uuid) =>
            _serviceData.scenes.TryGetValue(_serviceData.checkpointSceneName, out var data) &&
            data.checkpoints.Contains(uuid);

        public Vector2 GetCheckpointPosition(in string sceneName)
        {
#if UNITY_EDITOR
            if (sceneName != _serviceData.checkpointSceneName)
                throw new Exception("Попытка получить чекпоинт в неправильной сцене");
#endif

            return _serviceData.checkpointPosition;
        }

        public bool IsNewGame() => string.IsNullOrEmpty(_serviceData.checkpointSceneName);

        public bool LoadLastCheckpointIfExist()
        {
            if (string.IsNullOrEmpty(_serviceData.checkpointSceneName))
                return false;

            _playerStateService.sceneStartPosition = ScenePositionsEnum.LastCheckpointPosition;
            _addressableService.LoadSceneAsync(_serviceData.checkpointSceneName);
            return true;
        }

        public void SaveData() => _playerDataService.SerializeData(this, _serviceData);

        public void ReloadData() => _serviceData = _playerDataService.DeserializeData<ServiceData>(this);

        private class ServiceData
        {
            public string checkpointSceneName = string.Empty;
            public Vector2 checkpointPosition;
            public Dictionary<string, SceneData> scenes = new();
        }

        private class SceneData
        {
            public HashSet<string> checkpoints = new();
        }
    }
}