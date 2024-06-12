using Core.Components;

namespace Core.Systems
{
    public struct SlotValueComponent<S, T> : ISlotValue
        where S : struct, ISlotData
        where T : struct, IValue
    {
        public float value { get; set; }
    }
}