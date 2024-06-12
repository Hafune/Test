using System;
using Core.Components;
using Core.Generated;
using JetBrains.Annotations;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using Voody.UniLeo.Lite;

namespace Core.BehaviorTree
{
    [Serializable]
    public abstract class AbstractEntityAction : BehaviorDesigner.Runtime.Tasks.Action
    {
        private EcsPool<ActionCurrentComponent> _currentActionPool;
        private EcsPool<EventBehaviorTreeActionStartFailedCheck> _eventActionStartFailedCheckPool;
        protected ConvertToEntity ConvertToEntity { get; private set; }
        protected EcsWorld World { get; private set; }
        protected EcsEngine EcsEngine { get; private set; }
        protected int RawEntity => ConvertToEntity.RawEntity;
        protected Context Context => ConvertToEntity.Context;
        private bool _waitCompletePreviousAction;
        [CanBeNull] private Action _reStart;

        public override void OnAwake()
        {
            ConvertToEntity = GetComponent<ConvertToEntity>();
            World = ConvertToEntity.Context.Resolve<EcsWorld>();
            EcsEngine = ConvertToEntity.Context.Resolve<EcsEngine>();
            _currentActionPool = World.GetPool<ActionCurrentComponent>();
            _eventActionStartFailedCheckPool = World.GetPool<EventBehaviorTreeActionStartFailedCheck>();
        }

        protected void SubscribeOnActionStates(ActionEnum actionType, Action restart = null)
        {
            ref var currentAction = ref _currentActionPool.Get(RawEntity);

            if (currentAction.BTreeDesiredActionType != default)
            {
                OnActionStartFailedPrivate();
                return;
            }

            ReSendStartEvent();

            var currentActionType = _currentActionPool.Get(RawEntity).actionEnum;
            currentAction.BTreeDesiredActionType = actionType;
            currentAction.BTreeOnActionStart += OnActionStartPrivate;
            currentAction.BTreeOnActionStartFailed += OnActionStartFailedPrivate;
            currentAction.BTreeOnActionCompleted += OnActionCompletedPrivate;
            _reStart = restart;

            if (currentActionType == actionType)
                OnActionStart();
        }

        protected void DescribeOnActionState()
        {
            if (RawEntity == -1)
                return;

            ref var currentAction = ref _currentActionPool.Get(RawEntity);

            currentAction.BTreeDesiredActionType = default;
            currentAction.BTreeOnActionStart -= OnActionStartPrivate;
            currentAction.BTreeOnActionStartFailed -= OnActionStartFailedPrivate;
            currentAction.BTreeOnActionCompleted -= OnActionCompletedPrivate;
        }

        protected virtual void OnActionStart()
        {
        }

        protected virtual void OnActionStartFailed()
        {
        }

        protected virtual void OnActionCompleted()
        {
        }

        private void OnActionStartPrivate()
        {
            if (_waitCompletePreviousAction)
                return;
            
            _currentActionPool.Get(RawEntity).BTreeDesiredActionType = default;
            _reStart = null;
            OnActionStart();
        }

        private void OnActionStartFailedPrivate()
        {
            if (_reStart is not null && !_currentActionPool.Get(RawEntity).isCompleted)
            {
                _waitCompletePreviousAction = true;
                return;
            }

            _reStart = null;
            OnActionStartFailed();
        }

        private void OnActionCompletedPrivate()
        {
            if (_waitCompletePreviousAction)
            {
                _waitCompletePreviousAction = false;
                _reStart!.Invoke();
                ReSendStartEvent();
                return;
            }

            _reStart = null;
            OnActionCompleted();
        }

        private void ReSendStartEvent()
        {
            _waitCompletePreviousAction = false;
            _eventActionStartFailedCheckPool.AddIfNotExist(RawEntity);
        }
    }
}