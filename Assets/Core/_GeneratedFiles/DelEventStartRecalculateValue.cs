//Файл генерируется в GenDelEventStartRecalculateValue
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Core.Generated
{
    // @formatter:off
    public class DelEventStartRecalculateValue : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<AddScoreOnDeathValueComponent>>> _AddScoreOnDeathValueFilter;
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<AngularSpeedValueComponent>>> _AngularSpeedValueFilter;
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<AttackSpeedValueComponent>>> _AttackSpeedValueFilter;
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<CriticalChanceValueComponent>>> _CriticalChanceValueFilter;
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<CriticalDamageValueComponent>>> _CriticalDamageValueFilter;
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<DamageOrbValueComponent>>> _DamageOrbValueFilter;
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<DamagePercentValueComponent>>> _DamagePercentValueFilter;
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<DamageValueComponent>>> _DamageValueFilter;
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<DefenceValueComponent>>> _DefenceValueFilter;
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<EnergyMaxValueComponent>>> _EnergyMaxValueFilter;
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<EnergyValueComponent>>> _EnergyValueFilter;
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<ExperienceValueComponent>>> _ExperienceValueFilter;
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<HealingPotionMaxValueComponent>>> _HealingPotionMaxValueFilter;
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<HealingPotionValueComponent>>> _HealingPotionValueFilter;
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<HitPointMaxValueComponent>>> _HitPointMaxValueFilter;
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<HitPointPercentValueComponent>>> _HitPointPercentValueFilter;
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<HitPointValueComponent>>> _HitPointValueFilter;
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<LivesValueComponent>>> _LivesValueFilter;
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<ManaPointMaxValueComponent>>> _ManaPointMaxValueFilter;
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<ManaPointValueComponent>>> _ManaPointValueFilter;
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<MoveSpeedValueComponent>>> _MoveSpeedValueFilter;
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<ScoreValueComponent>>> _ScoreValueFilter;
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<ShockWaveValueComponent>>> _ShockWaveValueFilter;
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<RecoverySpeedValueComponent<HitPointValueComponent>>>> _RecoverySpeedHitPointValueFilter;
        private readonly EcsFilterInject<Inc<EventStartRecalculateValue<RecoverySpeedValueComponent<ManaPointValueComponent>>>> _RecoverySpeedManaPointValueFilter;
                
        private readonly EcsPoolInject<EventStartRecalculateValue<AddScoreOnDeathValueComponent>> _AddScoreOnDeathValuePool;
        private readonly EcsPoolInject<EventStartRecalculateValue<AngularSpeedValueComponent>> _AngularSpeedValuePool;
        private readonly EcsPoolInject<EventStartRecalculateValue<AttackSpeedValueComponent>> _AttackSpeedValuePool;
        private readonly EcsPoolInject<EventStartRecalculateValue<CriticalChanceValueComponent>> _CriticalChanceValuePool;
        private readonly EcsPoolInject<EventStartRecalculateValue<CriticalDamageValueComponent>> _CriticalDamageValuePool;
        private readonly EcsPoolInject<EventStartRecalculateValue<DamageOrbValueComponent>> _DamageOrbValuePool;
        private readonly EcsPoolInject<EventStartRecalculateValue<DamagePercentValueComponent>> _DamagePercentValuePool;
        private readonly EcsPoolInject<EventStartRecalculateValue<DamageValueComponent>> _DamageValuePool;
        private readonly EcsPoolInject<EventStartRecalculateValue<DefenceValueComponent>> _DefenceValuePool;
        private readonly EcsPoolInject<EventStartRecalculateValue<EnergyMaxValueComponent>> _EnergyMaxValuePool;
        private readonly EcsPoolInject<EventStartRecalculateValue<EnergyValueComponent>> _EnergyValuePool;
        private readonly EcsPoolInject<EventStartRecalculateValue<ExperienceValueComponent>> _ExperienceValuePool;
        private readonly EcsPoolInject<EventStartRecalculateValue<HealingPotionMaxValueComponent>> _HealingPotionMaxValuePool;
        private readonly EcsPoolInject<EventStartRecalculateValue<HealingPotionValueComponent>> _HealingPotionValuePool;
        private readonly EcsPoolInject<EventStartRecalculateValue<HitPointMaxValueComponent>> _HitPointMaxValuePool;
        private readonly EcsPoolInject<EventStartRecalculateValue<HitPointPercentValueComponent>> _HitPointPercentValuePool;
        private readonly EcsPoolInject<EventStartRecalculateValue<HitPointValueComponent>> _HitPointValuePool;
        private readonly EcsPoolInject<EventStartRecalculateValue<LivesValueComponent>> _LivesValuePool;
        private readonly EcsPoolInject<EventStartRecalculateValue<ManaPointMaxValueComponent>> _ManaPointMaxValuePool;
        private readonly EcsPoolInject<EventStartRecalculateValue<ManaPointValueComponent>> _ManaPointValuePool;
        private readonly EcsPoolInject<EventStartRecalculateValue<MoveSpeedValueComponent>> _MoveSpeedValuePool;
        private readonly EcsPoolInject<EventStartRecalculateValue<ScoreValueComponent>> _ScoreValuePool;
        private readonly EcsPoolInject<EventStartRecalculateValue<ShockWaveValueComponent>> _ShockWaveValuePool;
        private readonly EcsPoolInject<EventStartRecalculateValue<RecoverySpeedValueComponent<HitPointValueComponent>>> _RecoverySpeedHitPointValuePool;
        private readonly EcsPoolInject<EventStartRecalculateValue<RecoverySpeedValueComponent<ManaPointValueComponent>>> _RecoverySpeedManaPointValuePool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _AddScoreOnDeathValueFilter.Value) _AddScoreOnDeathValuePool.Value.Del(i);
            foreach (var i in _AngularSpeedValueFilter.Value) _AngularSpeedValuePool.Value.Del(i);
            foreach (var i in _AttackSpeedValueFilter.Value) _AttackSpeedValuePool.Value.Del(i);
            foreach (var i in _CriticalChanceValueFilter.Value) _CriticalChanceValuePool.Value.Del(i);
            foreach (var i in _CriticalDamageValueFilter.Value) _CriticalDamageValuePool.Value.Del(i);
            foreach (var i in _DamageOrbValueFilter.Value) _DamageOrbValuePool.Value.Del(i);
            foreach (var i in _DamagePercentValueFilter.Value) _DamagePercentValuePool.Value.Del(i);
            foreach (var i in _DamageValueFilter.Value) _DamageValuePool.Value.Del(i);
            foreach (var i in _DefenceValueFilter.Value) _DefenceValuePool.Value.Del(i);
            foreach (var i in _EnergyMaxValueFilter.Value) _EnergyMaxValuePool.Value.Del(i);
            foreach (var i in _EnergyValueFilter.Value) _EnergyValuePool.Value.Del(i);
            foreach (var i in _ExperienceValueFilter.Value) _ExperienceValuePool.Value.Del(i);
            foreach (var i in _HealingPotionMaxValueFilter.Value) _HealingPotionMaxValuePool.Value.Del(i);
            foreach (var i in _HealingPotionValueFilter.Value) _HealingPotionValuePool.Value.Del(i);
            foreach (var i in _HitPointMaxValueFilter.Value) _HitPointMaxValuePool.Value.Del(i);
            foreach (var i in _HitPointPercentValueFilter.Value) _HitPointPercentValuePool.Value.Del(i);
            foreach (var i in _HitPointValueFilter.Value) _HitPointValuePool.Value.Del(i);
            foreach (var i in _LivesValueFilter.Value) _LivesValuePool.Value.Del(i);
            foreach (var i in _ManaPointMaxValueFilter.Value) _ManaPointMaxValuePool.Value.Del(i);
            foreach (var i in _ManaPointValueFilter.Value) _ManaPointValuePool.Value.Del(i);
            foreach (var i in _MoveSpeedValueFilter.Value) _MoveSpeedValuePool.Value.Del(i);
            foreach (var i in _ScoreValueFilter.Value) _ScoreValuePool.Value.Del(i);
            foreach (var i in _ShockWaveValueFilter.Value) _ShockWaveValuePool.Value.Del(i);
            foreach (var i in _RecoverySpeedHitPointValueFilter.Value) _RecoverySpeedHitPointValuePool.Value.Del(i);
            foreach (var i in _RecoverySpeedManaPointValueFilter.Value) _RecoverySpeedManaPointValuePool.Value.Del(i);
        }
    }
}