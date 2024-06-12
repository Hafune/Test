using BehaviorDesigner.Runtime.Tasks;
using Core.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Core.BehaviorTree
{
    [TaskCategory("Actions")]
    public class ChangeMovementSpeed : AbstractEntityAction
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _byTime;
        private float _currentTime;
        private float _startSpeed;
        private EcsPool<MoveSpeedValueComponent> _movementSpeedValue;

        public override void OnAwake()
        {
            base.OnAwake();
            _movementSpeedValue = World.GetPool<MoveSpeedValueComponent>();
        }

        public override void OnStart()
        {
            _startSpeed = _movementSpeedValue.Get(RawEntity).value;
            _currentTime = 0;
        }

        public override TaskStatus OnUpdate()
        {
            _currentTime += Time.deltaTime / _byTime;
            ref var speed = ref _movementSpeedValue.Get(RawEntity);
            speed.value = Mathf.Lerp(_startSpeed, _speed, _currentTime);
            
            return speed.value == _speed ? TaskStatus.Success : TaskStatus.Running;
        }
    }
}