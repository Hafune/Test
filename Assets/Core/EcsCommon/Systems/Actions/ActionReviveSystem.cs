using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using UnityEngine;

namespace Core.Systems
{
    public class ActionReviveSystem : AbstractActionSystem<ActionReviveComponent>, IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ActionCurrentComponent,
                ActionReviveComponent,
                TransformComponent,
                EventActionStart<ActionReviveComponent>,
                AnimatorComponent
            >,
            Exc<InProgressTag<ActionReviveComponent>>> _startFilter;

        private readonly EcsFilterInject<Inc<EventDeath, LivesValueComponent>> _eventDiedFilter;
        private readonly EcsFilterInject<Inc<InvulnerabilityLifetimeComponent>> _invulnerabilityFilter;

        private readonly EcsPoolInject<EventActionStart<ActionReviveComponent>> _eventStartActionRevivePool;
        private readonly EcsPoolInject<EventValueUpdated<HitPointValueComponent>> _eventUpdatedHitPointValuePool;
        private readonly EcsPoolInject<EventValueUpdated<LivesValueComponent>> _eventUpdatedLivesPool;
        private readonly EcsPoolInject<HitPointMaxValueComponent> _hitPointMaxPool;
        private readonly EcsPoolInject<HitPointValueComponent> _hitPointPool;
        private readonly EcsPoolInject<InvulnerabilityLifetimeComponent> _invulnerabilityLifetimePool;
        private readonly EcsPoolInject<LivesValueComponent> _livesPool;
        private readonly EcsPoolInject<TransformComponent> _transformPool;

        private const int _invulnerabilityMaxLifetime = 4;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _eventDiedFilter.Value)
            {
                ref var lives = ref _livesPool.Value.Get(i);

                if (lives.value <= 1)
                {
                    lives.onLivesEnded?.Invoke();
                    continue;
                }

                lives.value--;
                _eventUpdatedLivesPool.Value.AddIfNotExist(i);
                _eventStartActionRevivePool.Value.Add(i);
            }

            foreach (var i in _startFilter.Value)
                StartAction(i);

            foreach (var i in _invulnerabilityFilter.Value)
                UpdateLifetime(i);

            CleanEventStart();
        }

        private void StartAction(int entity)
        {
            BeginActionProgress(entity);

            _hitPointPool.Value.Get(entity).value = _hitPointMaxPool.Value.Get(entity).value;
            _eventUpdatedHitPointValuePool.Value.AddIfNotExist(entity);

            ref var invulnerability = ref _invulnerabilityLifetimePool.Value.GetOrInitialize(entity);
            invulnerability.maxLifetime = _invulnerabilityMaxLifetime;
        }

        private void UpdateLifetime(int entity)
        {
            ref var lifetimeComponent = ref _invulnerabilityLifetimePool.Value.Get(entity);

            if ((lifetimeComponent.lifetime += Time.deltaTime) < lifetimeComponent.maxLifetime)
                return;

            _invulnerabilityLifetimePool.Value.Del(entity);
        }
    }
}