using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class ActionDeathSystem : AbstractActionSystem<ActionDeathComponent>, IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ActionDeathComponent,
                EventActionStart<ActionDeathComponent>,
                ActionCurrentComponent
            >,
            Exc<EventInit, InProgressTag<ActionDeathComponent>>> _filter;

        private readonly EcsFilterInject<
            Inc<
                InProgressTag<ActionDeathComponent>
            >> _watchFilter;
        
        private readonly EcsFilterInject<Inc<EventDeath>> _eventDiedFilter;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _eventDiedFilter.Value)
                _pools.EventDeath.Del(i);

            foreach (var i in _filter.Value)
            {
                BeginActionProgress(i);
                _pools.EventDeath.Add(i);
            }

            foreach (var i in _watchFilter.Value)
                WatchEntity(i);

            CleanEventStart();
        }

        private void WatchEntity(int entity)
        {
            if (!_actionCurrentPool.Value.Get(entity).isCompleted)
                return;

            _pools.EventRemoveEntity.AddIfNotExist(entity);
        }
    }
}