//Файл генерируется в GenSlotValueTypes
using System;
using Core.Components;
using Core.Systems;

namespace Core.Generated
{
    public static class SlotValueTypes
    {
        // @formatter:off
        public static Type SlotValueType<T>(ValueEnum @enum)
            where T : struct, ISlotData => @enum switch
        {
            ValueEnum.AddScoreOnDeathValue => typeof(SlotValueComponent<T, AddScoreOnDeathValueComponent>),
            ValueEnum.AngularSpeedValue => typeof(SlotValueComponent<T, AngularSpeedValueComponent>),
            ValueEnum.AttackSpeedValue => typeof(SlotValueComponent<T, AttackSpeedValueComponent>),
            ValueEnum.CriticalChanceValue => typeof(SlotValueComponent<T, CriticalChanceValueComponent>),
            ValueEnum.CriticalDamageValue => typeof(SlotValueComponent<T, CriticalDamageValueComponent>),
            ValueEnum.DamageOrbValue => typeof(SlotValueComponent<T, DamageOrbValueComponent>),
            ValueEnum.DamagePercentValue => typeof(SlotValueComponent<T, DamagePercentValueComponent>),
            ValueEnum.DamageValue => typeof(SlotValueComponent<T, DamageValueComponent>),
            ValueEnum.DefenceValue => typeof(SlotValueComponent<T, DefenceValueComponent>),
            ValueEnum.EnergyMaxValue => typeof(SlotValueComponent<T, EnergyMaxValueComponent>),
            ValueEnum.EnergyValue => typeof(SlotValueComponent<T, EnergyValueComponent>),
            ValueEnum.ExperienceValue => typeof(SlotValueComponent<T, ExperienceValueComponent>),
            ValueEnum.HealingPotionMaxValue => typeof(SlotValueComponent<T, HealingPotionMaxValueComponent>),
            ValueEnum.HealingPotionValue => typeof(SlotValueComponent<T, HealingPotionValueComponent>),
            ValueEnum.HitPointMaxValue => typeof(SlotValueComponent<T, HitPointMaxValueComponent>),
            ValueEnum.HitPointPercentValue => typeof(SlotValueComponent<T, HitPointPercentValueComponent>),
            ValueEnum.HitPointValue => typeof(SlotValueComponent<T, HitPointValueComponent>),
            ValueEnum.LivesValue => typeof(SlotValueComponent<T, LivesValueComponent>),
            ValueEnum.ManaPointMaxValue => typeof(SlotValueComponent<T, ManaPointMaxValueComponent>),
            ValueEnum.ManaPointValue => typeof(SlotValueComponent<T, ManaPointValueComponent>),
            ValueEnum.MoveSpeedValue => typeof(SlotValueComponent<T, MoveSpeedValueComponent>),
            ValueEnum.RecoverySpeedHitPointValue => typeof(SlotValueComponent<T, RecoverySpeedValueComponent<HitPointValueComponent>>),
            ValueEnum.RecoverySpeedManaPointValue => typeof(SlotValueComponent<T, RecoverySpeedValueComponent<ManaPointValueComponent>>),
            ValueEnum.ScoreValue => typeof(SlotValueComponent<T, ScoreValueComponent>),
            ValueEnum.ShockWaveValue => typeof(SlotValueComponent<T, ShockWaveValueComponent>),
            _ => throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null)
        };

        public static Type SlotTagType(SlotTagEnum @enum) => @enum switch
        {
            SlotTagEnum.DamageOrbSlot => typeof(DamageOrbSlotTag),
            SlotTagEnum.DoubleJumpSlot => typeof(DoubleJumpSlotTag),
            SlotTagEnum.ShockWaveSlot => typeof(ShockWaveSlotTag),
            SlotTagEnum.ThroughProjectileSlot => typeof(ThroughProjectileSlotTag),
            SlotTagEnum.TripleJumpSlot => typeof(TripleJumpSlotTag),
            _ => throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null)
        };
        // @formatter:on
    }
}
