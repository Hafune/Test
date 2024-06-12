using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.Systems
{
    public class ActionInListCondition : AbstractActionEntityCondition
    {
        [SerializeField] private ActionEnum[] cancelableActions;

        private EcsPool<ActionCurrentComponent> _pool;
        private void Awake() => _pool = Context.Resolve<ComponentPools>().ActionCurrent;

        public override bool Check(int entity) => IsCurrentActionInCancelableList(entity);

        private bool IsCurrentActionInCancelableList(int entity)
        {
            var actionEnum = _pool.Get(entity).actionEnum;

            for (int i = 0, iMax = cancelableActions.Length; i < iMax; i++)
                if (cancelableActions[i] == actionEnum)
                    return true;

            return false;
        }
    }
}