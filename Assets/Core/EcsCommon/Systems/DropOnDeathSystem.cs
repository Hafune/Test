using System;
using Core.Components;
using Core.Generated;
using Core.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Reflex;

namespace Core.Systems
{
    public class DropOnDeathSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                DropOnDeathComponent,
                EventDeath
            >,
            Exc<EventInit>> _filter;

        private readonly DropService _dropService;
        private readonly ComponentPools _pools;

        public DropOnDeathSystem(Context context) => _dropService = context.Resolve<DropService>();

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                UpdateEntity(i);
        }

        private void UpdateEntity(int entity)
        {
            var dropOnDeath = _pools.DropOnDeath.Get(entity);

            _dropService.SpawnUnitDrop(
                _pools.Transform.Get(entity).convertToEntity.TemplateId,
                dropOnDeath.spawnPoint.position,
                dropOnDeath.data
            );

            _dropService.SpawnSceneDrop(
                _pools.Transform.Get(entity).convertToEntity.TemplateId,
                dropOnDeath.spawnPoint.position,
                Array.Empty<DropGroup>()
            );
        }
    }
}