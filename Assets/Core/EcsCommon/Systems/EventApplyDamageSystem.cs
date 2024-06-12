using Core.Components;
using Core.Generated;
using Core.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Reflex;
using Unity.Mathematics;
using UnityEngine;

namespace Core.Systems
{
    public class EventApplyDamageSystem<T> : IEcsRunSystem
        where T : struct, IEventDamageData
    {
        private readonly EcsFilterInject<
            Inc<
                T,
                HitPointValueComponent
            >,
            Exc<InvulnerabilityLifetimeComponent>> _filter;

        private readonly ComponentPools _pools;
        private readonly EcsPoolInject<EventCausedDamage> _causedDamagePool;
        private readonly EcsPoolInject<EventDamageTaken> _eventDamageTakenPool;
        private readonly EcsPoolInject<EventValueUpdated<HitPointValueComponent>> _eventRefreshHitPointValuePool;
        private readonly EcsPoolInject<EventActionStart<ActionDeathComponent>> _eventStartDiedPool;
        private readonly EcsPoolInject<HitTakenEffectComponent> _hitEffectPool;
        private readonly EcsPoolInject<HitPointValueComponent> _hitPointPool;
        private readonly EcsPoolInject<T> _eventApplyDamagePool;

        private readonly UnityComponentMappedPool _mappedPool;

        public EventApplyDamageSystem(Context context) =>
            _mappedPool = context.Resolve<PoolService>().DontDisposablePool.BuildMappedPull();

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                UpdateEntity(i);
        }

        private void UpdateEntity(int entity)
        {
            var eventApplyDamage = _eventApplyDamagePool.Value.Get(entity).data;

            if (eventApplyDamage.GetDamagesCount() == 0)
                return;

            ref var hitPoint = ref _hitPointPool.Value.Get(entity);
            var totalPoint = Vector3.zero;

            for (int i = 0, iMax = eventApplyDamage.GetDamagesCount(); i < iMax; i++)
            {
                var (damage, point, owner, textEffectPrefab) = eventApplyDamage.Get(i);

                hitPoint.value -= damage;
                totalPoint += point;

                if (textEffectPrefab)
                    _mappedPool
                        .GetComponentByPrefab(textEffectPrefab, point, quaternion.identity)
                        .SetText(((int)damage).ToString());

                _causedDamagePool.Value.GetOrInitialize(owner).damages.Add(damage);
            }

            totalPoint /= eventApplyDamage.GetDamagesCount();
            _eventDamageTakenPool.Value.Add(entity);
            _eventRefreshHitPointValuePool.Value.AddIfNotExist(entity);

            if (hitPoint.value <= 0)
                _eventStartDiedPool.Value.AddIfNotExist(entity);

            if (!_pools.HitTakenEffect.Has(entity))
                return;

            var hitEffectSpawner = _pools.HitTakenEffect.Get(entity).hitEffectSpawner;
            hitEffectSpawner.transform.position = totalPoint;
            hitEffectSpawner.Execute();
        }
    }
}