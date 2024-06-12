using System;
using BehaviorDesigner.Runtime.Tasks;
using Core.BehaviorTree.Shared;
using Core.Components;
using Core.Generated;
using Core.Systems;
using JetBrains.Annotations;
using Lib;
using UnityEngine;

namespace Core.BehaviorTree
{
    [TaskCategory("Actions"), Serializable]
    public class StartAction : AbstractEntityAction
    {
        [SerializeField] private SharedAbstractActionEntityLogic actionLogic;
        [SerializeField, CanBeNull] private ActionCooldown Cooldown;
        [SerializeField] private bool _restartOnFail;

        private bool _isConditionValid;
        private bool _awaitingResult;
        private ActionSystemsService _actionSystemsService;
        private ComponentPools _pools;

        public override string OnDrawNodeText()
        {
            if (!actionLogic.Value || actionLogic.Value.name == FriendlyName.Replace(" ", ""))
            {
                if (FriendlyName != "Action Not Set")
                    FriendlyName = "Action Not Set";

                return string.Empty;
            }

            var newName = "Start " + actionLogic.Value.name.FormatAddCharBeforeCapitalLetters(true);

            if (FriendlyName != newName)
                FriendlyName = newName;

            return string.Empty;
        }

        public override void OnAwake()
        {
            if (!actionLogic.Value)
            {
                Disabled = true;
                return;
            }

            base.OnAwake();
            _actionSystemsService = Context.Resolve<ActionSystemsService>();
            _pools = Context.Resolve<ComponentPools>();
            IsInstant = false;
        }

        public override void OnStart()
        {
            _isConditionValid = actionLogic.Value.CheckConditionLogic(RawEntity);

            if (!_isConditionValid)
                return;

            _awaitingResult = true;
            SetupStartAction();
            SubscribeOnActionStates(
                ActionEnum.NpcActionComponent,
                _restartOnFail ? SetupStartAction : null);
        }

        public override TaskStatus OnUpdate()
        {
            if (!_isConditionValid)
                return TaskStatus.Failure;

            var result = _awaitingResult ? TaskStatus.Running : TaskStatus.Success;

            if (result == TaskStatus.Success)
                Cooldown?.Restart();

            return result;
        }

        public override void OnEnd() => DescribeOnActionState();

        protected override void OnActionCompleted() => _awaitingResult = false;

        protected override void OnActionStartFailed() => _isConditionValid = true;

        private void SetupStartAction()
        {
            _pools.NpcAction.Get(RawEntity).nextLogic = actionLogic.Value;
            _actionSystemsService.AddStartEvent(ActionEnum.NpcActionComponent, RawEntity);
        }
    }
}