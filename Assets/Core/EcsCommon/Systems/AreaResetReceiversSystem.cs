using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Core.Systems
{
    public class AreaResetReceiversSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                AreaResetReceiversComponent
            >,
            Exc<InProgressTag<AreaResetReceiversComponent>>> _filter;

        private readonly EcsFilterInject<
            Inc<
                AreaResetReceiversComponent,
                InProgressTag<AreaResetReceiversComponent>
            >> _progressFilter;

        private readonly EcsPoolInject<AreaResetReceiversComponent> _pool;
        private readonly EcsPoolInject<InProgressTag<AreaResetReceiversComponent>> _progressPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                _progressPool.Value.Add(i);

            foreach (var i in _progressFilter.Value)
            {
                ref var timer = ref _pool.Value.Get(i);

                if ((timer.time -= Time.deltaTime) > 0)
                    continue;

                timer.area?.ResetReceivers();
                _pool.Value.Del(i);
                _progressPool.Value.Del(i);
            }
        }
    }
}