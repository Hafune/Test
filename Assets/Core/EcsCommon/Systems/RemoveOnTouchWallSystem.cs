using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class RemoveOnTouchWallSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                RemoveOnTouchTag,
                EventTouchWall
            >> _destroyFilter;

        private readonly EcsPoolInject<EventRemoveEntity> _eventRemovePool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _destroyFilter.Value)
                _eventRemovePool.Value.AddIfNotExist(i);
        }
    }
}