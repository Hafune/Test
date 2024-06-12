using Core.Components;
using Core.Generated;
using Core.Systems;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ValueSumByValuePercentLogic : AbstractEntityLogic
    {
        [SerializeField] private ValueEnum _value;
        [SerializeField] private ValueEnum _byValue;
        [SerializeField] private float _percent;
        private ComponentPools _pool;

        private void Awake() => _pool = Context.Resolve<ComponentPools>();

        public override void Run(int entity)
        {
            var value = ValuePoolsUtility.GetValue(_pool, entity, _value);
            var byValue = ValuePoolsUtility.GetValue(_pool, entity, _byValue);
            ValuePoolsUtility.SetValue(_pool, entity, _value, value + byValue * _percent);
        }
    }
}