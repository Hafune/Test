using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Core.Systems;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Tasks
{
    public class TaskTutorialAwaitPlayerCombo : MonoBehaviour, IMyTask
    {
        public event Action<int, int> OnComboStepChange;

        [SerializeField] private ReferencePath[] _comboPaths;
        private Transform _playerTransform;
        private int _comboStep;
        private List<IActionEntityLogic> _comboLogics = new();
        private List<IActionEntityLogic> _otherLogics = new();
        private List<Action> _successActions = new();

        public bool InProgress { get; private set; }

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            var world = context.Resolve<EcsWorld>();
            var filter = world.Filter<PlayerUniqueTag>().Inc<TransformComponent>().End();
            _playerTransform = world.GetPool<TransformComponent>().Get(filter.GetFirst()).transform;
            _successActions.Clear();
            _comboLogics.Clear();
            _otherLogics.Clear();

            _comboLogics.AddRange(_comboPaths.Select(p =>
                p.Find(_playerTransform)!.GetComponent<IActionEntityLogic>()));

            for (int i = 0, iMax = _comboLogics.Count; i < iMax; i++)
            {
                var index = i + 1;
                void ChainAction() => _comboStep = index;
                _successActions.Add(ChainAction);
                _comboLogics[i].OnStart += ChainAction;
            }

            _otherLogics.AddRange(_playerTransform
                .GetComponentsInChildren<IActionEntityLogic>()
                .Where(a => !_comboLogics.Contains(a)));

            foreach (var logic in _otherLogics)
                logic.OnStart += Fail;

            StartCoroutine(Watch(onComplete));
        }

        private void OnDisable()
        {
            for (int i = 0, iMax = _comboLogics.Count; i < iMax; i++)
                _comboLogics[i].OnStart -= _successActions[i];

            foreach (var logic in _otherLogics)
                logic.OnStart -= Fail;
        }

        private IEnumerator Watch(Action<IMyTask> onComplete)
        {
            InProgress = true;
            _comboStep = 0;
            int lastComboStep = _comboStep;
            int totalSteps = _comboLogics.Count;
            
            while (lastComboStep < totalSteps)
            {
                if (lastComboStep + 1 == _comboStep)
                {
                    lastComboStep = _comboStep;
                    OnComboStepChange?.Invoke(_comboStep, totalSteps);
                }
                else if (lastComboStep != _comboStep)
                {
                    _comboStep = 0;
                    lastComboStep = _comboStep;
                    OnComboStepChange?.Invoke(_comboStep, totalSteps);
                }

                yield return null;
            }

            OnDisable();

            InProgress = false;

            onComplete?.Invoke(this);
        }

        private void Fail() => _comboStep = 0;
    }
}