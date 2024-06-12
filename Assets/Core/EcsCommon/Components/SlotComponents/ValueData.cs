using System;
using Core.Systems;

namespace Core.Components
{
    [Serializable]
    public struct ValueData
    {
        public ValueEnum valueEnum;
        public float value;
    }
}