using System;
using Core.Components;
using Core.Systems;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class CS_MonsterCockroach_3_UpdateLogic : AbstractEntityResettableLogic
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private AnimationCurve _floatSpeed;
        [SerializeField] private float _fallSpeed;
        [SerializeField] private float _fallStartTime;
        private float _currentTime;
        private float _forwardSpeed;
        private EcsPool<TransformComponent> _transformPool;
        private EcsFilter _playerFilter;

        private void OnValidate() => _rigidbody = _rigidbody ? _rigidbody : GetComponentInParent<Rigidbody2D>();

        private void Awake()
        {
            _transformPool = Context.Resolve<EcsWorld>().GetPool<TransformComponent>();
            _playerFilter = Context.Resolve<EcsWorld>().Filter<PlayerUniqueTag>().Inc<TransformComponent>().End();
        }

        public override void ResetLogic(int entity)
        {
            _currentTime = 0f;
            var playerTransform = _transformPool.Get(_playerFilter.GetFirst()).transform;
            _forwardSpeed = Math.Abs(playerTransform.position.x - transform.position.x) / _fallStartTime *
                            transform.right.x.Sign();
        }

        public override void Run(int entity)
        {
            _currentTime += Time.deltaTime;

            if (_currentTime < _fallStartTime)
            {
                float percent = _currentTime / _fallStartTime;
                var currentVelocity = new Vector2(_forwardSpeed, _floatSpeed.Evaluate(percent));
                _rigidbody.velocity = currentVelocity;
            }
            else
            {
                _rigidbody.velocity = new Vector2(0, -_fallSpeed);
            }
        }
    }
}