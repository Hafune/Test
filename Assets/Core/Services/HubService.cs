using Lib;
using Reflex;
using UnityEngine;

namespace Core.Services
{
    public class HubService : MonoConstruct, ISerializableService
    {
        [SerializeField] private SceneField _levelToOpenRestartOption;
        private ServiceData _serviceData = new ();
        private PlayerDataService _playerDataService;

        public bool HasReturnToBattlefieldOption => _serviceData.hasReturnOption;
        public bool HasRestartOption => _serviceData.hasRestartOption;
        private Context _context;

        protected override void Construct(Context context) => _context = context;

        private void Awake()
        {
            _playerDataService = _context.Resolve<PlayerDataService>();
            _playerDataService.RegisterService(this);
            var addressableService = _context.Resolve<AddressableService>();
            var playerStateService = _context.Resolve<PlayerStateService>();
            playerStateService.OnResetLevelProgress += DisableReturnOption;

            addressableService.OnSceneLoaded += () =>
            {
                if (addressableService.SceneName == _levelToOpenRestartOption)
                    EnableRestartOption();
            };
        }

        private void EnableRestartOption()
        {
            _serviceData.hasRestartOption = true;
            _playerDataService.SetDirty(this);
        }

        public void EnableReturnOption()
        {
            _serviceData.hasReturnOption = true;
            _playerDataService.SetDirty(this);
        }

        private void DisableReturnOption()
        {
            _serviceData.hasReturnOption = false;
            _playerDataService.SetDirty(this);
        }
        
        public void SaveData() => _playerDataService.SerializeData(this, _serviceData);
        
        public void ReloadData() => _serviceData = _playerDataService.DeserializeData<ServiceData>(this);

        private class ServiceData
        {
            public bool hasRestartOption;
            public bool hasReturnOption;
        }
    }
}