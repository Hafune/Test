//Файл генерируется в GenEventStartRecalculateValueSystem
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Generated
{
    // @formatter:off
    public class EventStartRecalculateValueSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<EventValueUpdated<AddScoreOnDeathValueComponent>>> _eventUpdatedAddScoreOnDeathValueFilter;
        private readonly EcsFilterInject<Inc<AddScoreOnDeathValueComponent, EventStartRecalculateValue<AddScoreOnDeathValueComponent>, BaseValueComponent<AddScoreOnDeathValueComponent>>> _baseAddScoreOnDeathValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<AngularSpeedValueComponent>>> _eventUpdatedAngularSpeedValueFilter;
        private readonly EcsFilterInject<Inc<AngularSpeedValueComponent, EventStartRecalculateValue<AngularSpeedValueComponent>, BaseValueComponent<AngularSpeedValueComponent>>> _baseAngularSpeedValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<AttackSpeedValueComponent>>> _eventUpdatedAttackSpeedValueFilter;
        private readonly EcsFilterInject<Inc<AttackSpeedValueComponent, EventStartRecalculateValue<AttackSpeedValueComponent>, BaseValueComponent<AttackSpeedValueComponent>>> _baseAttackSpeedValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CriticalChanceValueComponent>>> _eventUpdatedCriticalChanceValueFilter;
        private readonly EcsFilterInject<Inc<CriticalChanceValueComponent, EventStartRecalculateValue<CriticalChanceValueComponent>, BaseValueComponent<CriticalChanceValueComponent>>> _baseCriticalChanceValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CriticalDamageValueComponent>>> _eventUpdatedCriticalDamageValueFilter;
        private readonly EcsFilterInject<Inc<CriticalDamageValueComponent, EventStartRecalculateValue<CriticalDamageValueComponent>, BaseValueComponent<CriticalDamageValueComponent>>> _baseCriticalDamageValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<DamageOrbValueComponent>>> _eventUpdatedDamageOrbValueFilter;
        private readonly EcsFilterInject<Inc<DamageOrbValueComponent, EventStartRecalculateValue<DamageOrbValueComponent>, BaseValueComponent<DamageOrbValueComponent>>> _baseDamageOrbValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<DamagePercentValueComponent>>> _eventUpdatedDamagePercentValueFilter;
        private readonly EcsFilterInject<Inc<DamagePercentValueComponent, EventStartRecalculateValue<DamagePercentValueComponent>, BaseValueComponent<DamagePercentValueComponent>>> _baseDamagePercentValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<DamageValueComponent>>> _eventUpdatedDamageValueFilter;
        private readonly EcsFilterInject<Inc<DamageValueComponent, EventStartRecalculateValue<DamageValueComponent>, BaseValueComponent<DamageValueComponent>>> _baseDamageValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<DefenceValueComponent>>> _eventUpdatedDefenceValueFilter;
        private readonly EcsFilterInject<Inc<DefenceValueComponent, EventStartRecalculateValue<DefenceValueComponent>, BaseValueComponent<DefenceValueComponent>>> _baseDefenceValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<EnergyMaxValueComponent>>> _eventUpdatedEnergyMaxValueFilter;
        private readonly EcsFilterInject<Inc<EnergyMaxValueComponent, EventStartRecalculateValue<EnergyMaxValueComponent>, BaseValueComponent<EnergyMaxValueComponent>>> _baseEnergyMaxValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<EnergyValueComponent>>> _eventUpdatedEnergyValueFilter;
        private readonly EcsFilterInject<Inc<EnergyValueComponent, EventStartRecalculateValue<EnergyValueComponent>, BaseValueComponent<EnergyValueComponent>>> _baseEnergyValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ExperienceValueComponent>>> _eventUpdatedExperienceValueFilter;
        private readonly EcsFilterInject<Inc<ExperienceValueComponent, EventStartRecalculateValue<ExperienceValueComponent>, BaseValueComponent<ExperienceValueComponent>>> _baseExperienceValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<HealingPotionMaxValueComponent>>> _eventUpdatedHealingPotionMaxValueFilter;
        private readonly EcsFilterInject<Inc<HealingPotionMaxValueComponent, EventStartRecalculateValue<HealingPotionMaxValueComponent>, BaseValueComponent<HealingPotionMaxValueComponent>>> _baseHealingPotionMaxValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<HealingPotionValueComponent>>> _eventUpdatedHealingPotionValueFilter;
        private readonly EcsFilterInject<Inc<HealingPotionValueComponent, EventStartRecalculateValue<HealingPotionValueComponent>, BaseValueComponent<HealingPotionValueComponent>>> _baseHealingPotionValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<HitPointMaxValueComponent>>> _eventUpdatedHitPointMaxValueFilter;
        private readonly EcsFilterInject<Inc<HitPointMaxValueComponent, EventStartRecalculateValue<HitPointMaxValueComponent>, BaseValueComponent<HitPointMaxValueComponent>>> _baseHitPointMaxValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<HitPointPercentValueComponent>>> _eventUpdatedHitPointPercentValueFilter;
        private readonly EcsFilterInject<Inc<HitPointPercentValueComponent, EventStartRecalculateValue<HitPointPercentValueComponent>, BaseValueComponent<HitPointPercentValueComponent>>> _baseHitPointPercentValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<HitPointValueComponent>>> _eventUpdatedHitPointValueFilter;
        private readonly EcsFilterInject<Inc<HitPointValueComponent, EventStartRecalculateValue<HitPointValueComponent>, BaseValueComponent<HitPointValueComponent>>> _baseHitPointValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<LivesValueComponent>>> _eventUpdatedLivesValueFilter;
        private readonly EcsFilterInject<Inc<LivesValueComponent, EventStartRecalculateValue<LivesValueComponent>, BaseValueComponent<LivesValueComponent>>> _baseLivesValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ManaPointMaxValueComponent>>> _eventUpdatedManaPointMaxValueFilter;
        private readonly EcsFilterInject<Inc<ManaPointMaxValueComponent, EventStartRecalculateValue<ManaPointMaxValueComponent>, BaseValueComponent<ManaPointMaxValueComponent>>> _baseManaPointMaxValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ManaPointValueComponent>>> _eventUpdatedManaPointValueFilter;
        private readonly EcsFilterInject<Inc<ManaPointValueComponent, EventStartRecalculateValue<ManaPointValueComponent>, BaseValueComponent<ManaPointValueComponent>>> _baseManaPointValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<MoveSpeedValueComponent>>> _eventUpdatedMoveSpeedValueFilter;
        private readonly EcsFilterInject<Inc<MoveSpeedValueComponent, EventStartRecalculateValue<MoveSpeedValueComponent>, BaseValueComponent<MoveSpeedValueComponent>>> _baseMoveSpeedValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ScoreValueComponent>>> _eventUpdatedScoreValueFilter;
        private readonly EcsFilterInject<Inc<ScoreValueComponent, EventStartRecalculateValue<ScoreValueComponent>, BaseValueComponent<ScoreValueComponent>>> _baseScoreValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ShockWaveValueComponent>>> _eventUpdatedShockWaveValueFilter;
        private readonly EcsFilterInject<Inc<ShockWaveValueComponent, EventStartRecalculateValue<ShockWaveValueComponent>, BaseValueComponent<ShockWaveValueComponent>>> _baseShockWaveValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<RecoverySpeedValueComponent<HitPointValueComponent>>>> _eventUpdatedRecoverySpeedHitPointValueFilter;
        private readonly EcsFilterInject<Inc<RecoverySpeedValueComponent<HitPointValueComponent>, EventStartRecalculateValue<RecoverySpeedValueComponent<HitPointValueComponent>>, BaseValueComponent<RecoverySpeedValueComponent<HitPointValueComponent>>>> _baseRecoverySpeedHitPointValueFilter;
        private readonly EcsFilterInject<Inc<EventValueUpdated<RecoverySpeedValueComponent<ManaPointValueComponent>>>> _eventUpdatedRecoverySpeedManaPointValueFilter;
        private readonly EcsFilterInject<Inc<RecoverySpeedValueComponent<ManaPointValueComponent>, EventStartRecalculateValue<RecoverySpeedValueComponent<ManaPointValueComponent>>, BaseValueComponent<RecoverySpeedValueComponent<ManaPointValueComponent>>>> _baseRecoverySpeedManaPointValueFilter;
                
        private readonly EcsPoolInject<AddScoreOnDeathValueComponent> _AddScoreOnDeathValuePool;
        private readonly EcsPoolInject<BaseValueComponent<AddScoreOnDeathValueComponent>> _baseAddScoreOnDeathValuePool;
        private readonly EcsPoolInject<EventValueUpdated<AddScoreOnDeathValueComponent>> _eventUpdatedAddScoreOnDeathValuePool;
        private readonly EcsPoolInject<AngularSpeedValueComponent> _AngularSpeedValuePool;
        private readonly EcsPoolInject<BaseValueComponent<AngularSpeedValueComponent>> _baseAngularSpeedValuePool;
        private readonly EcsPoolInject<EventValueUpdated<AngularSpeedValueComponent>> _eventUpdatedAngularSpeedValuePool;
        private readonly EcsPoolInject<AttackSpeedValueComponent> _AttackSpeedValuePool;
        private readonly EcsPoolInject<BaseValueComponent<AttackSpeedValueComponent>> _baseAttackSpeedValuePool;
        private readonly EcsPoolInject<EventValueUpdated<AttackSpeedValueComponent>> _eventUpdatedAttackSpeedValuePool;
        private readonly EcsPoolInject<CriticalChanceValueComponent> _CriticalChanceValuePool;
        private readonly EcsPoolInject<BaseValueComponent<CriticalChanceValueComponent>> _baseCriticalChanceValuePool;
        private readonly EcsPoolInject<EventValueUpdated<CriticalChanceValueComponent>> _eventUpdatedCriticalChanceValuePool;
        private readonly EcsPoolInject<CriticalDamageValueComponent> _CriticalDamageValuePool;
        private readonly EcsPoolInject<BaseValueComponent<CriticalDamageValueComponent>> _baseCriticalDamageValuePool;
        private readonly EcsPoolInject<EventValueUpdated<CriticalDamageValueComponent>> _eventUpdatedCriticalDamageValuePool;
        private readonly EcsPoolInject<DamageOrbValueComponent> _DamageOrbValuePool;
        private readonly EcsPoolInject<BaseValueComponent<DamageOrbValueComponent>> _baseDamageOrbValuePool;
        private readonly EcsPoolInject<EventValueUpdated<DamageOrbValueComponent>> _eventUpdatedDamageOrbValuePool;
        private readonly EcsPoolInject<DamagePercentValueComponent> _DamagePercentValuePool;
        private readonly EcsPoolInject<BaseValueComponent<DamagePercentValueComponent>> _baseDamagePercentValuePool;
        private readonly EcsPoolInject<EventValueUpdated<DamagePercentValueComponent>> _eventUpdatedDamagePercentValuePool;
        private readonly EcsPoolInject<DamageValueComponent> _DamageValuePool;
        private readonly EcsPoolInject<BaseValueComponent<DamageValueComponent>> _baseDamageValuePool;
        private readonly EcsPoolInject<EventValueUpdated<DamageValueComponent>> _eventUpdatedDamageValuePool;
        private readonly EcsPoolInject<DefenceValueComponent> _DefenceValuePool;
        private readonly EcsPoolInject<BaseValueComponent<DefenceValueComponent>> _baseDefenceValuePool;
        private readonly EcsPoolInject<EventValueUpdated<DefenceValueComponent>> _eventUpdatedDefenceValuePool;
        private readonly EcsPoolInject<EnergyMaxValueComponent> _EnergyMaxValuePool;
        private readonly EcsPoolInject<BaseValueComponent<EnergyMaxValueComponent>> _baseEnergyMaxValuePool;
        private readonly EcsPoolInject<EventValueUpdated<EnergyMaxValueComponent>> _eventUpdatedEnergyMaxValuePool;
        private readonly EcsPoolInject<EnergyValueComponent> _EnergyValuePool;
        private readonly EcsPoolInject<BaseValueComponent<EnergyValueComponent>> _baseEnergyValuePool;
        private readonly EcsPoolInject<EventValueUpdated<EnergyValueComponent>> _eventUpdatedEnergyValuePool;
        private readonly EcsPoolInject<ExperienceValueComponent> _ExperienceValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ExperienceValueComponent>> _baseExperienceValuePool;
        private readonly EcsPoolInject<EventValueUpdated<ExperienceValueComponent>> _eventUpdatedExperienceValuePool;
        private readonly EcsPoolInject<HealingPotionMaxValueComponent> _HealingPotionMaxValuePool;
        private readonly EcsPoolInject<BaseValueComponent<HealingPotionMaxValueComponent>> _baseHealingPotionMaxValuePool;
        private readonly EcsPoolInject<EventValueUpdated<HealingPotionMaxValueComponent>> _eventUpdatedHealingPotionMaxValuePool;
        private readonly EcsPoolInject<HealingPotionValueComponent> _HealingPotionValuePool;
        private readonly EcsPoolInject<BaseValueComponent<HealingPotionValueComponent>> _baseHealingPotionValuePool;
        private readonly EcsPoolInject<EventValueUpdated<HealingPotionValueComponent>> _eventUpdatedHealingPotionValuePool;
        private readonly EcsPoolInject<HitPointMaxValueComponent> _HitPointMaxValuePool;
        private readonly EcsPoolInject<BaseValueComponent<HitPointMaxValueComponent>> _baseHitPointMaxValuePool;
        private readonly EcsPoolInject<EventValueUpdated<HitPointMaxValueComponent>> _eventUpdatedHitPointMaxValuePool;
        private readonly EcsPoolInject<HitPointPercentValueComponent> _HitPointPercentValuePool;
        private readonly EcsPoolInject<BaseValueComponent<HitPointPercentValueComponent>> _baseHitPointPercentValuePool;
        private readonly EcsPoolInject<EventValueUpdated<HitPointPercentValueComponent>> _eventUpdatedHitPointPercentValuePool;
        private readonly EcsPoolInject<HitPointValueComponent> _HitPointValuePool;
        private readonly EcsPoolInject<BaseValueComponent<HitPointValueComponent>> _baseHitPointValuePool;
        private readonly EcsPoolInject<EventValueUpdated<HitPointValueComponent>> _eventUpdatedHitPointValuePool;
        private readonly EcsPoolInject<LivesValueComponent> _LivesValuePool;
        private readonly EcsPoolInject<BaseValueComponent<LivesValueComponent>> _baseLivesValuePool;
        private readonly EcsPoolInject<EventValueUpdated<LivesValueComponent>> _eventUpdatedLivesValuePool;
        private readonly EcsPoolInject<ManaPointMaxValueComponent> _ManaPointMaxValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ManaPointMaxValueComponent>> _baseManaPointMaxValuePool;
        private readonly EcsPoolInject<EventValueUpdated<ManaPointMaxValueComponent>> _eventUpdatedManaPointMaxValuePool;
        private readonly EcsPoolInject<ManaPointValueComponent> _ManaPointValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ManaPointValueComponent>> _baseManaPointValuePool;
        private readonly EcsPoolInject<EventValueUpdated<ManaPointValueComponent>> _eventUpdatedManaPointValuePool;
        private readonly EcsPoolInject<MoveSpeedValueComponent> _MoveSpeedValuePool;
        private readonly EcsPoolInject<BaseValueComponent<MoveSpeedValueComponent>> _baseMoveSpeedValuePool;
        private readonly EcsPoolInject<EventValueUpdated<MoveSpeedValueComponent>> _eventUpdatedMoveSpeedValuePool;
        private readonly EcsPoolInject<ScoreValueComponent> _ScoreValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ScoreValueComponent>> _baseScoreValuePool;
        private readonly EcsPoolInject<EventValueUpdated<ScoreValueComponent>> _eventUpdatedScoreValuePool;
        private readonly EcsPoolInject<ShockWaveValueComponent> _ShockWaveValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ShockWaveValueComponent>> _baseShockWaveValuePool;
        private readonly EcsPoolInject<EventValueUpdated<ShockWaveValueComponent>> _eventUpdatedShockWaveValuePool;
        private readonly EcsPoolInject<RecoverySpeedValueComponent<HitPointValueComponent>> _RecoverySpeedHitPointValuePool;
        private readonly EcsPoolInject<BaseValueComponent<RecoverySpeedValueComponent<HitPointValueComponent>>> _baseRecoverySpeedHitPointValuePool;
        private readonly EcsPoolInject<EventValueUpdated<RecoverySpeedValueComponent<HitPointValueComponent>>> _eventUpdatedRecoverySpeedHitPointValuePool;
        private readonly EcsPoolInject<RecoverySpeedValueComponent<ManaPointValueComponent>> _RecoverySpeedManaPointValuePool;
        private readonly EcsPoolInject<BaseValueComponent<RecoverySpeedValueComponent<ManaPointValueComponent>>> _baseRecoverySpeedManaPointValuePool;
        private readonly EcsPoolInject<EventValueUpdated<RecoverySpeedValueComponent<ManaPointValueComponent>>> _eventUpdatedRecoverySpeedManaPointValuePool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _eventUpdatedAddScoreOnDeathValueFilter.Value) _eventUpdatedAddScoreOnDeathValuePool.Value.Del(i);
            foreach (var i in _baseAddScoreOnDeathValueFilter.Value) { _AddScoreOnDeathValuePool.Value.Get(i).value = _baseAddScoreOnDeathValuePool.Value.Get(i).baseValue; _eventUpdatedAddScoreOnDeathValuePool.Value.Add(i); }
            foreach (var i in _eventUpdatedAngularSpeedValueFilter.Value) _eventUpdatedAngularSpeedValuePool.Value.Del(i);
            foreach (var i in _baseAngularSpeedValueFilter.Value) { _AngularSpeedValuePool.Value.Get(i).value = _baseAngularSpeedValuePool.Value.Get(i).baseValue; _eventUpdatedAngularSpeedValuePool.Value.Add(i); }
            foreach (var i in _eventUpdatedAttackSpeedValueFilter.Value) _eventUpdatedAttackSpeedValuePool.Value.Del(i);
            foreach (var i in _baseAttackSpeedValueFilter.Value) { _AttackSpeedValuePool.Value.Get(i).value = _baseAttackSpeedValuePool.Value.Get(i).baseValue; _eventUpdatedAttackSpeedValuePool.Value.Add(i); }
            foreach (var i in _eventUpdatedCriticalChanceValueFilter.Value) _eventUpdatedCriticalChanceValuePool.Value.Del(i);
            foreach (var i in _baseCriticalChanceValueFilter.Value) { _CriticalChanceValuePool.Value.Get(i).value = _baseCriticalChanceValuePool.Value.Get(i).baseValue; _eventUpdatedCriticalChanceValuePool.Value.Add(i); }
            foreach (var i in _eventUpdatedCriticalDamageValueFilter.Value) _eventUpdatedCriticalDamageValuePool.Value.Del(i);
            foreach (var i in _baseCriticalDamageValueFilter.Value) { _CriticalDamageValuePool.Value.Get(i).value = _baseCriticalDamageValuePool.Value.Get(i).baseValue; _eventUpdatedCriticalDamageValuePool.Value.Add(i); }
            foreach (var i in _eventUpdatedDamageOrbValueFilter.Value) _eventUpdatedDamageOrbValuePool.Value.Del(i);
            foreach (var i in _baseDamageOrbValueFilter.Value) { _DamageOrbValuePool.Value.Get(i).value = _baseDamageOrbValuePool.Value.Get(i).baseValue; _eventUpdatedDamageOrbValuePool.Value.Add(i); }
            foreach (var i in _eventUpdatedDamagePercentValueFilter.Value) _eventUpdatedDamagePercentValuePool.Value.Del(i);
            foreach (var i in _baseDamagePercentValueFilter.Value) { _DamagePercentValuePool.Value.Get(i).value = _baseDamagePercentValuePool.Value.Get(i).baseValue; _eventUpdatedDamagePercentValuePool.Value.Add(i); }
            foreach (var i in _eventUpdatedDamageValueFilter.Value) _eventUpdatedDamageValuePool.Value.Del(i);
            foreach (var i in _baseDamageValueFilter.Value) { _DamageValuePool.Value.Get(i).value = _baseDamageValuePool.Value.Get(i).baseValue; _eventUpdatedDamageValuePool.Value.Add(i); }
            foreach (var i in _eventUpdatedDefenceValueFilter.Value) _eventUpdatedDefenceValuePool.Value.Del(i);
            foreach (var i in _baseDefenceValueFilter.Value) { _DefenceValuePool.Value.Get(i).value = _baseDefenceValuePool.Value.Get(i).baseValue; _eventUpdatedDefenceValuePool.Value.Add(i); }
            foreach (var i in _eventUpdatedEnergyMaxValueFilter.Value) _eventUpdatedEnergyMaxValuePool.Value.Del(i);
            foreach (var i in _baseEnergyMaxValueFilter.Value) { _EnergyMaxValuePool.Value.Get(i).value = _baseEnergyMaxValuePool.Value.Get(i).baseValue; _eventUpdatedEnergyMaxValuePool.Value.Add(i); }
            foreach (var i in _eventUpdatedEnergyValueFilter.Value) _eventUpdatedEnergyValuePool.Value.Del(i);
            foreach (var i in _baseEnergyValueFilter.Value) { _EnergyValuePool.Value.Get(i).value = _baseEnergyValuePool.Value.Get(i).baseValue; _eventUpdatedEnergyValuePool.Value.Add(i); }
            foreach (var i in _eventUpdatedExperienceValueFilter.Value) _eventUpdatedExperienceValuePool.Value.Del(i);
            foreach (var i in _baseExperienceValueFilter.Value) { _ExperienceValuePool.Value.Get(i).value = _baseExperienceValuePool.Value.Get(i).baseValue; _eventUpdatedExperienceValuePool.Value.Add(i); }
            foreach (var i in _eventUpdatedHealingPotionMaxValueFilter.Value) _eventUpdatedHealingPotionMaxValuePool.Value.Del(i);
            foreach (var i in _baseHealingPotionMaxValueFilter.Value) { _HealingPotionMaxValuePool.Value.Get(i).value = _baseHealingPotionMaxValuePool.Value.Get(i).baseValue; _eventUpdatedHealingPotionMaxValuePool.Value.Add(i); }
            foreach (var i in _eventUpdatedHealingPotionValueFilter.Value) _eventUpdatedHealingPotionValuePool.Value.Del(i);
            foreach (var i in _baseHealingPotionValueFilter.Value) { _HealingPotionValuePool.Value.Get(i).value = _baseHealingPotionValuePool.Value.Get(i).baseValue; _eventUpdatedHealingPotionValuePool.Value.Add(i); }
            foreach (var i in _eventUpdatedHitPointMaxValueFilter.Value) _eventUpdatedHitPointMaxValuePool.Value.Del(i);
            foreach (var i in _baseHitPointMaxValueFilter.Value) { _HitPointMaxValuePool.Value.Get(i).value = _baseHitPointMaxValuePool.Value.Get(i).baseValue; _eventUpdatedHitPointMaxValuePool.Value.Add(i); }
            foreach (var i in _eventUpdatedHitPointPercentValueFilter.Value) _eventUpdatedHitPointPercentValuePool.Value.Del(i);
            foreach (var i in _baseHitPointPercentValueFilter.Value) { _HitPointPercentValuePool.Value.Get(i).value = _baseHitPointPercentValuePool.Value.Get(i).baseValue; _eventUpdatedHitPointPercentValuePool.Value.Add(i); }
            foreach (var i in _eventUpdatedHitPointValueFilter.Value) _eventUpdatedHitPointValuePool.Value.Del(i);
            foreach (var i in _baseHitPointValueFilter.Value) { _HitPointValuePool.Value.Get(i).value = _baseHitPointValuePool.Value.Get(i).baseValue; _eventUpdatedHitPointValuePool.Value.Add(i); }
            foreach (var i in _eventUpdatedLivesValueFilter.Value) _eventUpdatedLivesValuePool.Value.Del(i);
            foreach (var i in _baseLivesValueFilter.Value) { _LivesValuePool.Value.Get(i).value = _baseLivesValuePool.Value.Get(i).baseValue; _eventUpdatedLivesValuePool.Value.Add(i); }
            foreach (var i in _eventUpdatedManaPointMaxValueFilter.Value) _eventUpdatedManaPointMaxValuePool.Value.Del(i);
            foreach (var i in _baseManaPointMaxValueFilter.Value) { _ManaPointMaxValuePool.Value.Get(i).value = _baseManaPointMaxValuePool.Value.Get(i).baseValue; _eventUpdatedManaPointMaxValuePool.Value.Add(i); }
            foreach (var i in _eventUpdatedManaPointValueFilter.Value) _eventUpdatedManaPointValuePool.Value.Del(i);
            foreach (var i in _baseManaPointValueFilter.Value) { _ManaPointValuePool.Value.Get(i).value = _baseManaPointValuePool.Value.Get(i).baseValue; _eventUpdatedManaPointValuePool.Value.Add(i); }
            foreach (var i in _eventUpdatedMoveSpeedValueFilter.Value) _eventUpdatedMoveSpeedValuePool.Value.Del(i);
            foreach (var i in _baseMoveSpeedValueFilter.Value) { _MoveSpeedValuePool.Value.Get(i).value = _baseMoveSpeedValuePool.Value.Get(i).baseValue; _eventUpdatedMoveSpeedValuePool.Value.Add(i); }
            foreach (var i in _eventUpdatedScoreValueFilter.Value) _eventUpdatedScoreValuePool.Value.Del(i);
            foreach (var i in _baseScoreValueFilter.Value) { _ScoreValuePool.Value.Get(i).value = _baseScoreValuePool.Value.Get(i).baseValue; _eventUpdatedScoreValuePool.Value.Add(i); }
            foreach (var i in _eventUpdatedShockWaveValueFilter.Value) _eventUpdatedShockWaveValuePool.Value.Del(i);
            foreach (var i in _baseShockWaveValueFilter.Value) { _ShockWaveValuePool.Value.Get(i).value = _baseShockWaveValuePool.Value.Get(i).baseValue; _eventUpdatedShockWaveValuePool.Value.Add(i); }
            foreach (var i in _eventUpdatedRecoverySpeedHitPointValueFilter.Value) _eventUpdatedRecoverySpeedHitPointValuePool.Value.Del(i);
            foreach (var i in _baseRecoverySpeedHitPointValueFilter.Value) { _RecoverySpeedHitPointValuePool.Value.Get(i).value = _baseRecoverySpeedHitPointValuePool.Value.Get(i).baseValue; _eventUpdatedRecoverySpeedHitPointValuePool.Value.Add(i); }
            foreach (var i in _eventUpdatedRecoverySpeedManaPointValueFilter.Value) _eventUpdatedRecoverySpeedManaPointValuePool.Value.Del(i);
            foreach (var i in _baseRecoverySpeedManaPointValueFilter.Value) { _RecoverySpeedManaPointValuePool.Value.Get(i).value = _baseRecoverySpeedManaPointValuePool.Value.Get(i).baseValue; _eventUpdatedRecoverySpeedManaPointValuePool.Value.Add(i); }
        }
    }
}