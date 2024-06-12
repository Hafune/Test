using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class InitEquateValueToMaxValueSystem<T, V> : IEcsRunSystem
        where T : struct, IValue
        where V : struct, IValue
    {
        private readonly EcsFilterInject<Inc<EventInit, V, T>>
            _hitPointFilter;

        private readonly EcsPoolInject<T> _fromPool;
        private readonly EcsPoolInject<V> _toPool;
        private readonly EcsPoolInject<EventValueUpdated<V>> _eventRefreshValuePool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _hitPointFilter.Value)
            {
                _toPool.Value.Get(i).value = _fromPool.Value.Get(i).value;
                _eventRefreshValuePool.Value.AddIfNotExist(i);
            }
        }
    }
}