using Core.Components;
using Core.Generated;
using Core.Tasks;
using UnityEngine;

namespace Core.Systems
{
    public class CheckValueCondition : AbstractActionEntityCondition
    {
        [SerializeField] private ValueEnum _value;
        [SerializeField] private EqualOperatorEnum _operator = EqualOperatorEnum.GreaterThanOrEqual;
        [SerializeField] private float _targetValue;
        private ComponentPools _pools;

        private void OnValidate() => name = $"Value {EqualOperator.GetName(_operator)} {_targetValue}";

        private void Awake() => _pools = Context.Resolve<ComponentPools>();

        public override bool Check(int entity)
        {
            float value = ValuePoolsUtility.GetValue(_pools, entity, _value);
            return _operator switch
            {
                EqualOperatorEnum.Equal when value != _targetValue => true,
                EqualOperatorEnum.LessThanOrEqual when value <= _targetValue => true,
                EqualOperatorEnum.LessThan when value < _targetValue => true,
                EqualOperatorEnum.GreaterThanOrEqual when value >= _targetValue => true,
                EqualOperatorEnum.GreaterThan when value > _targetValue => true,
                _ => false
            };
        }
    }
}