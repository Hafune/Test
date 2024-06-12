//Файл генерируется в GenInitRecalculateAllValuesSystem
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Generated
{
    // @formatter:off
    public class InitRecalculateAllValuesSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<EventInit, AddScoreOnDeathValueComponent>, Exc<EventStartRecalculateValue<AddScoreOnDeathValueComponent>>> _AddScoreOnDeathValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, AngularSpeedValueComponent>, Exc<EventStartRecalculateValue<AngularSpeedValueComponent>>> _AngularSpeedValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, AttackSpeedValueComponent>, Exc<EventStartRecalculateValue<AttackSpeedValueComponent>>> _AttackSpeedValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, CriticalChanceValueComponent>, Exc<EventStartRecalculateValue<CriticalChanceValueComponent>>> _CriticalChanceValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, CriticalDamageValueComponent>, Exc<EventStartRecalculateValue<CriticalDamageValueComponent>>> _CriticalDamageValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, DamageOrbValueComponent>, Exc<EventStartRecalculateValue<DamageOrbValueComponent>>> _DamageOrbValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, DamagePercentValueComponent>, Exc<EventStartRecalculateValue<DamagePercentValueComponent>>> _DamagePercentValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, DamageValueComponent>, Exc<EventStartRecalculateValue<DamageValueComponent>>> _DamageValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, DefenceValueComponent>, Exc<EventStartRecalculateValue<DefenceValueComponent>>> _DefenceValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, EnergyMaxValueComponent>, Exc<EventStartRecalculateValue<EnergyMaxValueComponent>>> _EnergyMaxValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, EnergyValueComponent>, Exc<EventStartRecalculateValue<EnergyValueComponent>>> _EnergyValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, ExperienceValueComponent>, Exc<EventStartRecalculateValue<ExperienceValueComponent>>> _ExperienceValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, HealingPotionMaxValueComponent>, Exc<EventStartRecalculateValue<HealingPotionMaxValueComponent>>> _HealingPotionMaxValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, HealingPotionValueComponent>, Exc<EventStartRecalculateValue<HealingPotionValueComponent>>> _HealingPotionValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, HitPointMaxValueComponent>, Exc<EventStartRecalculateValue<HitPointMaxValueComponent>>> _HitPointMaxValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, HitPointPercentValueComponent>, Exc<EventStartRecalculateValue<HitPointPercentValueComponent>>> _HitPointPercentValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, HitPointValueComponent>, Exc<EventStartRecalculateValue<HitPointValueComponent>>> _HitPointValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, LivesValueComponent>, Exc<EventStartRecalculateValue<LivesValueComponent>>> _LivesValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, ManaPointMaxValueComponent>, Exc<EventStartRecalculateValue<ManaPointMaxValueComponent>>> _ManaPointMaxValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, ManaPointValueComponent>, Exc<EventStartRecalculateValue<ManaPointValueComponent>>> _ManaPointValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, MoveSpeedValueComponent>, Exc<EventStartRecalculateValue<MoveSpeedValueComponent>>> _MoveSpeedValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, ScoreValueComponent>, Exc<EventStartRecalculateValue<ScoreValueComponent>>> _ScoreValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, ShockWaveValueComponent>, Exc<EventStartRecalculateValue<ShockWaveValueComponent>>> _ShockWaveValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, RecoverySpeedValueComponent<HitPointValueComponent>>, Exc<EventStartRecalculateValue<RecoverySpeedValueComponent<HitPointValueComponent>>>> _RecoverySpeedHitPointValueFilter;
        private readonly EcsFilterInject<Inc<EventInit, RecoverySpeedValueComponent<ManaPointValueComponent>>, Exc<EventStartRecalculateValue<RecoverySpeedValueComponent<ManaPointValueComponent>>>> _RecoverySpeedManaPointValueFilter;
        
        private readonly ComponentPools _pools;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _AddScoreOnDeathValueFilter.Value) _pools.EventStartRecalculateAddScoreOnDeathValue.Add(i);
            foreach (var i in _AngularSpeedValueFilter.Value) _pools.EventStartRecalculateAngularSpeedValue.Add(i);
            foreach (var i in _AttackSpeedValueFilter.Value) _pools.EventStartRecalculateAttackSpeedValue.Add(i);
            foreach (var i in _CriticalChanceValueFilter.Value) _pools.EventStartRecalculateCriticalChanceValue.Add(i);
            foreach (var i in _CriticalDamageValueFilter.Value) _pools.EventStartRecalculateCriticalDamageValue.Add(i);
            foreach (var i in _DamageOrbValueFilter.Value) _pools.EventStartRecalculateDamageOrbValue.Add(i);
            foreach (var i in _DamagePercentValueFilter.Value) _pools.EventStartRecalculateDamagePercentValue.Add(i);
            foreach (var i in _DamageValueFilter.Value) _pools.EventStartRecalculateDamageValue.Add(i);
            foreach (var i in _DefenceValueFilter.Value) _pools.EventStartRecalculateDefenceValue.Add(i);
            foreach (var i in _EnergyMaxValueFilter.Value) _pools.EventStartRecalculateEnergyMaxValue.Add(i);
            foreach (var i in _EnergyValueFilter.Value) _pools.EventStartRecalculateEnergyValue.Add(i);
            foreach (var i in _ExperienceValueFilter.Value) _pools.EventStartRecalculateExperienceValue.Add(i);
            foreach (var i in _HealingPotionMaxValueFilter.Value) _pools.EventStartRecalculateHealingPotionMaxValue.Add(i);
            foreach (var i in _HealingPotionValueFilter.Value) _pools.EventStartRecalculateHealingPotionValue.Add(i);
            foreach (var i in _HitPointMaxValueFilter.Value) _pools.EventStartRecalculateHitPointMaxValue.Add(i);
            foreach (var i in _HitPointPercentValueFilter.Value) _pools.EventStartRecalculateHitPointPercentValue.Add(i);
            foreach (var i in _HitPointValueFilter.Value) _pools.EventStartRecalculateHitPointValue.Add(i);
            foreach (var i in _LivesValueFilter.Value) _pools.EventStartRecalculateLivesValue.Add(i);
            foreach (var i in _ManaPointMaxValueFilter.Value) _pools.EventStartRecalculateManaPointMaxValue.Add(i);
            foreach (var i in _ManaPointValueFilter.Value) _pools.EventStartRecalculateManaPointValue.Add(i);
            foreach (var i in _MoveSpeedValueFilter.Value) _pools.EventStartRecalculateMoveSpeedValue.Add(i);
            foreach (var i in _ScoreValueFilter.Value) _pools.EventStartRecalculateScoreValue.Add(i);
            foreach (var i in _ShockWaveValueFilter.Value) _pools.EventStartRecalculateShockWaveValue.Add(i);
            foreach (var i in _RecoverySpeedHitPointValueFilter.Value) _pools.EventStartRecalculateRecoverySpeedHitPointValue.Add(i);
            foreach (var i in _RecoverySpeedManaPointValueFilter.Value) _pools.EventStartRecalculateRecoverySpeedManaPointValue.Add(i);
        }
    }
}