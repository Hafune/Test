using System;
using Core.Components;
using Core.Systems;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    [Obsolete("Использовать " + nameof(LookOnTargetLogic))]
    public class LookOnPlayer2DLogic : AbstractEntityLogic
    {
        [SerializeField] private float _maxAngleDifference;

        private EcsFilter _playerFilter;
        private EcsPool<TransformCenterComponent> _transformPool;
        private Vector3 _startLocalEulerAngles;

        private void Awake()
        {
            var world = Context.Resolve<EcsWorld>();
            _transformPool = world.GetPool<TransformCenterComponent>();
            _playerFilter = world.Filter<PlayerUniqueTag>().Inc<TransformCenterComponent>().End();
            _startLocalEulerAngles = transform.localEulerAngles;
        }

        public override void Run(int entity)
        {
            transform.localEulerAngles = _startLocalEulerAngles;
            var targetPosition = (Vector2)_transformPool.Get(_playerFilter.GetFirst()).transform.position;
            var position = (Vector2)transform.position;
            var right = (Vector2)transform.right;

            var totalRight = right.RotatedToward(targetPosition - position, _maxAngleDifference);
            transform.right = totalRight;
        }
    }
}