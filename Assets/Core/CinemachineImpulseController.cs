using Cinemachine;
using Core.Services;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Lib
{
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class CinemachineImpulseController : MonoConstruct
    {
        [SerializeField] private CinemachineImpulseSource _impulseSource;
        [SerializeField] private float force;

        private Context _context;
        private PlayerStateService _playerSceneState;

        private void OnValidate() =>
            _impulseSource = _impulseSource ? _impulseSource : GetComponent<CinemachineImpulseSource>();

        protected override void Construct(Context context) => _context = context;

        private void Awake() => _playerSceneState = _context.Resolve<PlayerStateService>();

        private void OnEnable()
        {
            if (!_playerSceneState.HasPlayerCharacter)
                return;

            var direction = (_playerSceneState.PlayerTransform.position - transform.position).normalized;
            _impulseSource.GenerateImpulse(direction * force);
        }
    }
}