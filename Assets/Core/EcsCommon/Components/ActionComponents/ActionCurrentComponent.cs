using System;
using Core.Generated;
using Core.Systems;
using Leopotam.EcsLite;
using Voody.UniLeo.Lite;

namespace Core.Components
{
    public struct ActionCurrentComponent : IEcsAutoReset<ActionCurrentComponent>, IResetInProvider
    {
        public IAbstractActionSystem currentAction;
        public bool canBeCanceled;
        public bool isCompleted;
        public ActionEnum actionEnum;

        public ActionEnum BTreeDesiredActionType;
        public Action BTreeOnActionStart;
        public Action BTreeOnActionStartFailed;
        public Action BTreeOnActionCompleted;

        public void ChangeAction(IAbstractActionSystem nextAction, int entity)
        {
            currentAction?.Cancel(entity);
            currentAction = nextAction;
            canBeCanceled = false;
            isCompleted = false;
        }

        public void AutoReset(ref ActionCurrentComponent c)
        {
            c.currentAction = null;
            c.isCompleted = true;
            c.canBeCanceled = true;
            c.actionEnum = default;

            c.BTreeDesiredActionType = default;
            c.BTreeOnActionStart = default;
            c.BTreeOnActionStartFailed = default;
            c.BTreeOnActionCompleted = default;
        }
    }
}