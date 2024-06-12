//Файл генерируется в GenSlotFilterAndPools
// @formatter:off
using Core.Systems;
using Leopotam.EcsLite;

namespace Core.Components
{
    public static class SlotFilterAndPools
    {
        public static RefreshValueBySlotWrapper<T> MakeSlotFiltersAndPools<T>(EcsWorld world)
            where T : struct, IValue => new(world);

        private static EcsFilter MakeFilter<S, T>(EcsWorld world)
            where S : struct, ISlotData
            where T : struct, IValue => world.Filter<T>()
            .Inc<EventValueUpdated<T>>().Inc<SlotValueComponent<S, T>>().End();

        private static EcsPool<SlotValueComponent<S, T>> MakePool<S, T>(EcsWorld world)
            where S : struct, ISlotData
            where T : struct, IValue =>
            world.GetPool<SlotValueComponent<S, T>>();

        public class RefreshValueBySlotWrapper<T> where T : struct, IValue
        {
            private readonly EcsFilter AbilitiesSlotFilter;
            private readonly EcsFilter EnhancementSlotFilter;
            private readonly EcsFilter MagicGemSlotFilter;
            private readonly EcsFilter TestSlotFilter;
            private readonly EcsFilter WeaponSlotFilter;

            private readonly EcsPool<SlotValueComponent<AbilitiesSlotComponent, T>> AbilitiesSlotPool;
            private readonly EcsPool<SlotValueComponent<EnhancementSlotComponent, T>> EnhancementSlotPool;
            private readonly EcsPool<SlotValueComponent<MagicGemSlotComponent, T>> MagicGemSlotPool;
            private readonly EcsPool<SlotValueComponent<TestSlotComponent, T>> TestSlotPool;
            private readonly EcsPool<SlotValueComponent<WeaponSlotComponent, T>> WeaponSlotPool;

            public RefreshValueBySlotWrapper(EcsWorld world)
            {
                AbilitiesSlotFilter = MakeFilter<AbilitiesSlotComponent, T>(world);
                EnhancementSlotFilter = MakeFilter<EnhancementSlotComponent, T>(world);
                MagicGemSlotFilter = MakeFilter<MagicGemSlotComponent, T>(world);
                TestSlotFilter = MakeFilter<TestSlotComponent, T>(world);
                WeaponSlotFilter = MakeFilter<WeaponSlotComponent, T>(world);

                AbilitiesSlotPool = MakePool<AbilitiesSlotComponent, T>(world);
                EnhancementSlotPool = MakePool<EnhancementSlotComponent, T>(world);
                MagicGemSlotPool = MakePool<MagicGemSlotComponent, T>(world);
                TestSlotPool = MakePool<TestSlotComponent, T>(world);
                WeaponSlotPool = MakePool<WeaponSlotComponent, T>(world);
            }

            public void AddSlotValues(EcsPool<T> targetPool)
            {
                foreach (var entity in AbilitiesSlotFilter) targetPool.Get(entity).value += AbilitiesSlotPool.Get(entity).value;
                foreach (var entity in EnhancementSlotFilter) targetPool.Get(entity).value += EnhancementSlotPool.Get(entity).value;
                foreach (var entity in MagicGemSlotFilter) targetPool.Get(entity).value += MagicGemSlotPool.Get(entity).value;
                foreach (var entity in TestSlotFilter) targetPool.Get(entity).value += TestSlotPool.Get(entity).value;
                foreach (var entity in WeaponSlotFilter) targetPool.Get(entity).value += WeaponSlotPool.Get(entity).value;
            }
        }
    }
}