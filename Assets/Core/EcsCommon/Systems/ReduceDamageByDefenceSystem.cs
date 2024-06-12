using System;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class ReduceDamageByDefenceSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<EventIncomingDamage, DefenceValueComponent>> _filter;

        private readonly ComponentPools _pools;

        private readonly float _overValue = 100;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var data = _pools.EventIncomingDamage.Get(i).data;
                var defence = _pools.DefenceValue.Get(i).value;
                var reducePercent = 1 - defence / Math.Max(defence + _overValue, _overValue);

                foreach (var index in data)
                    data.UpdateDamageValue(index, data.GetDamageAt(index) * reducePercent);
            }
        }
    }
}