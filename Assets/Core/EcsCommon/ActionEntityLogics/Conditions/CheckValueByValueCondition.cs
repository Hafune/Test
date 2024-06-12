using Core.Components;
using Core.Generated;
using Core.Tasks;
using UnityEngine;

namespace Core.Systems
{
    public class CheckValueByValueCondition : AbstractActionEntityCondition
    {
        [SerializeField] private ValueEnum _value;
        [SerializeField] private EqualOperatorEnum _operator = EqualOperatorEnum.GreaterThanOrEqual;
        [SerializeField] private ValueEnum _byValue;
        private ComponentPools _pools;

        private void OnValidate() => name = $"Value {EqualOperator.GetName(_operator)} Value";

        private void Awake() => _pools = Context.Resolve<ComponentPools>();

        public override bool Check(int entity)
        {
            float value = ValuePoolsUtility.GetValue(_pools, entity, _value);
            float targetValue = ValuePoolsUtility.GetValue(_pools, entity, _byValue);
            return _operator switch
            {
                EqualOperatorEnum.Equal when value != targetValue => true,
                EqualOperatorEnum.LessThanOrEqual when value <= targetValue => true,
                EqualOperatorEnum.LessThan when value < targetValue => true,
                EqualOperatorEnum.GreaterThanOrEqual when value >= targetValue => true,
                EqualOperatorEnum.GreaterThan when value > targetValue => true,
                _ => false
            };
        }
    }
}