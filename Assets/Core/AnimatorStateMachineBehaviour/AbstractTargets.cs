using System;
using System.Collections.Generic;
using System.Linq;
using Core.Lib;
using UnityEngine;

namespace Core.AnimatorStateMachineBehaviour
{
    public abstract class AbstractTargets : StateMachineBehaviour
    {
        [SerializeField] private ReferencePath[] _pathRefs;
        protected GameObject[] Targets { get; private set; } = Array.Empty<GameObject>();
        protected bool IsInitialized { get; private set; }
        //EnterCount нужен т.к. OnStateExit может произойти после повторного старта анимации
        protected int EnterCount { get; private set; }

        private void OnValidate()
        {
            HashSet<ReferencePath> set = new();
            _pathRefs = _pathRefs?.Select(i => set.Add(i) ? i : null).ToArray();
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) =>
            EnterCount++;

        public override void OnStateExit(
            Animator animator,
            AnimatorStateInfo stateInfo,
            int layerIndex)
        {
            EnterCount--;
#if UNITY_EDITOR
            if (EnterCount < 0)
                throw new Exception("Отрицательное значение входов в AbstractTargets");
#endif
            if (EnterCount > 0)
                return;

            for (int i = 0, iMax = Targets.Length; i < iMax; i++)
                Targets[i].SetActive(false);
        }

        protected void Initialize(Animator animator, int layerIndex)
        {
            if (IsInitialized)
                return;

            animator.gameObject.AddComponent<EnableDispatcher>().OnDisabled += () =>
            {
                for (int i = 0, iMax = Targets.Length; i < iMax; i++)
                    Targets[i].SetActive(false);
            };

            Targets = new GameObject[_pathRefs.Length];
#if UNITY_EDITOR
            try
            {
#endif
                for (int i = 0, iMax = _pathRefs.Length; i < iMax; i++)
                    Targets[i] = _pathRefs[i].Find(animator.transform)?.gameObject;
#if UNITY_EDITOR
            }
            catch (Exception e)
            {
                Debug.LogError(@$"Animation clip: ${animator.GetCurrentAnimatorClipInfo(layerIndex)[0].clip.name}");
                Debug.LogError(e.Message);
                throw;
            }
#endif

            IsInitialized = true;
        }
    }
}