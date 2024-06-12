using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class EventBehaviorTreeActionFailCheckSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ActionCurrentComponent,
                EventBehaviorTreeActionStartFailedCheck
            >> _filter;

        private readonly EcsFilterInject<Inc<EventBehaviorTreeActionStartFailedCheck>> _eventFilter;

        private readonly EcsPoolInject<ActionCurrentComponent> _actionCurrentPool;
        private readonly EcsPoolInject<EventBehaviorTreeActionStartFailedCheck> _eventBehaviorTreeActionFailCheckPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                _eventBehaviorTreeActionFailCheckPool.Value.Del(i);
                _actionCurrentPool.Value.Get(i).BTreeOnActionStartFailed?.Invoke();
            }
        }
    }
}