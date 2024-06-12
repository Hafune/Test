using System.Collections.Generic;
using Core.Components;
using Leopotam.EcsLite;

namespace Core.Systems
{
    public class MathValueSystemsNode
    {
        public IEnumerable<IEcsSystem> BuildSystems() => new IEcsSystem[]
        {
            new MultipleValueByValueSystem<DamageValueComponent, DamagePercentValueComponent>(),
            new MultipleValueByValueSystem<HitPointMaxValueComponent, HitPointPercentValueComponent>()
        };
    }
}