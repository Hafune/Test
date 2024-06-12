using System;
using System.Collections.Generic;
using Reflex;

namespace Core.Services
{
    public class TutorialsService : IInitializableService, ISerializableService
    {
        public event Action OnChange;

        private ServiceData _serviceData = new();
        private PlayerDataService _playerDataService;

        public void InitializeService(Context context)
        {
            _playerDataService = context.Resolve<PlayerDataService>();
            _playerDataService.RegisterService(this);
        }

        public bool IsTutorialCompleted(in string instanceUuid) => _serviceData.uuids.Contains(instanceUuid);

        public void SetTutorialComplete(in string instanceUuid)
        {
            _serviceData.uuids.Add(instanceUuid);
            _playerDataService.SetDirty(this);
        }

        public void SaveData() => _playerDataService.SerializeData(this, _serviceData);
        
        public void ReloadData()
        {
            _serviceData = _playerDataService.DeserializeData<ServiceData>(this);
            OnChange?.Invoke();
        }

        private class ServiceData
        {
            public HashSet<string> uuids = new();
        }
    }
}