//Файл генерируется в GenValueScaleSystemsUtility
using System;
using Core.Components;
using Core.Systems;
using Leopotam.EcsLite;

namespace Core.Services
{
    // @formatter:off
    public static class ValueScaleSystemsUtility
    {
        public static IEcsSystem BuildScaleSystem(ValueEnum valueEnum, Func<int, float> getScale) => valueEnum switch
        {
            ValueEnum.AddScoreOnDeathValue => new ScaleValueSystem<AddScoreOnDeathValueComponent>(getScale),
            ValueEnum.AngularSpeedValue => new ScaleValueSystem<AngularSpeedValueComponent>(getScale),
            ValueEnum.AttackSpeedValue => new ScaleValueSystem<AttackSpeedValueComponent>(getScale),
            ValueEnum.CriticalChanceValue => new ScaleValueSystem<CriticalChanceValueComponent>(getScale),
            ValueEnum.CriticalDamageValue => new ScaleValueSystem<CriticalDamageValueComponent>(getScale),
            ValueEnum.DamageOrbValue => new ScaleValueSystem<DamageOrbValueComponent>(getScale),
            ValueEnum.DamagePercentValue => new ScaleValueSystem<DamagePercentValueComponent>(getScale),
            ValueEnum.DamageValue => new ScaleValueSystem<DamageValueComponent>(getScale),
            ValueEnum.DefenceValue => new ScaleValueSystem<DefenceValueComponent>(getScale),
            ValueEnum.EnergyMaxValue => new ScaleValueSystem<EnergyMaxValueComponent>(getScale),
            ValueEnum.EnergyValue => new ScaleValueSystem<EnergyValueComponent>(getScale),
            ValueEnum.ExperienceValue => new ScaleValueSystem<ExperienceValueComponent>(getScale),
            ValueEnum.HealingPotionMaxValue => new ScaleValueSystem<HealingPotionMaxValueComponent>(getScale),
            ValueEnum.HealingPotionValue => new ScaleValueSystem<HealingPotionValueComponent>(getScale),
            ValueEnum.HitPointMaxValue => new ScaleValueSystem<HitPointMaxValueComponent>(getScale),
            ValueEnum.HitPointPercentValue => new ScaleValueSystem<HitPointPercentValueComponent>(getScale),
            ValueEnum.HitPointValue => new ScaleValueSystem<HitPointValueComponent>(getScale),
            ValueEnum.LivesValue => new ScaleValueSystem<LivesValueComponent>(getScale),
            ValueEnum.ManaPointMaxValue => new ScaleValueSystem<ManaPointMaxValueComponent>(getScale),
            ValueEnum.ManaPointValue => new ScaleValueSystem<ManaPointValueComponent>(getScale),
            ValueEnum.MoveSpeedValue => new ScaleValueSystem<MoveSpeedValueComponent>(getScale),
            ValueEnum.RecoverySpeedHitPointValue => new ScaleValueSystem<RecoverySpeedValueComponent<HitPointValueComponent>>(getScale),
            ValueEnum.RecoverySpeedManaPointValue => new ScaleValueSystem<RecoverySpeedValueComponent<ManaPointValueComponent>>(getScale),
            ValueEnum.ScoreValue => new ScaleValueSystem<ScoreValueComponent>(getScale),
            ValueEnum.ShockWaveValue => new ScaleValueSystem<ShockWaveValueComponent>(getScale),
            _ => throw new ArgumentOutOfRangeException(nameof(valueEnum), valueEnum, null)
        };
    }
}