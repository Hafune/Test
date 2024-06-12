using Core.EntityModules;
using Core.Lib;
using Leopotam.EcsLite;

namespace Core.Components
{
    public struct ShotTriggerComponent : IEcsAutoReset<ShotTriggerComponent>, IModuleTriggerComponent
    {
        public MyList<AbstractEntityModule> Modules { get; set; }

        public void AutoReset(ref ShotTriggerComponent c)
        {
            c.Modules ??= new();
            c.Modules.Clear();
        }
    }
}