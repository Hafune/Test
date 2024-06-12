using Core.Systems;

namespace Core.Components
{
    public static class ValueUtility
    {
        public static string Format(ValueEnum valueEnum, float value) => valueEnum switch
        {
            ValueEnum.AddScoreOnDeathValue => FormatUiValuesUtility.ToIntString(value),
            ValueEnum.AttackSpeedValue => FormatUiValuesUtility.ToPercentInt(value) + "%",
            ValueEnum.CriticalChanceValue => FormatUiValuesUtility.ToPercentInt(value) + "%",
            ValueEnum.CriticalDamageValue => FormatUiValuesUtility.ToPercentInt(value) + "%",
            ValueEnum.DamagePercentValue => FormatUiValuesUtility.ToPercentInt(value) + "%",
            ValueEnum.DamageValue => FormatUiValuesUtility.ToIntString(value),
            ValueEnum.DefenceValue => FormatUiValuesUtility.ToIntString(value),
            ValueEnum.EnergyMaxValue => FormatUiValuesUtility.ToIntString(value),
            ValueEnum.EnergyValue => FormatUiValuesUtility.ToIntString(value),
            ValueEnum.ExperienceValue => FormatUiValuesUtility.ToIntString(value),
            ValueEnum.HealingPotionMaxValue => FormatUiValuesUtility.ToIntString(value),
            ValueEnum.HealingPotionValue => FormatUiValuesUtility.ToIntString(value),
            ValueEnum.HitPointMaxValue => FormatUiValuesUtility.ToIntString(value),
            ValueEnum.HitPointValue => FormatUiValuesUtility.ToIntString(value),
            ValueEnum.HitPointPercentValue => FormatUiValuesUtility.ToPercentInt(value)+ "%",
            ValueEnum.LivesValue => FormatUiValuesUtility.ToIntString(value),
            ValueEnum.ManaPointMaxValue => FormatUiValuesUtility.ToIntString(value),
            ValueEnum.ManaPointValue => FormatUiValuesUtility.ToIntString(value),
            ValueEnum.MoveSpeedValue => FormatUiValuesUtility.ToPercentInt(value) + "%",
            ValueEnum.RecoverySpeedHitPointValue => FormatUiValuesUtility.ToIntString(value),
            ValueEnum.RecoverySpeedManaPointValue => FormatUiValuesUtility.ToIntString(value),
            ValueEnum.ScoreValue => FormatUiValuesUtility.ToIntString(value),
            ValueEnum.ShockWaveValue => FormatUiValuesUtility.ToPercentInt(value) + "%",
            _ => FormatUiValuesUtility.ToIntString(value)
        };
    }
}