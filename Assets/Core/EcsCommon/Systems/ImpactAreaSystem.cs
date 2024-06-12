using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class ImpactAreaSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ActiveArea<ImpactAreaComponent>,
                ImpactAreaComponent,
                HitImpactEventsComponent
            >,
            Exc<CriticalChanceValueComponent>> _filter;

        private readonly EcsPoolInject<ImpactAreaComponent> _areaPool;
        private readonly EcsPoolInject<HitImpactEventsComponent> _hitImpactPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                var area = _areaPool.Value.Get(i).area;
                var hitImpactController = _hitImpactPool.Value.Get(i);
                var targetEvents = hitImpactController.targetEvents;

                hitImpactController.selfEvents?.Run(i);
                area.ForEachEntity(targetEvents.Run);
            }
        }
    }
}