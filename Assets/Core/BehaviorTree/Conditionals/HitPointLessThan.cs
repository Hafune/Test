using System;
using BehaviorDesigner.Runtime.Tasks;
using Core.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.BehaviorTree
{
    [Serializable, TaskCategory("Actions")]
    public class HitPointLessThan : AbstractEntityCondition
    {
        
        [SerializeField] private float _percent;

        private EcsPool<HitPointValueComponent> _hitPointPool;
        private EcsPool<HitPointMaxValueComponent> _hitPointMaxPool;

        public override void OnAwake()
        {
            base.OnAwake();
            _hitPointPool = GetPool<HitPointValueComponent>();
            _hitPointMaxPool = GetPool<HitPointMaxValueComponent>();
        }

        public override TaskStatus OnUpdate() =>
            _hitPointPool.Get(RawEntity).value / _hitPointMaxPool.Get(RawEntity).value <= _percent
                ? TaskStatus.Success
                : TaskStatus.Failure;
    }
}