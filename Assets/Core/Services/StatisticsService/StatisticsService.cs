using System.Collections.Generic;
using GameAnalyticsSDK;
using Lib;
using Reflex;

namespace Core.Services
{
    public class StatisticsService : MonoConstruct, ISerializableService
    {
        private ServiceData _serviceData = new();
        private PlayerDataService _playerDataService;
        private Context _context;

        protected override void Construct(Context context) => _context = context;

        public void Awake()
        {
            _playerDataService = _context.Resolve<PlayerDataService>();
            _playerDataService.RegisterService(this, false, true);

            var addressableService = _context.Resolve<AddressableService>();

            _context.Resolve<PlayerStateService>().OnPlayerDied += () =>
            {
                _serviceData.totalDeaths++;
                _playerDataService.SetDirty(this);

                GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Total_death_count",
                    _serviceData.totalDeaths, string.Empty, string.Empty);

                var counter = _serviceData.deathsOnLevel;
                var sceneName = addressableService.SceneName;

                if (!counter.TryGetValue(sceneName, out _))
                    counter[sceneName] = 0;

                counter[sceneName]++;

                GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, $"Total_death_count_on_{sceneName}",
                    counter[sceneName], string.Empty, string.Empty);
            };

            addressableService.OnSceneLoaded += () =>
            {
                var sceneName = addressableService.SceneName;
                var counter = _serviceData.onSceneLoadedSceneCount;

                if (!counter.TryGetValue(sceneName, out _))
                    counter[sceneName] = 0;

                counter[sceneName]++;
                _playerDataService.SetDirty(this);

                if (counter[sceneName] == 1)
                    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, $"Loaded_{sceneName}");
            };

            addressableService.OnNextSceneWillBeLoaded += () =>
            {
                var sceneName = addressableService.SceneName;
                var counter = _serviceData.onNextSceneWillBeLoaded;

                if (!counter.TryGetValue(sceneName, out _))
                    counter[sceneName] = 0;

                counter[sceneName]++;
                _playerDataService.SetDirty(this);

                if (counter[sceneName] == 1)
                    GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete,
                        $"Begin_loaded_ - {sceneName}");
            };

            var sdkService = _context.Resolve<SdkService>();
            _context.Resolve<GemService>().OnCountPlus += value =>
            {
                _serviceData.totalGemsCollected += value;
                sdkService.SaveLeaderboard(_serviceData.totalGemsCollected);
            };
        }

        public void SaveData() => _playerDataService.SerializeData(this, _serviceData);

        public void ReloadData() => _serviceData = _playerDataService.DeserializeData<ServiceData>(this);

        private class ServiceData
        {
            public int totalDeaths;
            public int totalGemsCollected;
            public Dictionary<string, int> deathsOnLevel = new();
            public Dictionary<string, int> onSceneLoadedSceneCount = new();
            public Dictionary<string, int> onNextSceneWillBeLoaded = new();
        }
    }
}