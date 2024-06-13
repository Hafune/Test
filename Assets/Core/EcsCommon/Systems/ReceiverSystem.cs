using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Reflex;

namespace Core.Systems
{
    public class ReceiverSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ActiveArea<ReceiverAreaComponent>
            >> _filter;

        private readonly ComponentPools _pools;
        private readonly GemService _gemService;

        public ReceiverSystem(Context context) => _gemService = context.Resolve<GemService>();

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                UpdateEntity(i);
        }

        private void UpdateEntity(int entity)
        {
            _gemService.TryChangeValue(1);
            _pools.EventRemoveEntity.AddIfNotExist(entity);
        }
    }
}