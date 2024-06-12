using System;
using Cinemachine;
using Core.Components;
using Core.Generated;
using JetBrains.Annotations;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using UnityEngine;
using UnityEngine.SceneManagement;
using Voody.UniLeo.Lite;

namespace Core.Services
{
    [Serializable]
    public class PlayerStateService : MonoConstruct, IAbstractGlobalState
    {
        public event Action OnShowHud;
        public event Action OnHideHud;
        public event Action OnCanMove;
        public event Action OnCantMove;
        public Action<Action> OnOpenInventory;
        public Action OnResetLevelProgress;
        public Action OnPlayerDied;
        [NonSerialized] public bool canOpenInventory = true;
        [NonSerialized] public bool canOpenMap = true;
        [NonSerialized] public ScenePositionsEnum sceneStartPosition = ScenePositionsEnum.StartPosition;

        [SerializeField] private SceneField _mainMenuScene;
        [SerializeField] private ConvertToEntity _playerCharacterPrefab;
        [CanBeNull] private ConvertToEntity _playerCharacter;
        private Context _context;
        private EcsPool<EventActionStart<ActionReviveComponent>> _eventActionStartRevivePool;
        private GlobalStateService _globalStateService;
        private GlobalTeleportService _globalTeleportService;
        private SceneCheckpointsService _sceneCheckpointsService;
        private PlayerInputs.PlayerActions _playerInputs;
        private Joystick _joystick;
        private MapConstructorService _mapConstructorService;
        private AddressableService _addressableService;
        private EcsEngine _ecsEngine;
        private CinemachineVirtualCamera _virtualCamera;
        private CinemachineTransposer _transposer;
        private ComponentPools _pool;
        private GameObject _container;
        private int _totalInputUsers;

        public Transform PlayerTransform => _playerCharacter?.transform;

        public bool HasPlayerCharacter => _playerCharacter is not null;

        protected override void Construct(Context context) => _context = context;

        private void Awake()
        {
            _container = new GameObject("BuildPlayerContainer");
            _container.SetActive(false);
            _container.transform.parent = transform;

            _globalStateService = _context.Resolve<GlobalStateService>();
            _mapConstructorService = _context.Resolve<MapConstructorService>();
            _playerInputs = _context.Resolve<PlayerInputs.PlayerActions>();
            _joystick = _context.Resolve<Joystick>();
            _virtualCamera = _context.Resolve<CinemachineVirtualCamera>();
            _transposer = _virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            _eventActionStartRevivePool =
                _context.Resolve<EcsWorld>().GetPool<EventActionStart<ActionReviveComponent>>();
            _pool = _context.Resolve<ComponentPools>();
            _ecsEngine = _context.Resolve<EcsEngine>();
            _globalTeleportService = _context.Resolve<GlobalTeleportService>();
            _sceneCheckpointsService = _context.Resolve<SceneCheckpointsService>();

            _addressableService = _context.Resolve<AddressableService>();
            _addressableService.OnFadeBegin += PauseInputs;
            _addressableService.OnSceneLoaded += ResumeInputs;
            _addressableService.OnNextSceneWillBeLoaded += () =>
            {
                if (_playerCharacter != null)
                {
                    var body = _playerCharacter.GetComponent<Rigidbody>();
                    body.isKinematic = true;
                    body.detectCollisions = false;
                }

                OnCantMove?.Invoke();
            };
        }

        public void SetPlayerCharacterPrefab(ConvertToEntity prefab) => _playerCharacterPrefab = prefab;

        public void PlayerDied()
        {
            OnPlayerDied?.Invoke();
            OnResetLevelProgress?.Invoke();
            _eventActionStartRevivePool.Add(_playerCharacter!.RawEntity);
        }

        public void PlayerCompleteGame()
        {
            OnResetLevelProgress?.Invoke();
            _addressableService.LoadSceneAsync(_mainMenuScene, true);
        }

        public void EnableState()
        {
            _globalStateService.ChangeActiveState(this);
            BuildCharacter();

            if (_mapConstructorService.IsMapCreated)
                ApplyCheckpointPosition();
            else
                _mapConstructorService.OnMapCreated += ApplyCheckpointPosition;
        }

        public void DisableState() => RemoveCharacterAndControls();

        public void PauseInputs()
        {
            if (--_totalInputUsers != 0)
                return;

            _playerInputs.Disable();
            _joystick.gameObject.SetActive(false);
        }

        public void ResumeInputs()
        {
            if (++_totalInputUsers != 1)
                return;

            _playerInputs.Enable();
            _joystick.gameObject.SetActive(true);
        }

        public void RestoreConsumables()
        {
            if (_playerCharacter?.RawEntity != -1)
                _pool.EventRestoreConsumables.AddIfNotExist(_playerCharacter!.RawEntity);
            else
                Debug.LogError("Запрос восстоновления расходников при неактивном персонаже");
        }

        private void ApplyCheckpointPosition()
        {
            var sceneName = SceneManager.GetActiveScene().name;
            var position = sceneStartPosition switch
            {
                ScenePositionsEnum.LastCheckpointPosition => _sceneCheckpointsService.GetCheckpointPosition(
                    in sceneName),
                ScenePositionsEnum.StartPosition => _globalTeleportService.GetStartPosition(in sceneName),
                ScenePositionsEnum.LastUsedBattlefieldTeleportPosition => _globalTeleportService
                    .GetLastUsedTeleportPosition(
                        in sceneName),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            _playerCharacter!.transform.position = position;
            var body = _playerCharacter!.transform.GetComponent<Rigidbody>();
            body.position = position;
            body.isKinematic = false;
            body.detectCollisions = true;
            _transposer.ForceSetPosition(position);

            _mapConstructorService.OnMapCreated -= ApplyCheckpointPosition;

            OnCanMove?.Invoke();
            OnShowHud?.Invoke();
        }

        private void RemoveCharacterAndControls()
        {
            if (_playerCharacter == null)
                return;

            Destroy(_playerCharacter.gameObject);

            _playerCharacter = null;
            PauseInputs();
            OnHideHud?.Invoke();
        }

        private void BuildCharacter()
        {
            if (_playerCharacter is not null)
                return;

            _playerCharacter = _context.Instantiate(_playerCharacterPrefab, Vector3.zero, Quaternion.identity,
                _container.transform);

            _playerCharacter!.ManualConnection(_ecsEngine.World);
            _playerCharacter.transform.parent = null;

            ResumeInputs();
        }
    }
}