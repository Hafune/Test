using System;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using UnityEngine;

namespace Core.Systems
{
    public class PickCollectableSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                CollectableAreaComponent,
                MagnetAreaComponent,
                ActiveArea<CollectableAreaComponent>,
                RigidbodyComponent
            >> _filter;

        private readonly EcsFilterInject<
            Inc<
                ReceiverClientComponent,
                TransformComponent
            >> _itemsStackFilter;

        private readonly ComponentPools _pools;
        private Rigidbody _rigidbody;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                UpdateEntity(i);
        }

        private void UpdateEntity(int entity)
        {
            _rigidbody = _pools.Rigidbody.Get(entity).rigidbody;
            _pools.CollectableArea.Get(entity).area.ForEachEntity(Pick);
            _pools.CollectableArea.Del(entity);
            _pools.MagnetArea.Del(entity);
        }

        private void Pick(int targetEntity)
        {
#if UNITY_EDITOR
            if (!_itemsStackFilter.Value.HasEntity(targetEntity))
                throw new Exception("Нет необходимых компонентов!!!");
#endif
            _pools.ReceiverClient.Get(targetEntity).receiverClient.AddItem(_rigidbody);
        }
    }
}