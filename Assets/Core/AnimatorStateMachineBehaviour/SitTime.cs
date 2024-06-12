using UnityEngine;

namespace Core.AnimatorStateMachineBehaviour
{
    public class SitTime : StateMachineBehaviour
    {
        private static readonly int StandBy = Animator.StringToHash("StandBy");
        private static readonly int StandbyState = Animator.StringToHash("StandbyState");
        private int _standByTime;
        private float _standByStateTime;

        public override void OnStateUpdate(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            _standByTime = animator.GetInteger(StandBy);
            _standByStateTime = animator.GetFloat(StandbyState);
            
            if (_standByStateTime == _standByTime)
                return;

            _standByStateTime = Mathf.MoveTowards(_standByStateTime, _standByTime, Time.deltaTime * 10);
            animator.SetFloat(StandbyState, _standByStateTime);
        }
    }
}