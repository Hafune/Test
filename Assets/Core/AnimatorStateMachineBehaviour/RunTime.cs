using UnityEngine;

namespace Core.AnimatorStateMachineBehaviour
{
    public class RunTime : StateMachineBehaviour
    {
        private static readonly int InRunLastTime = Animator.StringToHash("InRunLastTime");
        private float _time;

        public override void OnStateEnter(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            animator.SetFloat(InRunLastTime, 0);
            _time = 0;
        }

        public override void OnStateUpdate(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex) => animator.SetFloat(InRunLastTime, _time += Time.deltaTime);

        public override void OnStateExit(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex) => animator.SetFloat(InRunLastTime, 0);
    }
}