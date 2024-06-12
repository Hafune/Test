using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using UnityEngine;

namespace Core.Systems
{
    public class MagnetSystem : IEcsRunSystem
    {
        private float _acceleration = 10f;
        private float _maxSpeed = 15;
        private float _maxFreeSpeed = 5;

        private readonly EcsFilterInject<
            Inc<
                MagnetTag,
                RigidbodyComponent,
                EventMagnetAreaTouch>,
            Exc<InProgressTag<MagnetTag>>> _filter;

        private readonly EcsFilterInject<
            Inc<
                InProgressTag<MagnetTag>,
                RigidbodyComponent>> _progressFilter;

        private readonly EcsFilterInject<
            Inc<
                PlayerUniqueTag,
                TransformCenterComponent>> _playerFilter;

        private readonly EcsFilterInject<Inc<EventMagnetAreaTouch>> _eventTouchFilter;

        private readonly EcsPoolInject<EventMagnetAreaTouch> _eventMagnetAreaTouchPool;
        private readonly EcsPoolInject<RigidbodyComponent> _rigidbodyPool;
        private readonly EcsPoolInject<InProgressTag<MagnetTag>> _progressPool;
        private readonly EcsPoolInject<TransformCenterComponent> _transformCenterPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                _progressPool.Value.Add(i);

            foreach (var i in _progressFilter.Value)
                UpdateEntity(i);

            foreach (var i in _eventTouchFilter.Value)
                _eventMagnetAreaTouchPool.Value.Del(i);
        }

        private void UpdateEntity(int entity)
        {
            var playerEntity = _playerFilter.Value.GetFirst();
            var transform = _transformCenterPool.Value.Get(playerEntity).transform;
            var rigidbody = _rigidbodyPool.Value.Get(entity).rigidbody;

            var position = transform.position;
            var line = position - rigidbody.position;
            var freeVelocity = rigidbody.velocity - Physics.gravity * Time.deltaTime;

            if (freeVelocity.magnitude > _maxFreeSpeed)
                freeVelocity = freeVelocity.normalized * _maxFreeSpeed;

            var velocity = freeVelocity +
                           line.normalized * (rigidbody.velocity.magnitude + _acceleration * Time.deltaTime);

            if (velocity.magnitude > _maxSpeed)
                velocity = velocity.normalized * _maxSpeed;

            rigidbody.velocity = velocity;
        }
    }
}