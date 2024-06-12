using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class EventTimelineActionSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<EventTimelineAction>> _timelineActionFilter;

        private readonly ComponentPools _pools;
        
        public void Run(IEcsSystems systems)
        {            
            foreach (var i in _timelineActionFilter.Value)
                _pools.EventTimelineAction.Get(i).logic?.Invoke(i);
        }
    }
}