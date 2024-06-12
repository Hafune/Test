using Core.Components;
using Core.Systems;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class EnemyToPlayerFlyLogic : AbstractActionEntityLogic
    {
        [SerializeField] private AbstractEntityLogic _start;
        [SerializeField] private AbstractEntityLogic _cancel;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _targetOffset;
        private Vector2 _offset;
        private EcsPool<TransformCenterComponent> _transformCenterPool;
        private EcsPool<ActionCurrentComponent> _actionCurrentPool;
        private EcsFilter _playerFilter;
        private Transform _playerTransform;
        private EcsPool<MoveSpeedValueComponent> _moveSpeedPool;

        private void OnValidate()
        {
            _rigidbody = _rigidbody ? _rigidbody : GetComponentInParent<Rigidbody2D>();
            _animator = _animator ? _animator : GetComponentInParent<Animator>();
        }

        private void Awake()
        {
            var world = Context.Resolve<EcsWorld>();
            _transformCenterPool = world.GetPool<TransformCenterComponent>();
            _moveSpeedPool = world.GetPool<MoveSpeedValueComponent>();
            _actionCurrentPool = world.GetPool<ActionCurrentComponent>();
            _playerFilter = world.Filter<PlayerUniqueTag>().Inc<TransformComponent>().End();
        }

        public override void StartLogic(int entity)
        {
            _start?.Run(entity);
            _offset = _targetOffset.localPosition;
            _playerTransform = _transformCenterPool.Get(_playerFilter.GetFirst()).transform;
            _actionCurrentPool.Get(entity).canBeCanceled = true;
        }

        public override void UpdateLogic(int entity)
        {
            var horizontalDirection = new Vector2((_playerTransform.position.x - transform.position.x).Sign(), 1);
            var _direction =
                (_playerTransform.position - transform.position - (Vector3)(_offset * horizontalDirection))
                .normalized;
            _rigidbody.velocity = _direction * _moveSpeedPool.Get(entity).value;
        }

        public override void CancelLogic(int entity)
        {
            _cancel?.Run(entity);
            _rigidbody.velocity = Vector2.zero;
        }
    }
}