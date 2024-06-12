using System;

namespace Core.Components
{
    [Serializable]
    public struct EventCameraShakeComponent
    {
        public float maxTime;
        public float maxIntensity;
    }
}