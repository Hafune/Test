using BehaviorDesigner.Runtime.Tasks;
using Core.BehaviorTree.Scripts;
using Core.BehaviorTree.Shared;
using UnityEngine;

namespace Core.BehaviorTree
{
    [TaskCategory("Actions")]
    [TaskDescription("Задержка после выполнения экшена, получается из вне")]
    [TaskIcon("{SkinColor}WaitIcon.png")]
    public class CommonActionWait : Action
    {
        [SerializeField] private SharedBTreeNpcAction _action;
        private BTreeCommonNpcAction _commonAction;

        private float _waitDuration;
        private float _startTime;

        public override void OnAwake()
        {
            _commonAction = (BTreeCommonNpcAction)_action.Value;
            
            if (_commonAction is null)
                Disabled = true;
        }

        public override void OnStart()
        {
            _startTime = Time.time;
            _waitDuration = _commonAction.WaitTime;
        }

        public override TaskStatus OnUpdate() =>
            _startTime + _waitDuration < Time.time ? TaskStatus.Success : TaskStatus.Running;
    }
}