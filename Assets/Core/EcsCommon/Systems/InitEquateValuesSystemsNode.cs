using System.Collections.Generic;
using Core.Components;
using Leopotam.EcsLite;

namespace Core.Systems
{
    public class InitEquateValuesSystemsNode
    {
        public IEnumerable<IEcsSystem> BuildSystems() => new IEcsSystem[]
        {
            //Установка макс значений при инициализации
            new InitEquateValueToMaxValueSystem<HitPointMaxValueComponent, HitPointValueComponent>(),
            new InitEquateValueToMaxValueSystem<ManaPointMaxValueComponent, ManaPointValueComponent>(),
            new InitEquateValueToMaxValueSystem<HealingPotionMaxValueComponent, HealingPotionValueComponent>(),
        };
    }
}