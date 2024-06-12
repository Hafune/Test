using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using UnityEngine;

namespace Core.Systems
{
    public class LifetimeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<LifetimeComponent>> _filter;

        private readonly EcsPoolInject<EventRemoveEntity> _eventRemovePool;
        private readonly EcsPoolInject<LifetimeComponent> _lifetimePool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                UpdateEntity(i);
        }

        private void UpdateEntity(int entity)
        {
            ref var lifetimeComponent = ref _lifetimePool.Value.Get(entity);
            lifetimeComponent.currentTime += Time.deltaTime;

            if (lifetimeComponent.currentTime < lifetimeComponent.maxTime)
                return;

            _eventRemovePool.Value.AddIfNotExist(entity);
            _lifetimePool.Value.Del(entity);
        }
    }
}