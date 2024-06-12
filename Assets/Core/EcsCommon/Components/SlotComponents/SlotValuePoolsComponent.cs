using System.Collections.Generic;
using Core.Components;
using Leopotam.EcsLite;
using Lib;

namespace Core.Systems
{
    public struct SlotValuePoolsComponent<T> : IEcsAutoReset<SlotValuePoolsComponent<T>> where T : ISlotData
    {
        private List<IEcsPool> valuePools;
        private List<IEcsPool> startRecalculatePools;

        public void AutoReset(ref SlotValuePoolsComponent<T> c)
        {
            c.valuePools ??= new();
            c.valuePools.Clear();

            c.startRecalculatePools ??= new();
            c.startRecalculatePools.Clear();
        }

        public void AddPools(
            IEcsPool valuePool,
            IEcsPool eventPool
        )
        {
            valuePools.Add(valuePool);
            startRecalculatePools.Add(eventPool);
        }

        public void DelAndStartRecalculate(int entity)
        {
            for (int index = 0, aMax = valuePools.Count; index < aMax; index++)
            {
                valuePools[index].Del(entity);
                startRecalculatePools[index].AddIfNotExist(entity);
            }
        }
        
        public void Clear()
        {
            valuePools.Clear();
            startRecalculatePools.Clear();
        }
    }
}