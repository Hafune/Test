using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Core.Systems
{
    public class MagnetSystem<T> : IEcsRunSystem where T : struct, IAreaComponent
    {
        private float _acceleration = 10f;
        private float _maxSpeed = 15;
        private float _maxFreeSpeed = 5;

        private readonly EcsFilterInject<
            Inc<
                T,
                MagnetTag,
                RigidbodyComponent,
                ActiveArea<T>>> _filter;

        private readonly EcsPoolInject<T> _areaPool;
        private readonly ComponentPools _pools;
        private Rigidbody _rigidbody;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                _rigidbody = _pools.Rigidbody.Get(i).rigidbody;
                _areaPool.Value.Get(i).area.ForEachEntity(UpdateEntity);
            }
        }

        private void UpdateEntity(int targetEntity)
        {
            var targetTransform = _pools.TransformCenter.Get(targetEntity).transform;

            var position = targetTransform.position;
            var line = position - _rigidbody.position;
            var freeVelocity = _rigidbody.velocity - Physics.gravity * Time.deltaTime;

            if (freeVelocity.magnitude > _maxFreeSpeed)
                freeVelocity = freeVelocity.normalized * _maxFreeSpeed;

            var velocity = freeVelocity +
                           line.normalized * (_rigidbody.velocity.magnitude + _acceleration * Time.deltaTime);

            if (velocity.magnitude > _maxSpeed)
                velocity = velocity.normalized * _maxSpeed;

            _rigidbody.velocity = velocity;
        }
    }
}