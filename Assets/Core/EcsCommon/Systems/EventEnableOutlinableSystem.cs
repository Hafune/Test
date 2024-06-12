using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class EventEnableOutlinableSystem : IEcsRunSystem
    {
        private readonly
            EcsFilterInject<
                Inc<
                    EventEnableOutlinable,
                    OutlinableComponent,
                    WriteDefaultsBeforeRemoveEntityComponent
                >,
                Exc<InProgressTag<OutlinableComponent>>> _filter;

        private readonly EcsFilterInject<Inc<EventEnableOutlinable>> _eventFilter;

        private readonly EcsPoolInject<EventEnableOutlinable> _eventEnableOutlinablePool;
        private readonly EcsPoolInject<OutlinableComponent> _outlinablePool;
        private readonly EcsPoolInject<InProgressTag<OutlinableComponent>> _progressPool;
        private readonly EcsPoolInject<WriteDefaultsBeforeRemoveEntityComponent> _writeDefaultsBeforeRemoveEntityPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
            {
                _outlinablePool.Value.Get(i).outlinable.enabled = true;
                _progressPool.Value.Add(i);
                _writeDefaultsBeforeRemoveEntityPool.Value.Get(i).writeDefaults += Reset;
            }

            foreach (var i in _eventFilter.Value)
                _eventEnableOutlinablePool.Value.Del(i);
        }

        private void Reset(int i) => _outlinablePool.Value.Get(i).outlinable.enabled = false;
    }
}