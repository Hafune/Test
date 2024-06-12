using Core.Lib;
using Leopotam.EcsLite;

namespace Core.Components
{
    public struct DamageOrbParentComponent : IEcsAutoReset<DamageOrbParentComponent>
    {
        public float time;
        public int orbIndex;
        public MyList<int> orbsStates;
        public MyList<int> orbEntities;

        public void AutoReset(ref DamageOrbParentComponent c)
        {
            c.time = 0;
            c.orbIndex = 0;
            c.orbsStates ??= new ();
            c.orbsStates.Clear();
            c.orbEntities ??= new ();
            c.orbEntities.Clear();
        }
    }
}