using System.Collections.Generic;
using Core.EntityModules;
using Leopotam.EcsLite;

namespace Core.Components
{
    public struct ModulesComponent : IEcsAutoReset<ModulesComponent>
    {
        public List<AbstractEntityModule> modules;
        
        public void AutoReset(ref ModulesComponent c)
        {
            c.modules ??= new();
            c.modules.Clear();
        }
    }
}