using System;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Systems
{
    public class PlayerControllerSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                ActionMoveComponent,
                MoveDirectionComponent,
                PlayerControllerTag
            >> _moveStreamingFilter;

        private readonly EcsFilterInject<
            Inc<
                InProgressTag<ActionMoveComponent>,
                MoveDirectionComponent,
                PlayerControllerTag
            >> _moveCompleteFilter;

        private readonly EcsFilterInject<
            Inc<
                PlayerInputMemoryComponent,
                ActionCurrentComponent,
                InProgressTag<PlayerInputMemoryComponent>
            >> _inputMemoryFilter;

        private readonly Joystick _joystick;
        private const float InputMemoryTime = .3f;

        private readonly ComponentPools _pools;

        public PlayerControllerSystem(Context context) =>
            _joystick = context.Resolve<Joystick>();

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _moveStreamingFilter.Value)
            {
                ref var move = ref _pools.MoveDirection.Get(i);
                move.direction = _joystick.Direction;

                if (!_pools.InProgressActionMove.Has(i) && move.direction != Vector2.zero)
                    _pools.EventStartActionMove.Add(i);
            }

            foreach (var i in _moveCompleteFilter.Value)
            {
                if (_joystick.Direction != Vector2.zero)
                    continue;

                _pools.MoveDirection.Get(i).direction = Vector2.zero;
                _pools.EventActionComplete.AddIfNotExist(i);
            }

            foreach (var i in _inputMemoryFilter.Value)
            {
                ref var memory = ref _pools.PlayerInputMemory.Get(i);

                if (memory.inputTime < Time.unscaledTime)
                {
                    _pools.InProgressPlayerInputMemory.Del(i);
                    continue;
                }

                var actionCurrent = _pools.ActionCurrent.Get(i);

                if (actionCurrent.currentAction != memory.lastActionSystem)
                {
                    _pools.InProgressPlayerInputMemory.Del(i);
                    continue;
                }

                if (!actionCurrent.canBeCanceled && !actionCurrent.isCompleted)
                    continue;

                memory.AddEventStartAction.Invoke(i);
                _pools.InProgressPlayerInputMemory.Del(i);
            }
        }

        private void AddIfReadyElseRemember(int i, Action<int> startAction)
        {
            if (_pools.ActionCurrent.Get(i).canBeCanceled)
                startAction(i);
            else
                WriteMemory(i, startAction);
        }

        private void WriteMemory(int entity, Action<int> addActionEvent)
        {
            var actionCurrent = _pools.ActionCurrent.Get(entity);
            ref var memory = ref _pools.PlayerInputMemory.Get(entity);
            memory.AddEventStartAction = addActionEvent;
            memory.inputTime = Time.unscaledTime + InputMemoryTime;
            memory.lastActionSystem = actionCurrent.currentAction;
            _pools.InProgressPlayerInputMemory.AddIfNotExist(entity);
        }
    }
}