using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class InitValueByMaxValueSystem<V, M> : IEcsRunSystem
        where V : struct, IValue
        where M : struct, IValue
    {
        private readonly EcsFilterInject<Inc<EventInit, M>, Exc<V>> _maxValueFilter;

        private readonly EcsPoolInject<V> _valuePool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _maxValueFilter.Value)
                _valuePool.Value.Add(i);
        }
    }
}