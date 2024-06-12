//Файл генерируется в GenValuePoolsUtility
using System;
using Core.Generated;
using Core.Systems;
using Leopotam.EcsLite;
using Lib;

namespace Core.Components
{    
    // @formatter:off
    public static class ValuePoolsUtility
    {
        public static void Sum(ComponentPools pools, int entity, ValueEnum @enum, float byValue)
        {
            switch (@enum)
            {
                case ValueEnum.AddScoreOnDeathValue: pools.AddScoreOnDeathValue.Get(entity).value += byValue; pools.EventUpdatedAddScoreOnDeathValue.AddIfNotExist(entity); break;
                case ValueEnum.AngularSpeedValue: pools.AngularSpeedValue.Get(entity).value += byValue; pools.EventUpdatedAngularSpeedValue.AddIfNotExist(entity); break;
                case ValueEnum.AttackSpeedValue: pools.AttackSpeedValue.Get(entity).value += byValue; pools.EventUpdatedAttackSpeedValue.AddIfNotExist(entity); break;
                case ValueEnum.CriticalChanceValue: pools.CriticalChanceValue.Get(entity).value += byValue; pools.EventUpdatedCriticalChanceValue.AddIfNotExist(entity); break;
                case ValueEnum.CriticalDamageValue: pools.CriticalDamageValue.Get(entity).value += byValue; pools.EventUpdatedCriticalDamageValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageOrbValue: pools.DamageOrbValue.Get(entity).value += byValue; pools.EventUpdatedDamageOrbValue.AddIfNotExist(entity); break;
                case ValueEnum.DamagePercentValue: pools.DamagePercentValue.Get(entity).value += byValue; pools.EventUpdatedDamagePercentValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageValue: pools.DamageValue.Get(entity).value += byValue; pools.EventUpdatedDamageValue.AddIfNotExist(entity); break;
                case ValueEnum.DefenceValue: pools.DefenceValue.Get(entity).value += byValue; pools.EventUpdatedDefenceValue.AddIfNotExist(entity); break;
                case ValueEnum.EnergyMaxValue: pools.EnergyMaxValue.Get(entity).value += byValue; pools.EventUpdatedEnergyMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.EnergyValue: pools.EnergyValue.Get(entity).value += byValue; pools.EventUpdatedEnergyValue.AddIfNotExist(entity); break;
                case ValueEnum.ExperienceValue: pools.ExperienceValue.Get(entity).value += byValue; pools.EventUpdatedExperienceValue.AddIfNotExist(entity); break;
                case ValueEnum.HealingPotionMaxValue: pools.HealingPotionMaxValue.Get(entity).value += byValue; pools.EventUpdatedHealingPotionMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.HealingPotionValue: pools.HealingPotionValue.Get(entity).value += byValue; pools.EventUpdatedHealingPotionValue.AddIfNotExist(entity); break;
                case ValueEnum.HitPointMaxValue: pools.HitPointMaxValue.Get(entity).value += byValue; pools.EventUpdatedHitPointMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.HitPointPercentValue: pools.HitPointPercentValue.Get(entity).value += byValue; pools.EventUpdatedHitPointPercentValue.AddIfNotExist(entity); break;
                case ValueEnum.HitPointValue: pools.HitPointValue.Get(entity).value += byValue; pools.EventUpdatedHitPointValue.AddIfNotExist(entity); break;
                case ValueEnum.LivesValue: pools.LivesValue.Get(entity).value += byValue; pools.EventUpdatedLivesValue.AddIfNotExist(entity); break;
                case ValueEnum.ManaPointMaxValue: pools.ManaPointMaxValue.Get(entity).value += byValue; pools.EventUpdatedManaPointMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.ManaPointValue: pools.ManaPointValue.Get(entity).value += byValue; pools.EventUpdatedManaPointValue.AddIfNotExist(entity); break;
                case ValueEnum.MoveSpeedValue: pools.MoveSpeedValue.Get(entity).value += byValue; pools.EventUpdatedMoveSpeedValue.AddIfNotExist(entity); break;
                case ValueEnum.ScoreValue: pools.ScoreValue.Get(entity).value += byValue; pools.EventUpdatedScoreValue.AddIfNotExist(entity); break;
                case ValueEnum.ShockWaveValue: pools.ShockWaveValue.Get(entity).value += byValue; pools.EventUpdatedShockWaveValue.AddIfNotExist(entity); break;
                case ValueEnum.RecoverySpeedHitPointValue: pools.RecoverySpeedHitPointValue.Get(entity).value += byValue; pools.EventUpdatedRecoverySpeedHitPointValue.AddIfNotExist(entity); break;
                case ValueEnum.RecoverySpeedManaPointValue: pools.RecoverySpeedManaPointValue.Get(entity).value += byValue; pools.EventUpdatedRecoverySpeedManaPointValue.AddIfNotExist(entity); break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null);
            }
        }
        
        public static void Sum(ComponentPools pools, int entity, ValueEnum @enum, ValueEnum byValue)
        {
            switch (@enum)
            {
                case ValueEnum.AddScoreOnDeathValue: pools.AddScoreOnDeathValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedAddScoreOnDeathValue.AddIfNotExist(entity); break;
                case ValueEnum.AngularSpeedValue: pools.AngularSpeedValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedAngularSpeedValue.AddIfNotExist(entity); break;
                case ValueEnum.AttackSpeedValue: pools.AttackSpeedValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedAttackSpeedValue.AddIfNotExist(entity); break;
                case ValueEnum.CriticalChanceValue: pools.CriticalChanceValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedCriticalChanceValue.AddIfNotExist(entity); break;
                case ValueEnum.CriticalDamageValue: pools.CriticalDamageValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedCriticalDamageValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageOrbValue: pools.DamageOrbValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedDamageOrbValue.AddIfNotExist(entity); break;
                case ValueEnum.DamagePercentValue: pools.DamagePercentValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedDamagePercentValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageValue: pools.DamageValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedDamageValue.AddIfNotExist(entity); break;
                case ValueEnum.DefenceValue: pools.DefenceValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedDefenceValue.AddIfNotExist(entity); break;
                case ValueEnum.EnergyMaxValue: pools.EnergyMaxValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedEnergyMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.EnergyValue: pools.EnergyValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedEnergyValue.AddIfNotExist(entity); break;
                case ValueEnum.ExperienceValue: pools.ExperienceValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedExperienceValue.AddIfNotExist(entity); break;
                case ValueEnum.HealingPotionMaxValue: pools.HealingPotionMaxValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedHealingPotionMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.HealingPotionValue: pools.HealingPotionValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedHealingPotionValue.AddIfNotExist(entity); break;
                case ValueEnum.HitPointMaxValue: pools.HitPointMaxValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedHitPointMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.HitPointPercentValue: pools.HitPointPercentValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedHitPointPercentValue.AddIfNotExist(entity); break;
                case ValueEnum.HitPointValue: pools.HitPointValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedHitPointValue.AddIfNotExist(entity); break;
                case ValueEnum.LivesValue: pools.LivesValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedLivesValue.AddIfNotExist(entity); break;
                case ValueEnum.ManaPointMaxValue: pools.ManaPointMaxValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedManaPointMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.ManaPointValue: pools.ManaPointValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedManaPointValue.AddIfNotExist(entity); break;
                case ValueEnum.MoveSpeedValue: pools.MoveSpeedValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedMoveSpeedValue.AddIfNotExist(entity); break;
                case ValueEnum.ScoreValue: pools.ScoreValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedScoreValue.AddIfNotExist(entity); break;
                case ValueEnum.ShockWaveValue: pools.ShockWaveValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedShockWaveValue.AddIfNotExist(entity); break;
                case ValueEnum.RecoverySpeedHitPointValue: pools.RecoverySpeedHitPointValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedRecoverySpeedHitPointValue.AddIfNotExist(entity); break;
                case ValueEnum.RecoverySpeedManaPointValue: pools.RecoverySpeedManaPointValue.Get(entity).value += GetValue(pools, entity, byValue); pools.EventUpdatedRecoverySpeedManaPointValue.AddIfNotExist(entity); break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null);
            }
        }
        
        public static float GetValue(ComponentPools pools, int entity, ValueEnum @enum) => @enum switch
        {
            ValueEnum.AddScoreOnDeathValue => pools.AddScoreOnDeathValue.Get(entity).value,
            ValueEnum.AngularSpeedValue => pools.AngularSpeedValue.Get(entity).value,
            ValueEnum.AttackSpeedValue => pools.AttackSpeedValue.Get(entity).value,
            ValueEnum.CriticalChanceValue => pools.CriticalChanceValue.Get(entity).value,
            ValueEnum.CriticalDamageValue => pools.CriticalDamageValue.Get(entity).value,
            ValueEnum.DamageOrbValue => pools.DamageOrbValue.Get(entity).value,
            ValueEnum.DamagePercentValue => pools.DamagePercentValue.Get(entity).value,
            ValueEnum.DamageValue => pools.DamageValue.Get(entity).value,
            ValueEnum.DefenceValue => pools.DefenceValue.Get(entity).value,
            ValueEnum.EnergyMaxValue => pools.EnergyMaxValue.Get(entity).value,
            ValueEnum.EnergyValue => pools.EnergyValue.Get(entity).value,
            ValueEnum.ExperienceValue => pools.ExperienceValue.Get(entity).value,
            ValueEnum.HealingPotionMaxValue => pools.HealingPotionMaxValue.Get(entity).value,
            ValueEnum.HealingPotionValue => pools.HealingPotionValue.Get(entity).value,
            ValueEnum.HitPointMaxValue => pools.HitPointMaxValue.Get(entity).value,
            ValueEnum.HitPointPercentValue => pools.HitPointPercentValue.Get(entity).value,
            ValueEnum.HitPointValue => pools.HitPointValue.Get(entity).value,
            ValueEnum.LivesValue => pools.LivesValue.Get(entity).value,
            ValueEnum.ManaPointMaxValue => pools.ManaPointMaxValue.Get(entity).value,
            ValueEnum.ManaPointValue => pools.ManaPointValue.Get(entity).value,
            ValueEnum.MoveSpeedValue => pools.MoveSpeedValue.Get(entity).value,
            ValueEnum.ScoreValue => pools.ScoreValue.Get(entity).value,
            ValueEnum.ShockWaveValue => pools.ShockWaveValue.Get(entity).value,
            ValueEnum.RecoverySpeedHitPointValue => pools.RecoverySpeedHitPointValue.Get(entity).value,
            ValueEnum.RecoverySpeedManaPointValue => pools.RecoverySpeedManaPointValue.Get(entity).value,
            _ => throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null)
        };

        public static IEcsPool GetStartRecalculatePool(ComponentPools pools, ValueEnum @enum) => @enum switch
        {
            ValueEnum.AddScoreOnDeathValue => pools.EventStartRecalculateAddScoreOnDeathValue,
            ValueEnum.AngularSpeedValue => pools.EventStartRecalculateAngularSpeedValue,
            ValueEnum.AttackSpeedValue => pools.EventStartRecalculateAttackSpeedValue,
            ValueEnum.CriticalChanceValue => pools.EventStartRecalculateCriticalChanceValue,
            ValueEnum.CriticalDamageValue => pools.EventStartRecalculateCriticalDamageValue,
            ValueEnum.DamageOrbValue => pools.EventStartRecalculateDamageOrbValue,
            ValueEnum.DamagePercentValue => pools.EventStartRecalculateDamagePercentValue,
            ValueEnum.DamageValue => pools.EventStartRecalculateDamageValue,
            ValueEnum.DefenceValue => pools.EventStartRecalculateDefenceValue,
            ValueEnum.EnergyMaxValue => pools.EventStartRecalculateEnergyMaxValue,
            ValueEnum.EnergyValue => pools.EventStartRecalculateEnergyValue,
            ValueEnum.ExperienceValue => pools.EventStartRecalculateExperienceValue,
            ValueEnum.HealingPotionMaxValue => pools.EventStartRecalculateHealingPotionMaxValue,
            ValueEnum.HealingPotionValue => pools.EventStartRecalculateHealingPotionValue,
            ValueEnum.HitPointMaxValue => pools.EventStartRecalculateHitPointMaxValue,
            ValueEnum.HitPointPercentValue => pools.EventStartRecalculateHitPointPercentValue,
            ValueEnum.HitPointValue => pools.EventStartRecalculateHitPointValue,
            ValueEnum.LivesValue => pools.EventStartRecalculateLivesValue,
            ValueEnum.ManaPointMaxValue => pools.EventStartRecalculateManaPointMaxValue,
            ValueEnum.ManaPointValue => pools.EventStartRecalculateManaPointValue,
            ValueEnum.MoveSpeedValue => pools.EventStartRecalculateMoveSpeedValue,
            ValueEnum.ScoreValue => pools.EventStartRecalculateScoreValue,
            ValueEnum.ShockWaveValue => pools.EventStartRecalculateShockWaveValue,
            ValueEnum.RecoverySpeedHitPointValue => pools.EventStartRecalculateRecoverySpeedHitPointValue,
            ValueEnum.RecoverySpeedManaPointValue => pools.EventStartRecalculateRecoverySpeedManaPointValue,
            _ => throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null)
        };
        
        public static void SetValue(ComponentPools pools, int entity, ValueEnum @enum, float value)
        {
            switch (@enum)
            {
                case ValueEnum.AddScoreOnDeathValue: pools.AddScoreOnDeathValue.Get(entity).value = value; pools.EventUpdatedAddScoreOnDeathValue.AddIfNotExist(entity); break;
                case ValueEnum.AngularSpeedValue: pools.AngularSpeedValue.Get(entity).value = value; pools.EventUpdatedAngularSpeedValue.AddIfNotExist(entity); break;
                case ValueEnum.AttackSpeedValue: pools.AttackSpeedValue.Get(entity).value = value; pools.EventUpdatedAttackSpeedValue.AddIfNotExist(entity); break;
                case ValueEnum.CriticalChanceValue: pools.CriticalChanceValue.Get(entity).value = value; pools.EventUpdatedCriticalChanceValue.AddIfNotExist(entity); break;
                case ValueEnum.CriticalDamageValue: pools.CriticalDamageValue.Get(entity).value = value; pools.EventUpdatedCriticalDamageValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageOrbValue: pools.DamageOrbValue.Get(entity).value = value; pools.EventUpdatedDamageOrbValue.AddIfNotExist(entity); break;
                case ValueEnum.DamagePercentValue: pools.DamagePercentValue.Get(entity).value = value; pools.EventUpdatedDamagePercentValue.AddIfNotExist(entity); break;
                case ValueEnum.DamageValue: pools.DamageValue.Get(entity).value = value; pools.EventUpdatedDamageValue.AddIfNotExist(entity); break;
                case ValueEnum.DefenceValue: pools.DefenceValue.Get(entity).value = value; pools.EventUpdatedDefenceValue.AddIfNotExist(entity); break;
                case ValueEnum.EnergyMaxValue: pools.EnergyMaxValue.Get(entity).value = value; pools.EventUpdatedEnergyMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.EnergyValue: pools.EnergyValue.Get(entity).value = value; pools.EventUpdatedEnergyValue.AddIfNotExist(entity); break;
                case ValueEnum.ExperienceValue: pools.ExperienceValue.Get(entity).value = value; pools.EventUpdatedExperienceValue.AddIfNotExist(entity); break;
                case ValueEnum.HealingPotionMaxValue: pools.HealingPotionMaxValue.Get(entity).value = value; pools.EventUpdatedHealingPotionMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.HealingPotionValue: pools.HealingPotionValue.Get(entity).value = value; pools.EventUpdatedHealingPotionValue.AddIfNotExist(entity); break;
                case ValueEnum.HitPointMaxValue: pools.HitPointMaxValue.Get(entity).value = value; pools.EventUpdatedHitPointMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.HitPointPercentValue: pools.HitPointPercentValue.Get(entity).value = value; pools.EventUpdatedHitPointPercentValue.AddIfNotExist(entity); break;
                case ValueEnum.HitPointValue: pools.HitPointValue.Get(entity).value = value; pools.EventUpdatedHitPointValue.AddIfNotExist(entity); break;
                case ValueEnum.LivesValue: pools.LivesValue.Get(entity).value = value; pools.EventUpdatedLivesValue.AddIfNotExist(entity); break;
                case ValueEnum.ManaPointMaxValue: pools.ManaPointMaxValue.Get(entity).value = value; pools.EventUpdatedManaPointMaxValue.AddIfNotExist(entity); break;
                case ValueEnum.ManaPointValue: pools.ManaPointValue.Get(entity).value = value; pools.EventUpdatedManaPointValue.AddIfNotExist(entity); break;
                case ValueEnum.MoveSpeedValue: pools.MoveSpeedValue.Get(entity).value = value; pools.EventUpdatedMoveSpeedValue.AddIfNotExist(entity); break;
                case ValueEnum.ScoreValue: pools.ScoreValue.Get(entity).value = value; pools.EventUpdatedScoreValue.AddIfNotExist(entity); break;
                case ValueEnum.ShockWaveValue: pools.ShockWaveValue.Get(entity).value = value; pools.EventUpdatedShockWaveValue.AddIfNotExist(entity); break;
                case ValueEnum.RecoverySpeedHitPointValue: pools.RecoverySpeedHitPointValue.Get(entity).value = value; pools.EventUpdatedRecoverySpeedHitPointValue.AddIfNotExist(entity); break;
                case ValueEnum.RecoverySpeedManaPointValue: pools.RecoverySpeedManaPointValue.Get(entity).value = value; pools.EventUpdatedRecoverySpeedManaPointValue.AddIfNotExist(entity); break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(@enum), @enum, null);
            }
        }
    }
}