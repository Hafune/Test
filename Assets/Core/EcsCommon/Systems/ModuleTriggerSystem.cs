using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Systems
{
    public class ModuleTriggerSystem<T> : IEcsRunSystem where T : struct, IModuleTriggerComponent
    {
        private readonly EcsFilterInject<
            Inc<
                T,
                EventStartProgress<T>
            >,
            Exc<InProgressTag<T>>> _startFilter;

        private readonly EcsFilterInject<
            Inc<
                T,
                InProgressTag<T>
            >> _progressFilter;

        private readonly EcsFilterInject<
            Inc<
                T,
                InProgressTag<T>,
                EventCancelProgress<T>
            >> _cancelFilter;

        private readonly EcsPoolInject<T> _pool;
        private readonly EcsPoolInject<EventStartProgress<T>> _startPool;
        private readonly EcsPoolInject<InProgressTag<T>> _progressPool;
        private readonly EcsPoolInject<EventCancelProgress<T>> _cancelPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _startFilter.Value)
            {
                foreach (var module in _pool.Value.Get(i).Modules)
                    module.StartLogic(i);

                _startPool.Value.Del(i);
                _progressPool.Value.Add(i);
            }

            foreach (var i in _progressFilter.Value)
            foreach (var module in _pool.Value.Get(i).Modules)
                module.UpdateLogic(i);

            foreach (var i in _cancelFilter.Value)
            {
                foreach (var module in _pool.Value.Get(i).Modules)
                    module.CancelLogic(i);

                _progressPool.Value.Del(i);
                _cancelPool.Value.Del(i);
            }
        }
    }
}