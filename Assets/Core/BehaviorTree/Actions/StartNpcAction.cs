using BehaviorDesigner.Runtime.Tasks;
using Core.BehaviorTree.Shared;
using Core.Generated;
using Core.Systems;
using JetBrains.Annotations;
using Lib;
using UnityEngine;

namespace Core.BehaviorTree
{
    [TaskCategory("Actions")]
    public class StartNpcAction : AbstractEntityAction
    {
        [SerializeField] protected SharedBTreeNpcAction Action;
        [SerializeField, CanBeNull] private ActionCooldown Cooldown;
        [SerializeField] private bool _restartOnFail;

        private bool _failed;
        private bool _awaitingResult;
        private ActionSystemsService _actionSystemsService;
        private ActionEnum _actionEnum;

        public override string OnDrawNodeText()
        {
            if (Action!.Name is null || Action.Name == FriendlyName.Replace(" ", ""))
            {
                if (FriendlyName != "Npc Action Not Set")
                    FriendlyName = "Npc Action Not Set";

                return string.Empty;
            }

            var newName = "Start " + Action.Name.FormatAddCharBeforeCapitalLetters(true);

            if (FriendlyName != newName)
                FriendlyName = newName;

            return string.Empty;
        }

        public override void OnAwake()
        {
            if (Action.Value is null)
            {
                Disabled = true;
                return;
            }

            base.OnAwake();
            _actionSystemsService = Context.Resolve<ActionSystemsService>();
            IsInstant = false;
        }

        public override void OnStart()
        {
            _failed = !Action.Value.TriggerIsActive();

            if (_failed)
                return;

            _actionEnum = Action.Value!.ActionEnum;
            _awaitingResult = true;
            SetupStartAction();
            SubscribeOnActionStates(_actionEnum, _restartOnFail ? SetupStartAction : null);
        }

        public override TaskStatus OnUpdate()
        {
            if (_failed)
                return TaskStatus.Failure;

            var result = _awaitingResult ? TaskStatus.Running : TaskStatus.Success;

            if (result == TaskStatus.Success)
                Cooldown?.Restart();

            return result;
        }

        public override void OnEnd() => DescribeOnActionState();

        protected override void OnActionCompleted() => _awaitingResult = false;

        protected override void OnActionStartFailed() => _failed = true;

        private void SetupStartAction() => _actionSystemsService.AddStartEvent(_actionEnum, RawEntity);
    }
}