using System;
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Core.Systems
{
    public class SpawnOnHealthPercentSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                TransformComponent,
                SpawnOnHealthPercentComponent,
                HitPointValueComponent,
                // EventRefreshValue<HitPointValueComponent>,
                HitPointMaxValueComponent
            >,
            Exc<EventInit>> _filter;

        private readonly EcsPoolInject<SpawnOnHealthPercentComponent> _abilitySpawnOnHealthPercentPool;
        private readonly EcsPoolInject<TransformComponent> _transformPool;
        private readonly EcsPoolInject<HitPointValueComponent> _hitPointPool;
        private readonly EcsPoolInject<HitPointMaxValueComponent> _hitPointMaxPool;

        private Func<Transform, Vector3, Quaternion, int> _instantiateEntity;

        public SpawnOnHealthPercentSystem(Func<Transform, Vector3, Quaternion, int> instantiateEntity) =>
            _instantiateEntity = instantiateEntity;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                UpdateEntity(i);
        }

        private void UpdateEntity(int entity)
        {
            var percent = _hitPointPool.Value.Get(entity).value / _hitPointMaxPool.Value.Get(entity).value;
            ref var ability = ref _abilitySpawnOnHealthPercentPool.Value.Get(entity);

            while (ability.lastPercent > percent)
            {
                // var data = ability.spawnDatas.MaxByOrDefault(i => i.percent);
                //
                // if (data.percent < percent)
                //     return;
                //
                // ability.spawnDatas.Remove(data);
                // ability.lastPercent = data.percent;
                //
                // var prefabs = data.prefabs;
                // var pivot = _transformPool.Value.Get(entity).transform.position;
                //
                // foreach (var prefab in prefabs)
                //     _instantiateEntity.Invoke(prefab, pivot, Quaternion.identity);
                //
                // if (ability.spawnDatas.IsEmpty())
                // {
                //     _abilitySpawnOnHealthPercentPool.Value.Del(entity);
                //     return;
                // }
            }
        }
    }
}