using System;
using System.Collections.Generic;
using Reflex;

namespace Core.Services
{
    public abstract class AbstractResourceService : IInitializableService, ISerializableService
    {
        public event Action OnDataChange;
        public event Action<int> OnCountChange;

        protected ServiceData _serviceData = new();
        private PlayerDataService _playerDataService;

        public int Count => _serviceData.count;

        public void InitializeService(Context context)
        {
            _playerDataService = context.Resolve<PlayerDataService>();
            _playerDataService.RegisterService(this);
        }

        public bool TryChangeValue(int value)
        {
            if (_serviceData.count + value < 0)
                return false;

            _serviceData.count += value;
            _playerDataService.SetDirty(this);
            OnCountChange?.Invoke(_serviceData.count);
            return true;
        }

        public void AddCollected(in string instanceUuid)
        {
            if (string.IsNullOrEmpty(instanceUuid)) 
                return;
            
            _serviceData.collectedItems.Add(instanceUuid);
            _playerDataService.SetDirty(this);
        }

        public bool TryGetInstanceState(in string instanceUuid) => _serviceData.collectedItems.Contains(instanceUuid);

        public void SaveData() => _playerDataService.SerializeData(this, _serviceData);
        
        public void ReloadData()
        {
            _serviceData = _playerDataService.DeserializeData<ServiceData>(this);
            OnCountChange?.Invoke(_serviceData.count);
            OnDataChange?.Invoke();
        }

        protected class ServiceData
        {
            public int count;
            public HashSet<string> collectedItems = new();
        }
    }
}