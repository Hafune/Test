using System;

namespace Core.Components
{
    [Serializable]
    public struct ExperienceValueComponent : IValue
    {
        public float value { get; set; }
    }
}