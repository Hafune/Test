using System;
using System.Collections.Generic;
using Core.EntityModules;
using Leopotam.EcsLite;

namespace Core.Components
{
    [Serializable]
    public struct EventSetupModule<T> : IEcsAutoReset<EventSetupModule<T>> where T : struct, IModuleTriggerComponent
    {
        public List<AbstractEntityModule> modules;

        public void AutoReset(ref EventSetupModule<T> c)
        {
            c.modules ??= new();
            c.modules.Clear();
        }
    }
}