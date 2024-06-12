using Core.Generated;
using Core.Systems;

namespace Core.Components
{
    public interface ISlotData
    {
        public ValueData[] values { get; set; }
        public SlotTagEnum[] tags { get; set; }
    }
}