using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.VisualScripting;

namespace Core.Systems
{
    public class RemoveModulesBeforeRemoveEntitySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ModulesComponent,
                EventRemoveEntity
            >> _filter;

        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var modules = _pools.Modules.Get(i).modules;
                foreach (var module in modules)
                    module.RemoveLogic(i);
                
                modules.Clear();
            }
        }
    }
}