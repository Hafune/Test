using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class EventRemoveEntitySystem : IEcsRunSystem
    {
        private EcsWorldInject _world;
        
        private readonly EcsFilterInject<Inc<EventRemoveEntity>> _filter;

        private readonly EcsPoolInject<TransformComponent> _transformPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                if (_transformPool.Value.Has(i))
                    _transformPool.Value.Get(i).convertToEntity.RemoveConnectionInfo();
                
                _world.Value.DelEntity(i);
            }
        }
    }
}