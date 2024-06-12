using System;
using System.Collections.Generic;
using Lib;
using Reflex;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Services
{
    public class GlobalTeleportService : MonoConstruct, ISerializableService
    {
        public event Action<Action> OnNeedShowBedMenuInBattlefield;
        public event Action<Action> OnNeedShowBedMenuInHub;

        [SerializeField] private SceneField _hubScene;
        private Action _closeCallback;
        private Context _context;
        private AddressableService _addressableService;
        private PlayerDataService _playerDataService;
        private SceneCheckpointsService _sceneCheckpointsService;
        private PlayerStateService _playerStateService;
        private ServiceData _serviceData = new();
        private TooltipService _tooltipService;
        private HubService _hudService;
        private Vector2 _contactTeleportPosition;
        private bool _hasContact;

        protected override void Construct(Context context) => _context = context;

        private void Awake()
        {
            _playerDataService = _context.Resolve<PlayerDataService>();
            _playerDataService.RegisterService(this);

            _playerStateService = _context.Resolve<PlayerStateService>();

            _sceneCheckpointsService = _context.Resolve<SceneCheckpointsService>();
            _addressableService = _context.Resolve<AddressableService>();
            _tooltipService = _context.Resolve<TooltipService>();
            _hudService = _context.Resolve<HubService>();

            _context.Resolve<PlayerStateService>().OnResetLevelProgress += () =>
            {
                _serviceData = new();
                _serviceData.lastBattlefieldScene = _addressableService.SceneName;
                _playerDataService.SetDirty(this);
            };
        }

        public void InTouch(Vector2 returnPosition)
        {
            if (!_hudService.HasRestartOption)
                return;

            _hasContact = true;
            _contactTeleportPosition = returnPosition;
            _tooltipService.ShowBedTooltip();
        }

        public void OutOfTouch()
        {
            if (!_hudService.HasRestartOption)
                return;

            _hasContact = false;
            _tooltipService.HideTooltip();
        }

        public bool TryInteract(Action closeCallback)
        {
            if (!_hasContact)
                return false;

            _closeCallback = closeCallback;
            _tooltipService.HideTooltip();

            if (_hubScene == SceneManager.GetActiveScene().name)
                OnNeedShowBedMenuInHub!(BeforeClose);
            else
                OnNeedShowBedMenuInBattlefield!(BeforeClose);

            return true;
        }

        public void SetStartPosition(in string sceneName, Vector2 position)
        {
            if (!_serviceData.scenes.TryGetValue(sceneName, out var data))
                _serviceData.scenes[sceneName] = data = new();

            if (data.startPosition == position)
                return;

            data.startPosition = position;
            _playerDataService.SetDirty(this);
        }

        public Vector2 GetStartPosition(in string sceneName) =>
            _serviceData.scenes[sceneName].startPosition;

        public Vector2 GetLastUsedTeleportPosition(in string sceneName) =>
            _serviceData.scenes[sceneName].lastUsedTeleportPosition;

        public void SaveLastUsedTeleportPosition()
        {
            _serviceData.lastBattlefieldScene = SceneManager.GetActiveScene().name;
            _serviceData.scenes[_serviceData.lastBattlefieldScene].lastUsedTeleportPosition = _contactTeleportPosition;
            _playerDataService.SetDirty(this);
        }

        public void LoadHubScene() => _addressableService.LoadSceneAsync(_hubScene);

        public void LoadLastBattlefieldScene()
        {
            _playerStateService.sceneStartPosition = ScenePositionsEnum.LastUsedBattlefieldTeleportPosition;
            _sceneCheckpointsService.SetPositionAsCheckpoint(_serviceData.lastBattlefieldScene,
                _serviceData.scenes[_serviceData.lastBattlefieldScene].lastUsedTeleportPosition);
            _addressableService.LoadSceneAsync(_serviceData.lastBattlefieldScene);
        }

        public void RestartLastBattlefieldScene()
        {
            _playerStateService.sceneStartPosition = ScenePositionsEnum.StartPosition;
            _addressableService.LoadSceneAsync(_serviceData.lastBattlefieldScene);
        }

        private void BeforeClose()
        {
            if (_hasContact)
                _tooltipService.ShowBedTooltip();

            _closeCallback?.Invoke();
        }

        public void SaveData() => _playerDataService.SerializeData(this, _serviceData);

        public void ReloadData() => _serviceData = _playerDataService.DeserializeData<ServiceData>(this);

        private class ServiceData
        {
            public string lastBattlefieldScene;
            public Dictionary<string, SceneData> scenes = new();
        }

        private class SceneData
        {
            public Vector2 startPosition;
            public Vector2 lastUsedTeleportPosition;
        }
    }
}