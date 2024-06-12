using System;
using System.Collections.Generic;
using Core.Services;
using Reflex;

namespace Core
{
    public class GemService : IInitializableService, ISerializableService
    {
        public event Action OnDataChange;
        public event Action<int> OnCountChange;
        public event Action<int> OnCountPlus;

        private ServiceData _serviceData = new();
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

            if (value > 0)
                OnCountPlus?.Invoke(value);

            return true;
        }

        public void AddCollected(in string instanceUuid)
        {
            if (string.IsNullOrEmpty(instanceUuid))
                return;

            _serviceData.collectedGems.Add(instanceUuid);
            _playerDataService.SetDirty(this);
        }

        public bool TryGetInstanceState(in string instanceUuid) => _serviceData.collectedGems.Contains(instanceUuid);

        public void SaveData() => _playerDataService.SerializeData(this, _serviceData);

        public void ReloadData()
        {
            _serviceData = _playerDataService.DeserializeData<ServiceData>(this);
            OnCountChange?.Invoke(_serviceData.count);
            OnDataChange?.Invoke();
        }

        private class ServiceData
        {
#if UNITY_EDITOR && USE_TEST_ITEMS
            public int count = 100000;
#else
            public int count = 0;
#endif
            public HashSet<string> collectedGems = new();
        }
    }
}