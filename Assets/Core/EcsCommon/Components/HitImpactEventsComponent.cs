using System;
using Core.Systems;

namespace Core.Components
{
    [Serializable]
    public struct HitImpactEventsComponent
    {
        public AbstractEntityLogic targetEvents;
        public AbstractEntityLogic selfEvents;
    }
}