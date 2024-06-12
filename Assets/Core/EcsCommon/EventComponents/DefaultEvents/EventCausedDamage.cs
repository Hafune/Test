using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Voody.UniLeo.Lite;

namespace Core.Components
{
    [Serializable]
    public struct EventCausedDamage : IEcsAutoReset<EventCausedDamage>,
        IResetInProvider
    {
        public List<float> damages { get; set; }

        public void AutoReset(ref EventCausedDamage c)
        {
            c.damages ??= new();
            c.damages.Clear();
        }
    }
}