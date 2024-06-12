using System;

namespace Core.Components
{
    [Serializable]
    public struct CollectableGemComponent
    {
        public int count;
        public GemInstance instance;
    }
}