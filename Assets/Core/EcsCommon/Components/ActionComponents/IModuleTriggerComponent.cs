using Core.EntityModules;
using Core.Lib;

namespace Core.Components
{
    public interface IModuleTriggerComponent
    {
        public MyList<AbstractEntityModule> Modules { get; set; }
    }
}