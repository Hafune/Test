using Core.Components;
using Core.Generated;
using Core.Services;
using Leopotam.EcsLite;
using Reflex;

namespace Core.Systems
{
    public class ActionSystemsService : IInitializableService
    {
        private ActionSystemsSchema _eventActionStartTypes;
        private EcsPool<ActionCurrentComponent> _currentActionPool;

        public void AddStartEvent(ActionEnum actionEnum, int entity, bool forceCompleteCurrent = false)
        {
            _eventActionStartTypes.AddEventActionStart(actionEnum, entity);

            if (!forceCompleteCurrent)
                return;
            
            ref var action = ref _currentActionPool.Get(entity);
            action.isCompleted = true;
            action.BTreeOnActionCompleted?.Invoke();
        }

        public void InitializeService(Context context)
        {
            var world = context.Resolve<EcsWorld>();
            _eventActionStartTypes = new(world);
            _currentActionPool = world.GetPool<ActionCurrentComponent>();
        }
    }
}