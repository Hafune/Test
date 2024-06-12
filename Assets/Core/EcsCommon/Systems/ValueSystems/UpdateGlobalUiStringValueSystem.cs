using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class UpdateGlobalUiStringValueSystem<ByTag, T> : IEcsRunSystem
        where ByTag : struct
        where T : struct, IStringValue
    {
        private readonly EcsFilterInject<Inc<ByTag, T, EventStringValueUpdated<T>>> _valueRefreshFilter;
        private readonly EcsFilterInject<Inc<GlobalUiLink<ByTag>, UiStringValue<T>>> _uiFilter;

        private readonly EcsFilterInject<Inc<ByTag, T>> _filter;

        private readonly EcsFilterInject<
            Inc<
                GlobalUiLink<ByTag>,
                UiStringValue<T>,
                EventUiStringValueUpdated<UiStringValue<T>>
            >> _eventRefreshFilter;

        private readonly EcsPoolInject<T> _valuePool;
        private readonly EcsPoolInject<UiStringValue<T>> _uiValuePool;
        private readonly EcsPoolInject<EventUiStringValueUpdated<UiStringValue<T>>> _eventRefreshPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _valueRefreshFilter.Value)
            foreach (var ui in _uiFilter.Value)
                UpdateEntity(i, ui);

            foreach (var ui in _eventRefreshFilter.Value)
            foreach (var i in _filter.Value)
                UpdateEntity(i, ui);

            foreach (var i in _eventRefreshFilter.Value)
                _eventRefreshPool.Value.Del(i);
        }

        private void UpdateEntity(int entity, int uiEntity) => _uiValuePool.Value.Get(uiEntity).data
            .RefreshUiView(_valuePool.Value.Get(entity).value);
    }
}