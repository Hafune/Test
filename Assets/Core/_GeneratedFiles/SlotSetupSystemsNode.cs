//Файл генерируется в GenSlotSetupSystemsNode
using System.Collections.Generic;
using Core.Components;
using Leopotam.EcsLite;
using Reflex;

namespace Core.Systems
{
    public class SlotSetupSystemsNode
    {
        public IEnumerable<IEcsSystem> BuildSystems(Context _context)
        {
            var world = _context.Resolve<EcsWorld>();
            var systems = new List<IEcsSystem>(16);

            void A<T>() where T : struct, ISlotData => systems.Add(new SlotSystem<T>(world));

            A<AbilitiesSlotComponent>();
            A<EnhancementSlotComponent>();
            A<MagicGemSlotComponent>();
            A<TestSlotComponent>();
            A<WeaponSlotComponent>();

            return systems;
        }
    }
}
