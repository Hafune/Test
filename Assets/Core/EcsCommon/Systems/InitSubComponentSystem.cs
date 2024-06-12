using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class InitSubComponentSystem : IEcsRunSystem
    {
        private EcsWorldInject _world;

        private readonly EcsFilterInject<Inc<EventInit>> _hasInitFilter;
        private readonly EcsFilterInject<Inc<EventInit>, Exc<WriteDefaultsBeforeRemoveEntityComponent>> _writeFilter;
        private readonly EcsFilterInject<Inc<EventInit, PlayerUniqueTag>> _playerFilter;
        private readonly EcsFilterInject<Inc<EventInit, EnemyTag>> _enemiesFilter;
        private readonly EcsFilterInject<Inc<EventInit, MissileTag>> _missileFilter;
        private readonly EcsFilterInject<Inc<EventInit, BehaviorTreeComponent>> _behaviorFilter;

        private readonly EcsFilterInject<Inc<EventInit, LookOnTargetComponent>> _abilityLookOnTargetFilter;
        private readonly EcsFilterInject<Inc<EventInit, DamageAreaComponent>> _damageAreaFilter;

        private readonly EcsFilterInject<Inc<EventInit, TransformComponent, NestedEntitiesComponent>> _nestedFilter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            if (_hasInitFilter.Value.GetEntitiesCount() == 0)
                return;

            foreach (var i in _writeFilter.Value)
                _pools.WriteDefaultsBeforeRemoveEntity.Add(i);

            foreach (var i in _playerFilter.Value)
            {
                ref var c = ref _pools.ActionCurrent.Add(i);
                c.AutoReset(ref c);

                _pools.Target.Add(i);
                _pools.MoveDirection.Add(i);
                _pools.DamageScale.Add(i).value = 1f;
                _pools.PlayerInputMemory.Add(i);
                _pools.PlayerController.Add(i);
                _pools.Modules.Add(i);
                _pools.ShotTrigger.Add(i);
            }

            foreach (var i in _enemiesFilter.Value)
            {
                ref var c = ref _pools.ActionCurrent.Add(i);
                c.AutoReset(ref c);

                _pools.Target.Add(i);
                _pools.MoveDirection.Add(i);
                _pools.DamageScale.Add(i).value = 1f;
                _pools.Modules.Add(i);
            }

            foreach (var i in _behaviorFilter.Value)
            {
                _pools.NpcAction.Add(i);
            }

            foreach (var i in _missileFilter.Value)
            {
                ref var c = ref _pools.ActionCurrent.GetOrInitialize(i);
                c.AutoReset(ref c);

                if (!_pools.DamageScale.Has(i))
                    _pools.DamageScale.Add(i).value = 1f;
            }

            foreach (var i in _abilityLookOnTargetFilter.Value)
            {
                _pools.Direction.Add(i).transform = _pools.LookOnTarget.Get(i).transform;
            }

            foreach (var i in _nestedFilter.Value)
            {
                _pools.EventScanHierarchyForNestedEntities.Add(i);
            }
        }
    }
}