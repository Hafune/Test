using BehaviorDesigner.Runtime.Tasks;
using Core.BehaviorTree.Scripts;
using Core.BehaviorTree.Shared;
using UnityEngine;

namespace Core.BehaviorTree
{
    [TaskCategory("Actions")]
    public class CommonActionCooldown : ActionCooldown
    {
        [SerializeField] private SharedBTreeNpcAction _action;
        
        public override void OnAwake()
        {
            var commonAction = (BTreeCommonNpcAction)_action.Value;
            
            if (commonAction is null)
            {
                Disabled = true;
                return;
            }
            
            _startDelay = commonAction.StartDelay;
            _cooldown = commonAction.Cooldown;
            base.OnAwake();
        }
    }
}