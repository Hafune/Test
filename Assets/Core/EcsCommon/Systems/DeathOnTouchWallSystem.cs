using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class DeathOnTouchWallSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                DeathOnTouchTag,
                ActionDeathComponent,
                EventTouchWall
            >> _deathFilter;

        private readonly EcsPoolInject<EventActionStart<ActionDeathComponent>> _eventStartDiedPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _deathFilter.Value)
                _eventStartDiedPool.Value.AddIfNotExist(i);
        }
    }
}