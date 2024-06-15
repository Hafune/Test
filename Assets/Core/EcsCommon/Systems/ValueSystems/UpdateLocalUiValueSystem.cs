using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class UpdateLocalUiValueSystem<T> : IEcsRunSystem
        where T : struct, IValue
    {
        private readonly EcsFilterInject<Inc<T, EventValueUpdated<T>, UiValue<T>>> _valueRefreshFilter;

        private readonly EcsPoolInject<T> _valuePool;
        private readonly EcsPoolInject<UiValue<T>> _uiValuePool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _valueRefreshFilter.Value)
                _uiValuePool.Value.Get(i).update(_valuePool.Value.Get(i));
        }
    }
}