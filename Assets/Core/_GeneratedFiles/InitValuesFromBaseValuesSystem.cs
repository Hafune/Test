//Файл генерируется в GenInitValuesFromBaseValuesSystem
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Generated
{
    // @formatter:off
    public class InitValuesFromBaseValuesSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<EventInit>> _hasInitFilter;
        
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<AddScoreOnDeathValueComponent>>> _baseAddScoreOnDeathValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<AngularSpeedValueComponent>>> _baseAngularSpeedValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<AttackSpeedValueComponent>>> _baseAttackSpeedValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<CriticalChanceValueComponent>>> _baseCriticalChanceValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<CriticalDamageValueComponent>>> _baseCriticalDamageValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<DamageOrbValueComponent>>> _baseDamageOrbValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<DamagePercentValueComponent>>> _baseDamagePercentValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<DamageValueComponent>>> _baseDamageValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<DefenceValueComponent>>> _baseDefenceValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<EnergyMaxValueComponent>>> _baseEnergyMaxValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<EnergyValueComponent>>> _baseEnergyValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<ExperienceValueComponent>>> _baseExperienceValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<HealingPotionMaxValueComponent>>> _baseHealingPotionMaxValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<HealingPotionValueComponent>>> _baseHealingPotionValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<HitPointMaxValueComponent>>> _baseHitPointMaxValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<HitPointPercentValueComponent>>> _baseHitPointPercentValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<HitPointValueComponent>>> _baseHitPointValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<LivesValueComponent>>> _baseLivesValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<ManaPointMaxValueComponent>>> _baseManaPointMaxValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<ManaPointValueComponent>>> _baseManaPointValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<MoveSpeedValueComponent>>> _baseMoveSpeedValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<ScoreValueComponent>>> _baseScoreValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<ShockWaveValueComponent>>> _baseShockWaveValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<RecoverySpeedValueComponent<HitPointValueComponent>>>> _baseRecoverySpeedHitPointValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, BaseValueComponent<RecoverySpeedValueComponent<ManaPointValueComponent>>>> _baseRecoverySpeedManaPointValueFilter;

        private readonly EcsPoolInject<BaseValueComponent<AddScoreOnDeathValueComponent>> _baseAddScoreOnDeathValuePool;
        private readonly EcsPoolInject<AddScoreOnDeathValueComponent> _AddScoreOnDeathValuePool;
        private readonly EcsPoolInject<BaseValueComponent<AngularSpeedValueComponent>> _baseAngularSpeedValuePool;
        private readonly EcsPoolInject<AngularSpeedValueComponent> _AngularSpeedValuePool;
        private readonly EcsPoolInject<BaseValueComponent<AttackSpeedValueComponent>> _baseAttackSpeedValuePool;
        private readonly EcsPoolInject<AttackSpeedValueComponent> _AttackSpeedValuePool;
        private readonly EcsPoolInject<BaseValueComponent<CriticalChanceValueComponent>> _baseCriticalChanceValuePool;
        private readonly EcsPoolInject<CriticalChanceValueComponent> _CriticalChanceValuePool;
        private readonly EcsPoolInject<BaseValueComponent<CriticalDamageValueComponent>> _baseCriticalDamageValuePool;
        private readonly EcsPoolInject<CriticalDamageValueComponent> _CriticalDamageValuePool;
        private readonly EcsPoolInject<BaseValueComponent<DamageOrbValueComponent>> _baseDamageOrbValuePool;
        private readonly EcsPoolInject<DamageOrbValueComponent> _DamageOrbValuePool;
        private readonly EcsPoolInject<BaseValueComponent<DamagePercentValueComponent>> _baseDamagePercentValuePool;
        private readonly EcsPoolInject<DamagePercentValueComponent> _DamagePercentValuePool;
        private readonly EcsPoolInject<BaseValueComponent<DamageValueComponent>> _baseDamageValuePool;
        private readonly EcsPoolInject<DamageValueComponent> _DamageValuePool;
        private readonly EcsPoolInject<BaseValueComponent<DefenceValueComponent>> _baseDefenceValuePool;
        private readonly EcsPoolInject<DefenceValueComponent> _DefenceValuePool;
        private readonly EcsPoolInject<BaseValueComponent<EnergyMaxValueComponent>> _baseEnergyMaxValuePool;
        private readonly EcsPoolInject<EnergyMaxValueComponent> _EnergyMaxValuePool;
        private readonly EcsPoolInject<BaseValueComponent<EnergyValueComponent>> _baseEnergyValuePool;
        private readonly EcsPoolInject<EnergyValueComponent> _EnergyValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ExperienceValueComponent>> _baseExperienceValuePool;
        private readonly EcsPoolInject<ExperienceValueComponent> _ExperienceValuePool;
        private readonly EcsPoolInject<BaseValueComponent<HealingPotionMaxValueComponent>> _baseHealingPotionMaxValuePool;
        private readonly EcsPoolInject<HealingPotionMaxValueComponent> _HealingPotionMaxValuePool;
        private readonly EcsPoolInject<BaseValueComponent<HealingPotionValueComponent>> _baseHealingPotionValuePool;
        private readonly EcsPoolInject<HealingPotionValueComponent> _HealingPotionValuePool;
        private readonly EcsPoolInject<BaseValueComponent<HitPointMaxValueComponent>> _baseHitPointMaxValuePool;
        private readonly EcsPoolInject<HitPointMaxValueComponent> _HitPointMaxValuePool;
        private readonly EcsPoolInject<BaseValueComponent<HitPointPercentValueComponent>> _baseHitPointPercentValuePool;
        private readonly EcsPoolInject<HitPointPercentValueComponent> _HitPointPercentValuePool;
        private readonly EcsPoolInject<BaseValueComponent<HitPointValueComponent>> _baseHitPointValuePool;
        private readonly EcsPoolInject<HitPointValueComponent> _HitPointValuePool;
        private readonly EcsPoolInject<BaseValueComponent<LivesValueComponent>> _baseLivesValuePool;
        private readonly EcsPoolInject<LivesValueComponent> _LivesValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ManaPointMaxValueComponent>> _baseManaPointMaxValuePool;
        private readonly EcsPoolInject<ManaPointMaxValueComponent> _ManaPointMaxValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ManaPointValueComponent>> _baseManaPointValuePool;
        private readonly EcsPoolInject<ManaPointValueComponent> _ManaPointValuePool;
        private readonly EcsPoolInject<BaseValueComponent<MoveSpeedValueComponent>> _baseMoveSpeedValuePool;
        private readonly EcsPoolInject<MoveSpeedValueComponent> _MoveSpeedValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ScoreValueComponent>> _baseScoreValuePool;
        private readonly EcsPoolInject<ScoreValueComponent> _ScoreValuePool;
        private readonly EcsPoolInject<BaseValueComponent<ShockWaveValueComponent>> _baseShockWaveValuePool;
        private readonly EcsPoolInject<ShockWaveValueComponent> _ShockWaveValuePool;
        private readonly EcsPoolInject<BaseValueComponent<RecoverySpeedValueComponent<HitPointValueComponent>>> _baseRecoverySpeedHitPointValuePool;
        private readonly EcsPoolInject<RecoverySpeedValueComponent<HitPointValueComponent>> _RecoverySpeedHitPointValuePool;
        private readonly EcsPoolInject<BaseValueComponent<RecoverySpeedValueComponent<ManaPointValueComponent>>> _baseRecoverySpeedManaPointValuePool;
        private readonly EcsPoolInject<RecoverySpeedValueComponent<ManaPointValueComponent>> _RecoverySpeedManaPointValuePool;

        public void Run(IEcsSystems systems)
        {
            if (_hasInitFilter.Value.GetEntitiesCount() == 0)
                return;
            
            foreach (var i in _baseAddScoreOnDeathValueFilter.Value) _AddScoreOnDeathValuePool.Value.Add(i).value = _baseAddScoreOnDeathValuePool.Value.Get(i).baseValue;
            foreach (var i in _baseAngularSpeedValueFilter.Value) _AngularSpeedValuePool.Value.Add(i).value = _baseAngularSpeedValuePool.Value.Get(i).baseValue;
            foreach (var i in _baseAttackSpeedValueFilter.Value) _AttackSpeedValuePool.Value.Add(i).value = _baseAttackSpeedValuePool.Value.Get(i).baseValue;
            foreach (var i in _baseCriticalChanceValueFilter.Value) _CriticalChanceValuePool.Value.Add(i).value = _baseCriticalChanceValuePool.Value.Get(i).baseValue;
            foreach (var i in _baseCriticalDamageValueFilter.Value) _CriticalDamageValuePool.Value.Add(i).value = _baseCriticalDamageValuePool.Value.Get(i).baseValue;
            foreach (var i in _baseDamageOrbValueFilter.Value) _DamageOrbValuePool.Value.Add(i).value = _baseDamageOrbValuePool.Value.Get(i).baseValue;
            foreach (var i in _baseDamagePercentValueFilter.Value) _DamagePercentValuePool.Value.Add(i).value = _baseDamagePercentValuePool.Value.Get(i).baseValue;
            foreach (var i in _baseDamageValueFilter.Value) _DamageValuePool.Value.Add(i).value = _baseDamageValuePool.Value.Get(i).baseValue;
            foreach (var i in _baseDefenceValueFilter.Value) _DefenceValuePool.Value.Add(i).value = _baseDefenceValuePool.Value.Get(i).baseValue;
            foreach (var i in _baseEnergyMaxValueFilter.Value) _EnergyMaxValuePool.Value.Add(i).value = _baseEnergyMaxValuePool.Value.Get(i).baseValue;
            foreach (var i in _baseEnergyValueFilter.Value) _EnergyValuePool.Value.Add(i).value = _baseEnergyValuePool.Value.Get(i).baseValue;
            foreach (var i in _baseExperienceValueFilter.Value) _ExperienceValuePool.Value.Add(i).value = _baseExperienceValuePool.Value.Get(i).baseValue;
            foreach (var i in _baseHealingPotionMaxValueFilter.Value) _HealingPotionMaxValuePool.Value.Add(i).value = _baseHealingPotionMaxValuePool.Value.Get(i).baseValue;
            foreach (var i in _baseHealingPotionValueFilter.Value) _HealingPotionValuePool.Value.Add(i).value = _baseHealingPotionValuePool.Value.Get(i).baseValue;
            foreach (var i in _baseHitPointMaxValueFilter.Value) _HitPointMaxValuePool.Value.Add(i).value = _baseHitPointMaxValuePool.Value.Get(i).baseValue;
            foreach (var i in _baseHitPointPercentValueFilter.Value) _HitPointPercentValuePool.Value.Add(i).value = _baseHitPointPercentValuePool.Value.Get(i).baseValue;
            foreach (var i in _baseHitPointValueFilter.Value) _HitPointValuePool.Value.Add(i).value = _baseHitPointValuePool.Value.Get(i).baseValue;
            foreach (var i in _baseLivesValueFilter.Value) _LivesValuePool.Value.Add(i).value = _baseLivesValuePool.Value.Get(i).baseValue;
            foreach (var i in _baseManaPointMaxValueFilter.Value) _ManaPointMaxValuePool.Value.Add(i).value = _baseManaPointMaxValuePool.Value.Get(i).baseValue;
            foreach (var i in _baseManaPointValueFilter.Value) _ManaPointValuePool.Value.Add(i).value = _baseManaPointValuePool.Value.Get(i).baseValue;
            foreach (var i in _baseMoveSpeedValueFilter.Value) _MoveSpeedValuePool.Value.Add(i).value = _baseMoveSpeedValuePool.Value.Get(i).baseValue;
            foreach (var i in _baseScoreValueFilter.Value) _ScoreValuePool.Value.Add(i).value = _baseScoreValuePool.Value.Get(i).baseValue;
            foreach (var i in _baseShockWaveValueFilter.Value) _ShockWaveValuePool.Value.Add(i).value = _baseShockWaveValuePool.Value.Get(i).baseValue;
            foreach (var i in _baseRecoverySpeedHitPointValueFilter.Value) _RecoverySpeedHitPointValuePool.Value.Add(i).value = _baseRecoverySpeedHitPointValuePool.Value.Get(i).baseValue;
            foreach (var i in _baseRecoverySpeedManaPointValueFilter.Value) _RecoverySpeedManaPointValuePool.Value.Add(i).value = _baseRecoverySpeedManaPointValuePool.Value.Get(i).baseValue;
        }
    }
}