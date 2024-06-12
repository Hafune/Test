using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class ActionIdleSystem : AbstractActionSystem<ActionIdleComponent>, IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ActionIdleComponent,
                ActionCurrentComponent
            >,
            Exc<InProgressTag<ActionIdleComponent>>> _filter;

        private readonly EcsFilterInject<
            Inc<
                ActionIdleComponent,
                EventActionStart<ActionIdleComponent>,
                ActionCurrentComponent
            >,
            Exc<InProgressTag<ActionIdleComponent>>> _activateFilter;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                if (_actionCurrentPool.Value.Get(i).isCompleted)
                    UpdateEntity(i);

            foreach (var i in _activateFilter.Value)
                if (_actionPool.Value.Get(i).logic.CheckConditionLogic(i))
                    UpdateEntity(i);
            
            CleanEventStart();
        }

        private void UpdateEntity(int entity)
        {
            BeginActionProgress(entity);
            ref var actionCurrent = ref _actionCurrentPool.Value.Get(entity);
            actionCurrent.isCompleted = true;
            actionCurrent.canBeCanceled = true;
        }
    }
}