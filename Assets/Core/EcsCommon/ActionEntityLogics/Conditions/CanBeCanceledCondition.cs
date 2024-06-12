using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.Systems
{
    public class CanBeCanceledCondition : AbstractActionEntityCondition
    {
        [SerializeField] private ActionEnum[] _cancelableActions;
        private EcsPool<ActionCurrentComponent> _pool;

        private void Awake() => _pool = Context.Resolve<EcsWorld>().GetPool<ActionCurrentComponent>();

        public override bool Check(int entity)
        {
            ref var current = ref _pool.Get(entity);

            if (!current.canBeCanceled)
                return false;

            var actionEnum = current.actionEnum;

            for (int i = 0, iMax = _cancelableActions.Length; i < iMax; i++)
                if (_cancelableActions[i] == actionEnum)
                    return true;

            return false;
        }
    }
}