using Core.Components;
using Core.Generated;
using Core.Systems;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ValueSumByValueLogic : AbstractEntityLogic
    {
        [SerializeField] private ValueEnum _value;
        [SerializeField] private ValueEnum _byValue;
        private ComponentPools _pool;

        private void Awake() => _pool = Context.Resolve<ComponentPools>();

        public override void Run(int entity) => ValuePoolsUtility.Sum(_pool, entity, _value, _byValue);
    }
}