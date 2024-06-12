using Core.Components;
using Core.Lib;
using Leopotam.EcsLite;

namespace Core.Systems
{
    public struct SlotTagPoolsComponent<T> : IEcsAutoReset<SlotTagPoolsComponent<T>> where T : ISlotData
    {
        public MyList<IEcsPool> pools;

        public void AutoReset(ref SlotTagPoolsComponent<T> c)
        {
            c.pools ??= new();
            c.pools.Clear();
        }
    }
}