using System;
using UnityEngine;

namespace Core.Components
{
    [Serializable]
    public struct AngularSpeedValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct AttackSpeedValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct EnergyValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct EnergyMaxValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct CriticalChanceValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct CriticalDamageValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct HealingPotionMaxValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct HealingPotionValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct HitPointMaxValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct HitPointValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct HitPointPercentValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct ManaPointMaxValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct ManaPointValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct MoveSpeedValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct DamageValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
    }

    [Serializable]
    public struct LivesValueComponent : IValue
    {
        [field: SerializeField] public float value { get; set; }
        public Action onLivesEnded;
    }

    [Serializable]
    public struct DefenceValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct DamagePercentValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct ScoreValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct ShockWaveValueComponent : IValue
    {
        public float value { get; set; }
    }

    [Serializable]
    public struct DamageOrbValueComponent : IValue
    {
        public float value { get; set; }
    }
}