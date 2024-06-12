using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class NpcActionSystem : AbstractActionSystem<NpcActionComponent>, IEcsRunSystem
    {
        protected readonly EcsFilterInject<
            Inc<
                NpcActionComponent,
                EventActionStart<NpcActionComponent>,
                ActionCurrentComponent
            >> _activateFilter;

        private readonly EcsFilterInject<
            Inc<
                NpcActionComponent,
                InProgressTag<ActionMoveComponent>
            >> _progressFilter;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _activateFilter.Value)
            {
                ref var action = ref _actionPool.Value.Get(i);

                if (!action.nextLogic.CheckConditionLogic(i))
                    continue;

                ref var current = ref _actionCurrentPool.Value.Get(i);
                current.ChangeAction(this, i);
                current.isCompleted = false;
                _inProgressPool.Value.Add(i);

                action.logic = action.nextLogic;
                action.logic?.StartLogic(i);

                current.BTreeOnActionStart?.Invoke();
                _eventBehaviorTreeActionFailCheckPool.Value.DelIfExist(i);
            }

            foreach (var i in _progressFilter.Value)
                _actionPool.Value.Get(i).logic.UpdateLogic(i);

            CleanEventStart();
        }
    }
}