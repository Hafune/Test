using System.Runtime.CompilerServices;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public interface IAbstractActionSystem
    {
        void Cancel(int entity);
    }

    public abstract class AbstractActionSystem<T> : IAbstractActionSystem where T : struct, IActionComponent
    {
        protected readonly EcsFilterInject<Inc<EventActionStart<T>>> _eventStartFilter;

        protected readonly EcsPoolInject<InProgressTag<T>> _inProgressPool;
        protected readonly EcsPoolInject<ActionCurrentComponent> _actionCurrentPool;
        protected readonly EcsPoolInject<T> _actionPool;
        protected readonly EcsPoolInject<EventBehaviorTreeActionStartFailedCheck> _eventBehaviorTreeActionFailCheckPool;
        protected readonly EcsPoolInject<EventActionStart<T>> _eventStartPool;

        protected ComponentPools _pools;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void CleanEventStart()
        {
            foreach (var i in _eventStartFilter.Value)
                _eventStartPool.Value.Del(i);
        }

        protected void BeginActionProgress(int entity)
        {
            ref var current = ref _actionCurrentPool.Value.Get(entity);
            current.ChangeAction(this, entity);
            current.isCompleted = false;
            _inProgressPool.Value.Add(entity);

            var action = _actionPool.Value.Get(entity);
            current.actionEnum = action.actionEnum;
            var logic = action.logic;
            logic.ResetOnStart(entity);
            logic.StartLogic(entity);

            if (current.BTreeDesiredActionType != action.actionEnum)
                return;

            current.BTreeOnActionStart?.Invoke();
            _eventBehaviorTreeActionFailCheckPool.Value.DelIfExist(entity);
        }

        public virtual void Cancel(int entity)
        {
            _actionPool.Value.Get(entity).logic?.CancelLogic(entity);
            _inProgressPool.Value.Del(entity);
        }
    }
}