using Leopotam.EcsLite;
using Voody.UniLeo.Lite;

namespace Core.Components
{
    public struct EventIncomingDamage : IEcsAutoReset<EventIncomingDamage>, IEventDamageData, IResetInProvider
    {
        public DamageData data { get; private set; }

        public void AutoReset(ref EventIncomingDamage c)
        {
            c.data ??= new();
            c.data.AutoReset();
        }
    }
}