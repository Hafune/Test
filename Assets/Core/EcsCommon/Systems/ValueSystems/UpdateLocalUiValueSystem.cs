using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class UpdateLocalUiValueSystem<T> : IEcsRunSystem
        where T : struct, IValue
    {
        private readonly EcsFilterInject<Inc<T, EventValueUpdated<T>, UiValue<T>>> _valueRefreshFilter;
        // private readonly EcsFilterInject<Inc<T, EventGlobalUiValueUpdated<UiValue<T>>, UiValue<T>>> _eventRefreshFilter;

        private readonly EcsPoolInject<T> _valuePool;
        private readonly EcsPoolInject<UiValue<T>> _uiValuePool;
        // private readonly EcsPoolInject<EventGlobalUiValueUpdated<UiValue<T>>> _eventRefreshPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _valueRefreshFilter.Value)
                UpdateEntity(i);

            // foreach (var i in _eventRefreshFilter.Value)
            //     UpdateEntity(i);
            //
            // foreach (var i in _eventRefreshFilter.Value)
            //     _eventRefreshPool.Value.Del(i);
        }

        private void UpdateEntity(int entity) => _uiValuePool.Value.Get(entity).data
            .RefreshUiView(_valuePool.Value.Get(entity).value);
    }
}