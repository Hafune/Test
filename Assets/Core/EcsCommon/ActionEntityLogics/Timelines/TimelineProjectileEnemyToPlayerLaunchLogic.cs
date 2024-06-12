using System;
using Core.Components;
using Core.Systems;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    [Obsolete("использовать "+ nameof(LookOnTargetLogic))]
    public class TimelineProjectileEnemyToPlayerLaunchLogic : AbstractEntityLogic
    {
        [SerializeField] private ProjectileLauncher2D _projectileLauncher;

        private EcsFilter _playerFilter;
        private EcsPool<TransformComponent> _transformPool;

        private void Awake()
        {
            var world = Context.Resolve<EcsWorld>();
            _transformPool = world.GetPool<TransformComponent>();
            _playerFilter = world.Filter<PlayerUniqueTag>().Inc<TransformComponent>().End();
        }

        public override void Run(int entity)
        {
            var targetPosition = (Vector2)_transformPool.Get(_playerFilter.GetFirst()).transform.position;
            var spawnerPosition = (Vector2)_projectileLauncher.transform.position;

            var angle = spawnerPosition.SignedAngleAtRight(targetPosition);

            if (transform.right.x < 0)
                angle = 180 - angle;

            var rotation = _projectileLauncher.transform.rotation;
            var euler = rotation.eulerAngles;
            rotation.eulerAngles = new Vector3(euler.x, euler.y, angle);
            _projectileLauncher.transform.rotation = rotation;
            _projectileLauncher.Launch();
        }
    }
}