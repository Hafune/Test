using UnityEngine;

namespace Core.AnimatorStateMachineBehaviour
{
    public class FloatTime : StateMachineBehaviour
    {
        private static readonly int FloatLastTime = Animator.StringToHash("FloatLastTime");
        private float _time;

        public override void OnStateEnter(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            animator.SetFloat(FloatLastTime, 0);
            _time = 0;
        }

        public override void OnStateUpdate(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex) => animator.SetFloat(FloatLastTime, _time += Time.deltaTime);

        public override void OnStateExit(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex) => animator.SetFloat(FloatLastTime, 0);
    }
}