using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class EventSetupModuleSystem<T> : IEcsRunSystem where T : struct, IModuleTriggerComponent
    {
        private readonly EcsFilterInject<
            Inc<
                EventSetupModule<T>,
                ModulesComponent
            >> _filter;

        private readonly EcsPoolInject<EventSetupModule<T>> _eventPool;
        private readonly EcsPoolInject<T> _triggerPool;
        private readonly EcsPoolInject<InProgressTag<T>> _inProgressPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                foreach (var module in _eventPool.Value.Get(i).modules)
                {
                    module.SetupLogic(i);
                    _triggerPool.Value.Get(i).Modules.Add(module);
                    
                    if (_inProgressPool.Value.Has(i))
                        module.StartLogic(i);
                }
                
                _eventPool.Value.Del(i);
            }
        }
    }
}