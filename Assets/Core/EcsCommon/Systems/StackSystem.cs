using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Core.Systems
{
    public class StackSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ParentComponent,
                RigidbodyComponent,
                StackComponent
            >,
            Exc<
                ActiveArea<ReceiverAreaComponent>
            >> _filter;

        private readonly ComponentPools _pools;
        private const float _destinationTime = .03f;
        private Vector3 _itemOffset = new(0, .5f, 0);

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                UpdateEntity(i);
        }

        private void UpdateEntity(int entity)
        {
            var item = _pools.Rigidbody.Get(entity).rigidbody;
            var position = _pools.Stack.Get(entity).targetTransform.position;
            var distance = position + _itemOffset - item.position;
            item.velocity = distance / _destinationTime;
        }
    }
}