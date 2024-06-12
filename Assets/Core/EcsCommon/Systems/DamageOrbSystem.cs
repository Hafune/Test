using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using UnityEngine;

namespace Core.Systems
{
    public class DamageOrbSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ParentComponent,
                DamageOrbComponent,
                DamageAreaComponent,
                TransformComponent,
                TransformCenterComponent
            >> _filter;

        private readonly EcsFilterInject<
            Inc<
                DamageOrbComponent
            >,
            Exc<ParentComponent>> _removeFilter;

        private readonly ComponentPools _pools;
        private const float Radius = 1.5f;
        private const float CircleLength = Mathf.PI * 2;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var transform = _pools.Transform.Get(i).transform;
                var center = _pools.TransformCenter.Get(i).transform.position;
                ref var orb = ref _pools.DamageOrb.Get(i);
                orb.rotationProgress += Time.deltaTime * orb.rotationPerSecond;

                if (orb.rotationProgress > 1)
                    orb.rotationProgress -= 1;

                float angle = CircleLength * orb.rotationProgress;

                // Вычисляем координаты точки на окружности
                float x = Radius * Mathf.Sin(angle);
                float y = Radius * Mathf.Cos(angle);
                transform.position = center + new Vector3(x, y, 0);

                if (orb.lastX.Sign() != x.Sign())
                    _pools.DamageArea.Get(i).area.ResetReceivers();

                orb.lastX = x;
            }

            foreach (var i in _removeFilter.Value)
                _pools.EventRemoveEntity.Add(i);
        }
    }
}