using System;
using Core.Systems;

namespace Core.Components
{
    [Serializable]
    public struct PlayerInputMemoryComponent
    {
        // [field: SerializeField] public float storageTime { get; private set; }
        [NonSerialized] public float inputTime;
        [NonSerialized] public Action<int> AddEventStartAction;
        public IAbstractActionSystem lastActionSystem;
    }
}