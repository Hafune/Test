using UnityEngine;

namespace Core.AnimatorStateMachineBehaviour
{
    public class ControlTargetByTime : AbstractTargets
    {
        [SerializeField] private float _enableTime;
        [SerializeField] private float _disableTime;
        private float _currentTime;
        private bool _enabled;
        private bool _disabled;
        private bool _initialized;
        private bool _ignoreTransition;

        public override void OnStateEnter(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            _currentTime = 0;
            _enabled = false;
            _disabled = false;

            Initialize(animator, layerIndex);
        }

        public override void OnStateUpdate(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            _currentTime += Time.deltaTime * stateInfo.speed;
            
            if (!_enabled && _currentTime >= _enableTime)
            {
                _enabled = true;

                for (int i = 0, iMax = Targets.Length; i < iMax; i++)
                    Targets[i].SetActive(true);
            }
            
            if (_ignoreTransition)
                _ignoreTransition = _ignoreTransition && animator.IsInTransition(layerIndex);

            if (!_disabled && !_ignoreTransition && (_currentTime >= _disableTime || animator.IsInTransition(layerIndex)))
            {
                _disabled = true;

                for (int i = 0, iMax = Targets.Length; i < iMax; i++)
                    Targets[i].SetActive(false);
            }
        }
    }
}