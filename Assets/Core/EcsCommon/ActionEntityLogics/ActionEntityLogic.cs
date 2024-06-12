using System;
using Lib;
using UnityEngine;

namespace Core.Systems
{
    public class ActionEntityLogic : AbstractActionEntityLogic, IActionEntityLogic
    {
        public event Action OnStart;

        [SerializeField] public AbstractActionEntityCondition _condition;
        [SerializeField] public AbstractEntityLogic _start;
        [SerializeField] public AbstractEntityLogic _update;
        [SerializeField] public AbstractEntityLogic _completeStreaming;
        [SerializeField] public AbstractEntityLogic _cancel;
        [SerializeField] public AbstractEntityResettableLogic[] _resetOnStart;

        private void OnValidate()
        {
            _condition = _condition ? _condition : transform.GetSelfChildrenComponent<AbstractActionEntityCondition>();
            _start = _start ? _start : transform.GetSelfChildrenComponent<AbstractEntityLogic>();
        }

        public override bool CheckConditionLogic(int entity) => _condition?.Check(entity) ?? true;

        public override void ResetOnStart(int entity)
        {
            for (int i = 0, iMax = _resetOnStart.Length; i < iMax; i++)
                _resetOnStart[i].ResetLogic(entity);   
        }
        
        public override void StartLogic(int entity)
        {
            _start?.Run(entity);
            OnStart?.Invoke();
        }

        public void UpdateLogic(int entity) => _update?.Run(entity);

        public override void CompleteStreamingLogic(int entity) => _completeStreaming?.Run(entity);

        public override void CancelLogic(int entity) => _cancel?.Run(entity);
    }
}