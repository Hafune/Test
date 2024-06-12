using System;
using BehaviorDesigner.Runtime.Tasks;
using Core.Components;
using Core.Generated;
using Core.Systems;
using Core.Tasks;
using UnityEngine;

namespace Core.BehaviorTree
{
    [Serializable, TaskCategory("Actions")]
    public class ValueLevel : AbstractEntityCondition
    {
        [SerializeField] private ValueEnum _value;
        [SerializeField] private ValueEnum _valueMax;
        [SerializeField] private EqualOperatorEnum _operator;
        [SerializeField] private float _percent;
        private ComponentPools _pools;

#if UNITY_EDITOR

        public override string OnDrawNodeText()
        {
            if (Application.isPlaying)
                return String.Empty;

            var valueName = Enum.GetName(typeof(ValueEnum), _value)?.Replace("ValueComponent", "");

            if (valueName is null)
                return String.Empty;

            var newName = $"{valueName} {EqualOperator.GetName(_operator)} {FormatUiValuesUtility.ToPercentInt(_percent)}%";

            if (FriendlyName == newName)
                return string.Empty;

            FriendlyName = newName;
            return string.Empty;
        }
#endif

        public override void OnAwake()
        {
            base.OnAwake();
            _pools = Context.Resolve<ComponentPools>();
        }

        public override TaskStatus OnUpdate()
        {
            var currentPercent = ValuePoolsUtility.GetValue(_pools, RawEntity, _value) /
                                 ValuePoolsUtility.GetValue(_pools, RawEntity, _valueMax);
            return _operator switch
            {
                EqualOperatorEnum.Equal when currentPercent != _percent => TaskStatus.Success,
                EqualOperatorEnum.LessThanOrEqual when currentPercent <= _percent => TaskStatus.Success,
                EqualOperatorEnum.LessThan when currentPercent < _percent => TaskStatus.Success,
                EqualOperatorEnum.GreaterThanOrEqual when currentPercent >= _percent => TaskStatus.Success,
                EqualOperatorEnum.GreaterThan when currentPercent > _percent => TaskStatus.Success,
                _ => TaskStatus.Failure
            };
        }
    }
}