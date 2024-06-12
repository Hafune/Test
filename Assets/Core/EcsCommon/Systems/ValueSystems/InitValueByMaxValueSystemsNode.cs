using System.Collections.Generic;
using Core.Components;
using Leopotam.EcsLite;

namespace Core.Systems
{
    public class InitValueByMaxValueSystemsNode
    {
        public IEnumerable<IEcsSystem> BuildSystems() =>
            new IEcsSystem[]
            {
                new InitValueByMaxValueSystem<HitPointValueComponent, HitPointMaxValueComponent>(),
                new InitValueByMaxValueSystem<ManaPointValueComponent, ManaPointMaxValueComponent>(),
                new InitValueByMaxValueSystem<HealingPotionValueComponent, HealingPotionMaxValueComponent>()
            };
    }
}