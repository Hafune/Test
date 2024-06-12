using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class MultipleValueByValueSystem<V, M> : IEcsRunSystem
        where V : struct, IValue
        where M : struct, IValue
    {
        private readonly EcsFilterInject<
            Inc<
                EventValueUpdated<V>, M, V
            >> _filter;

        private readonly EcsFilterInject<
            Inc<
                EventValueUpdated<M>, M, V
            >,
            Exc<
                EventValueUpdated<V>
            >> _eventMultFilter;

        private readonly EcsPoolInject<V> _pool;
        private readonly EcsPoolInject<M> _byPool;
        private readonly EcsPoolInject<EventStartRecalculateValue<V>> _eventPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                _pool.Value.Get(i).value += _pool.Value.Get(i).value * _byPool.Value.Get(i).value;

            foreach (var i in _eventMultFilter.Value)
                _eventPool.Value.AddIfNotExist(i);
        }
    }
}