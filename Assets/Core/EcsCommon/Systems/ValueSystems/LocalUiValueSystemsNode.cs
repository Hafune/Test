using System.Collections.Generic;
using Core.Components;
using Leopotam.EcsLite;

namespace Core.Systems
{
    public class LocalUiValueSystemsNode
    {
        public IEnumerable<IEcsSystem> BuildSystems()
        {
            return new IEcsSystem[]
            {
                new UpdateLocalUiValueSystem<HitPointValueComponent>(),
                new UpdateLocalUiValueSystem<HitPointMaxValueComponent>(),
                new UpdateLocalUiValueSystem<ManaPointValueComponent>(),
                new UpdateLocalUiValueSystem<ManaPointMaxValueComponent>(),
            };
        }
    }
}