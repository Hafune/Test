using Core.Generated;
using Core.Systems;

namespace Core.Components
{
    public interface IActionComponent
    {
        public ActionEnum actionEnum { get; }

        public AbstractActionEntityLogic logic { get; set; }
    }
}