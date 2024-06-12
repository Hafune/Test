using System.Collections.Generic;
using Core.Generated;
using Leopotam.EcsLite;

namespace Core.Systems
{
    public class RecalculateValueSystemsNode
    {
        public IEnumerable<IEcsSystem> BuildSystems() => new IEcsSystem[]
        {
            new InitRecalculateAllValuesSystem(),
            new EventStartRecalculateValueSystem(),
            new DelEventStartRecalculateValue(),
        };
    }
}