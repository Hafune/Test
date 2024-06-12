using System;

namespace Core.Components
{
    [Serializable]
    public struct AreaResetReceiversComponent
    {
        public AbstractArea area;
        public float time;
    }
}