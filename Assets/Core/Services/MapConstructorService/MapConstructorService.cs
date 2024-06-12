using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Core.Services;
using Lib;
using Reflex;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Core
{
    public class MapConstructorService : MonoConstruct, ISerializableService
    {
        public static ushort RoomVariantSeed;

        public event Action OnMapCreated;
        public event Action OnMinimapEnable;
        public event Action OnMinimapDisable;
        public bool MinimapRenderersShouldBeEnable { get; private set; }
        public Bounds MapBounds { get; private set; }
        public bool IsMapCreated { get; private set; }

        private ServiceData _serviceData = new();
        private PlayerDataService _playerDataService;
        private Context _context;
        private bool _isNewMap;

        protected override void Construct(Context context) => _context = context;

        private void Awake()
        {
            RoomVariantSeed = (ushort)(Random.value * 5);
            _playerDataService = _context.Resolve<PlayerDataService>();
            _playerDataService.RegisterService(this);

            var playerStateService = _context.Resolve<PlayerStateService>();
            playerStateService.OnResetLevelProgress += () =>
            {
                RoomVariantSeed++;
                _serviceData = new();
                _playerDataService.SetDirty(this);
            };
        }

        public void SetBeginCreating() => IsMapCreated = false;

        public void MapCreated(Bounds bounds)
        {
            MapBounds = bounds;
            StartCoroutine(WaitSpawners());

            if (_isNewMap)
                _playerDataService.SetDirty(this);

            _isNewMap = false;
        }

        private IEnumerator WaitSpawners()
        {
            yield return null; //wait start spawnes
            yield return null; //wait start spawned objects

            IsMapCreated = true;
            OnMapCreated?.Invoke();
        }

        public bool TryGetData(out LevelData data)
        {
            var sceneName = SceneManager.GetActiveScene().name;
            var scenes = _serviceData.scenes;
            bool exist = scenes.ContainsKey(sceneName);

            if (!exist)
            {
                scenes.Add(sceneName, new LevelData());
                _isNewMap = true;
            }

            data = scenes[sceneName];
            return exist;
        }

        public void EnableMinimapRenderers()
        {
            MinimapRenderersShouldBeEnable = true;
            OnMinimapEnable?.Invoke();
        }

        public void DisableMinimapRenderers()
        {
            MinimapRenderersShouldBeEnable = false;
            OnMinimapDisable?.Invoke();
        }

        public void SaveData() => _playerDataService.SerializeData(this, _serviceData);

        public void ReloadData() => _serviceData = _playerDataService.DeserializeData<ServiceData>(this);

        private class ServiceData
        {
            public Dictionary<string, LevelData> scenes = new();
        }

        [Serializable]
        public class LevelData
        {
            public List<RoomData> rooms = new();
        }

        [Serializable]
        public class RoomData
        {
            public int roomIndex;
            public int exitIndex;
        }
    }
}