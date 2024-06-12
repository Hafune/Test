using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Reflex;

namespace Core.Systems
{
    public class ShockWaveMissileSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ShockWaveSlotTag,
                ShockWaveValueComponent,
                ShockWavePrefabComponent,
                TransformCenterComponent,
                EventSlash
            >> _filter;

        private readonly EcsFilterInject<
            Inc<
                EventSlash
            >> _eventFilter;

        private readonly ComponentPools _pools;
        private ChildEntityBuilder _childEntityBuilder;
        private float _force = 20;
        private float _baseDamagePercent = .1f;

        public ShockWaveMissileSystem(Context context) => _childEntityBuilder = new(context);

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                UpdateEntity(i);

            foreach (var i in _eventFilter.Value)
                _pools.EventSlash.Del(i);
        }

        private void UpdateEntity(int entity)
        {
            var prefab = _pools.ShockWavePrefab.Get(entity).prefab;
            var center = _pools.TransformCenter.Get(entity).transform;
            var value = _pools.ShockWaveValue.Get(entity).value;
            var data = _childEntityBuilder.BuildEvent(prefab, center, entity);

            var velocity = center.right * _force;
            data.AddComponent(new EventSetupVelocity { velocity = velocity }, _pools.EventSetupVelocity);
            data.AddComponent(new DamageScaleComponent { value = _baseDamagePercent + value }, _pools.DamageScale);
        }
    }
}