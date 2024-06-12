using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    //ByTag компонент для связи
    public class UpdateGlobalUiValueSystem<ByTag, T> : IEcsRunSystem
        where ByTag : struct
        where T : struct, IValue
    {
        private readonly EcsFilterInject<
            Inc<
                ByTag,
                T,
                EventValueUpdated<T>
            >> _valueRefreshFilter;

        private readonly EcsFilterInject<
            Inc<
                GlobalUiLink<ByTag>,
                UiValue<T>
            >> _uiFilter;

        private readonly EcsFilterInject<
            Inc<
                ByTag,
                T>> _filter;

        private readonly EcsFilterInject<
            Inc<
                ByTag,
                T,
                EventGlobalUiValueUpdated<T>
            >> _eventRefreshFilter;

        private readonly EcsFilterInject<
            Inc<
                EventGlobalUiValueUpdated<T>
            >> _eventRemoveFilter;

        private readonly EcsPoolInject<T> _valuePool;
        private readonly EcsPoolInject<UiValue<T>> _uiValuePool;
        private readonly EcsPoolInject<EventGlobalUiValueUpdated<T>> _eventRefreshPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _valueRefreshFilter.Value)
            foreach (var ui in _uiFilter.Value)
                UpdateEntity(i, ui);

            foreach (var i in _eventRefreshFilter.Value)
            foreach (var ui in _uiFilter.Value)
                UpdateEntity(i, ui);

            foreach (var i in _eventRemoveFilter.Value)
                _eventRefreshPool.Value.Del(i);
        }

        private void UpdateEntity(int entity, int uiEntity) => _uiValuePool.Value.Get(uiEntity).data
            .RefreshUiView(_valuePool.Value.Get(entity).value);
    }
}