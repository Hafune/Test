//Файл генерируется в GenEventRefreshValueBySlotSystem
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using static Core.Components.SlotFilterAndPools;

namespace Core.Generated
{
    // @formatter:off
    public class EventRefreshValueBySlotSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<EventValueUpdated<AddScoreOnDeathValueComponent>>> AddScoreOnDeathValueFilter;
        private readonly EcsPoolInject<AddScoreOnDeathValueComponent> AddScoreOnDeathValuePool;
        private readonly RefreshValueBySlotWrapper<AddScoreOnDeathValueComponent> AddScoreOnDeathValueWrapper;
        private readonly EcsFilterInject<Inc<EventValueUpdated<AngularSpeedValueComponent>>> AngularSpeedValueFilter;
        private readonly EcsPoolInject<AngularSpeedValueComponent> AngularSpeedValuePool;
        private readonly RefreshValueBySlotWrapper<AngularSpeedValueComponent> AngularSpeedValueWrapper;
        private readonly EcsFilterInject<Inc<EventValueUpdated<AttackSpeedValueComponent>>> AttackSpeedValueFilter;
        private readonly EcsPoolInject<AttackSpeedValueComponent> AttackSpeedValuePool;
        private readonly RefreshValueBySlotWrapper<AttackSpeedValueComponent> AttackSpeedValueWrapper;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CriticalChanceValueComponent>>> CriticalChanceValueFilter;
        private readonly EcsPoolInject<CriticalChanceValueComponent> CriticalChanceValuePool;
        private readonly RefreshValueBySlotWrapper<CriticalChanceValueComponent> CriticalChanceValueWrapper;
        private readonly EcsFilterInject<Inc<EventValueUpdated<CriticalDamageValueComponent>>> CriticalDamageValueFilter;
        private readonly EcsPoolInject<CriticalDamageValueComponent> CriticalDamageValuePool;
        private readonly RefreshValueBySlotWrapper<CriticalDamageValueComponent> CriticalDamageValueWrapper;
        private readonly EcsFilterInject<Inc<EventValueUpdated<DamageOrbValueComponent>>> DamageOrbValueFilter;
        private readonly EcsPoolInject<DamageOrbValueComponent> DamageOrbValuePool;
        private readonly RefreshValueBySlotWrapper<DamageOrbValueComponent> DamageOrbValueWrapper;
        private readonly EcsFilterInject<Inc<EventValueUpdated<DamagePercentValueComponent>>> DamagePercentValueFilter;
        private readonly EcsPoolInject<DamagePercentValueComponent> DamagePercentValuePool;
        private readonly RefreshValueBySlotWrapper<DamagePercentValueComponent> DamagePercentValueWrapper;
        private readonly EcsFilterInject<Inc<EventValueUpdated<DamageValueComponent>>> DamageValueFilter;
        private readonly EcsPoolInject<DamageValueComponent> DamageValuePool;
        private readonly RefreshValueBySlotWrapper<DamageValueComponent> DamageValueWrapper;
        private readonly EcsFilterInject<Inc<EventValueUpdated<DefenceValueComponent>>> DefenceValueFilter;
        private readonly EcsPoolInject<DefenceValueComponent> DefenceValuePool;
        private readonly RefreshValueBySlotWrapper<DefenceValueComponent> DefenceValueWrapper;
        private readonly EcsFilterInject<Inc<EventValueUpdated<EnergyMaxValueComponent>>> EnergyMaxValueFilter;
        private readonly EcsPoolInject<EnergyMaxValueComponent> EnergyMaxValuePool;
        private readonly RefreshValueBySlotWrapper<EnergyMaxValueComponent> EnergyMaxValueWrapper;
        private readonly EcsFilterInject<Inc<EventValueUpdated<EnergyValueComponent>>> EnergyValueFilter;
        private readonly EcsPoolInject<EnergyValueComponent> EnergyValuePool;
        private readonly RefreshValueBySlotWrapper<EnergyValueComponent> EnergyValueWrapper;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ExperienceValueComponent>>> ExperienceValueFilter;
        private readonly EcsPoolInject<ExperienceValueComponent> ExperienceValuePool;
        private readonly RefreshValueBySlotWrapper<ExperienceValueComponent> ExperienceValueWrapper;
        private readonly EcsFilterInject<Inc<EventValueUpdated<HealingPotionMaxValueComponent>>> HealingPotionMaxValueFilter;
        private readonly EcsPoolInject<HealingPotionMaxValueComponent> HealingPotionMaxValuePool;
        private readonly RefreshValueBySlotWrapper<HealingPotionMaxValueComponent> HealingPotionMaxValueWrapper;
        private readonly EcsFilterInject<Inc<EventValueUpdated<HealingPotionValueComponent>>> HealingPotionValueFilter;
        private readonly EcsPoolInject<HealingPotionValueComponent> HealingPotionValuePool;
        private readonly RefreshValueBySlotWrapper<HealingPotionValueComponent> HealingPotionValueWrapper;
        private readonly EcsFilterInject<Inc<EventValueUpdated<HitPointMaxValueComponent>>> HitPointMaxValueFilter;
        private readonly EcsPoolInject<HitPointMaxValueComponent> HitPointMaxValuePool;
        private readonly RefreshValueBySlotWrapper<HitPointMaxValueComponent> HitPointMaxValueWrapper;
        private readonly EcsFilterInject<Inc<EventValueUpdated<HitPointPercentValueComponent>>> HitPointPercentValueFilter;
        private readonly EcsPoolInject<HitPointPercentValueComponent> HitPointPercentValuePool;
        private readonly RefreshValueBySlotWrapper<HitPointPercentValueComponent> HitPointPercentValueWrapper;
        private readonly EcsFilterInject<Inc<EventValueUpdated<HitPointValueComponent>>> HitPointValueFilter;
        private readonly EcsPoolInject<HitPointValueComponent> HitPointValuePool;
        private readonly RefreshValueBySlotWrapper<HitPointValueComponent> HitPointValueWrapper;
        private readonly EcsFilterInject<Inc<EventValueUpdated<LivesValueComponent>>> LivesValueFilter;
        private readonly EcsPoolInject<LivesValueComponent> LivesValuePool;
        private readonly RefreshValueBySlotWrapper<LivesValueComponent> LivesValueWrapper;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ManaPointMaxValueComponent>>> ManaPointMaxValueFilter;
        private readonly EcsPoolInject<ManaPointMaxValueComponent> ManaPointMaxValuePool;
        private readonly RefreshValueBySlotWrapper<ManaPointMaxValueComponent> ManaPointMaxValueWrapper;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ManaPointValueComponent>>> ManaPointValueFilter;
        private readonly EcsPoolInject<ManaPointValueComponent> ManaPointValuePool;
        private readonly RefreshValueBySlotWrapper<ManaPointValueComponent> ManaPointValueWrapper;
        private readonly EcsFilterInject<Inc<EventValueUpdated<MoveSpeedValueComponent>>> MoveSpeedValueFilter;
        private readonly EcsPoolInject<MoveSpeedValueComponent> MoveSpeedValuePool;
        private readonly RefreshValueBySlotWrapper<MoveSpeedValueComponent> MoveSpeedValueWrapper;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ScoreValueComponent>>> ScoreValueFilter;
        private readonly EcsPoolInject<ScoreValueComponent> ScoreValuePool;
        private readonly RefreshValueBySlotWrapper<ScoreValueComponent> ScoreValueWrapper;
        private readonly EcsFilterInject<Inc<EventValueUpdated<ShockWaveValueComponent>>> ShockWaveValueFilter;
        private readonly EcsPoolInject<ShockWaveValueComponent> ShockWaveValuePool;
        private readonly RefreshValueBySlotWrapper<ShockWaveValueComponent> ShockWaveValueWrapper;
        private readonly EcsFilterInject<Inc<EventValueUpdated<RecoverySpeedValueComponent<HitPointValueComponent>>>> RecoverySpeedHitPointValueFilter;
        private readonly EcsPoolInject<RecoverySpeedValueComponent<HitPointValueComponent>> RecoverySpeedHitPointValuePool;
        private readonly RefreshValueBySlotWrapper<RecoverySpeedValueComponent<HitPointValueComponent>> RecoverySpeedHitPointValueWrapper;
        private readonly EcsFilterInject<Inc<EventValueUpdated<RecoverySpeedValueComponent<ManaPointValueComponent>>>> RecoverySpeedManaPointValueFilter;
        private readonly EcsPoolInject<RecoverySpeedValueComponent<ManaPointValueComponent>> RecoverySpeedManaPointValuePool;
        private readonly RefreshValueBySlotWrapper<RecoverySpeedValueComponent<ManaPointValueComponent>> RecoverySpeedManaPointValueWrapper;
        
        public EventRefreshValueBySlotSystem(EcsWorld world)
        {
            AddScoreOnDeathValueWrapper = MakeSlotFiltersAndPools<AddScoreOnDeathValueComponent>(world);
            AngularSpeedValueWrapper = MakeSlotFiltersAndPools<AngularSpeedValueComponent>(world);
            AttackSpeedValueWrapper = MakeSlotFiltersAndPools<AttackSpeedValueComponent>(world);
            CriticalChanceValueWrapper = MakeSlotFiltersAndPools<CriticalChanceValueComponent>(world);
            CriticalDamageValueWrapper = MakeSlotFiltersAndPools<CriticalDamageValueComponent>(world);
            DamageOrbValueWrapper = MakeSlotFiltersAndPools<DamageOrbValueComponent>(world);
            DamagePercentValueWrapper = MakeSlotFiltersAndPools<DamagePercentValueComponent>(world);
            DamageValueWrapper = MakeSlotFiltersAndPools<DamageValueComponent>(world);
            DefenceValueWrapper = MakeSlotFiltersAndPools<DefenceValueComponent>(world);
            EnergyMaxValueWrapper = MakeSlotFiltersAndPools<EnergyMaxValueComponent>(world);
            EnergyValueWrapper = MakeSlotFiltersAndPools<EnergyValueComponent>(world);
            ExperienceValueWrapper = MakeSlotFiltersAndPools<ExperienceValueComponent>(world);
            HealingPotionMaxValueWrapper = MakeSlotFiltersAndPools<HealingPotionMaxValueComponent>(world);
            HealingPotionValueWrapper = MakeSlotFiltersAndPools<HealingPotionValueComponent>(world);
            HitPointMaxValueWrapper = MakeSlotFiltersAndPools<HitPointMaxValueComponent>(world);
            HitPointPercentValueWrapper = MakeSlotFiltersAndPools<HitPointPercentValueComponent>(world);
            HitPointValueWrapper = MakeSlotFiltersAndPools<HitPointValueComponent>(world);
            LivesValueWrapper = MakeSlotFiltersAndPools<LivesValueComponent>(world);
            ManaPointMaxValueWrapper = MakeSlotFiltersAndPools<ManaPointMaxValueComponent>(world);
            ManaPointValueWrapper = MakeSlotFiltersAndPools<ManaPointValueComponent>(world);
            MoveSpeedValueWrapper = MakeSlotFiltersAndPools<MoveSpeedValueComponent>(world);
            ScoreValueWrapper = MakeSlotFiltersAndPools<ScoreValueComponent>(world);
            ShockWaveValueWrapper = MakeSlotFiltersAndPools<ShockWaveValueComponent>(world);
            RecoverySpeedHitPointValueWrapper = MakeSlotFiltersAndPools<RecoverySpeedValueComponent<HitPointValueComponent>>(world);
            RecoverySpeedManaPointValueWrapper = MakeSlotFiltersAndPools<RecoverySpeedValueComponent<ManaPointValueComponent>>(world);
        }

        public void Run(IEcsSystems systems)
        {
            if (AddScoreOnDeathValueFilter.Value.GetEntitiesCount() != 0) AddScoreOnDeathValueWrapper.AddSlotValues(AddScoreOnDeathValuePool.Value);
            if (AngularSpeedValueFilter.Value.GetEntitiesCount() != 0) AngularSpeedValueWrapper.AddSlotValues(AngularSpeedValuePool.Value);
            if (AttackSpeedValueFilter.Value.GetEntitiesCount() != 0) AttackSpeedValueWrapper.AddSlotValues(AttackSpeedValuePool.Value);
            if (CriticalChanceValueFilter.Value.GetEntitiesCount() != 0) CriticalChanceValueWrapper.AddSlotValues(CriticalChanceValuePool.Value);
            if (CriticalDamageValueFilter.Value.GetEntitiesCount() != 0) CriticalDamageValueWrapper.AddSlotValues(CriticalDamageValuePool.Value);
            if (DamageOrbValueFilter.Value.GetEntitiesCount() != 0) DamageOrbValueWrapper.AddSlotValues(DamageOrbValuePool.Value);
            if (DamagePercentValueFilter.Value.GetEntitiesCount() != 0) DamagePercentValueWrapper.AddSlotValues(DamagePercentValuePool.Value);
            if (DamageValueFilter.Value.GetEntitiesCount() != 0) DamageValueWrapper.AddSlotValues(DamageValuePool.Value);
            if (DefenceValueFilter.Value.GetEntitiesCount() != 0) DefenceValueWrapper.AddSlotValues(DefenceValuePool.Value);
            if (EnergyMaxValueFilter.Value.GetEntitiesCount() != 0) EnergyMaxValueWrapper.AddSlotValues(EnergyMaxValuePool.Value);
            if (EnergyValueFilter.Value.GetEntitiesCount() != 0) EnergyValueWrapper.AddSlotValues(EnergyValuePool.Value);
            if (ExperienceValueFilter.Value.GetEntitiesCount() != 0) ExperienceValueWrapper.AddSlotValues(ExperienceValuePool.Value);
            if (HealingPotionMaxValueFilter.Value.GetEntitiesCount() != 0) HealingPotionMaxValueWrapper.AddSlotValues(HealingPotionMaxValuePool.Value);
            if (HealingPotionValueFilter.Value.GetEntitiesCount() != 0) HealingPotionValueWrapper.AddSlotValues(HealingPotionValuePool.Value);
            if (HitPointMaxValueFilter.Value.GetEntitiesCount() != 0) HitPointMaxValueWrapper.AddSlotValues(HitPointMaxValuePool.Value);
            if (HitPointPercentValueFilter.Value.GetEntitiesCount() != 0) HitPointPercentValueWrapper.AddSlotValues(HitPointPercentValuePool.Value);
            if (HitPointValueFilter.Value.GetEntitiesCount() != 0) HitPointValueWrapper.AddSlotValues(HitPointValuePool.Value);
            if (LivesValueFilter.Value.GetEntitiesCount() != 0) LivesValueWrapper.AddSlotValues(LivesValuePool.Value);
            if (ManaPointMaxValueFilter.Value.GetEntitiesCount() != 0) ManaPointMaxValueWrapper.AddSlotValues(ManaPointMaxValuePool.Value);
            if (ManaPointValueFilter.Value.GetEntitiesCount() != 0) ManaPointValueWrapper.AddSlotValues(ManaPointValuePool.Value);
            if (MoveSpeedValueFilter.Value.GetEntitiesCount() != 0) MoveSpeedValueWrapper.AddSlotValues(MoveSpeedValuePool.Value);
            if (ScoreValueFilter.Value.GetEntitiesCount() != 0) ScoreValueWrapper.AddSlotValues(ScoreValuePool.Value);
            if (ShockWaveValueFilter.Value.GetEntitiesCount() != 0) ShockWaveValueWrapper.AddSlotValues(ShockWaveValuePool.Value);
            if (RecoverySpeedHitPointValueFilter.Value.GetEntitiesCount() != 0) RecoverySpeedHitPointValueWrapper.AddSlotValues(RecoverySpeedHitPointValuePool.Value);
            if (RecoverySpeedManaPointValueFilter.Value.GetEntitiesCount() != 0) RecoverySpeedManaPointValueWrapper.AddSlotValues(RecoverySpeedManaPointValuePool.Value);
        }
    }
}