using Core.BehaviorTree;
using Lib;

namespace Core.Systems
{
    public class TriggerIsActiveCondition : AbstractActionEntityCondition
    {
        private TriggerCounter2D _triggerCounter2D;

        private void Awake() => _triggerCounter2D = transform.GetSelfChildrenComponent<TriggerCounter2D>();

        public override bool Check(int entity) => _triggerCounter2D.TriggerIsActive();
    }
}