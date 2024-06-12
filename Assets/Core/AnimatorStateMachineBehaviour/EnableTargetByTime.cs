using System;
using UnityEngine;

namespace Core.AnimatorStateMachineBehaviour
{
    [Serializable]
    public class EnableTargetByTime : AbstractTargets
    {
        [SerializeField] private float _enableTime;
        private float _currentTime;
        private bool _enabled;
        private bool _disabled;
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
            _ignoreTransition = animator.IsInTransition(layerIndex);
            Initialize(animator, layerIndex);
        }

        public override void OnStateUpdate(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            if (_ignoreTransition)
                _ignoreTransition = _ignoreTransition && animator.IsInTransition(layerIndex);

            if (_enabled && !_ignoreTransition && !_disabled && animator.IsInTransition(layerIndex))
            {
                _disabled = true;

                for (int i = 0, iMax = Targets.Length; i < iMax; i++)
                    Targets[i].SetActive(false);
            }

            if (_enabled)
                return;

            if ((_currentTime += Time.deltaTime * stateInfo.speed) < _enableTime)
                return;

            _enabled = true;

            for (int i = 0, iMax = Targets.Length; i < iMax; i++)
                Targets[i].SetActive(true);
        }
    }
}