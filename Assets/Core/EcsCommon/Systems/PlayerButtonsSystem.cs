using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Reflex;
using UnityEngine.InputSystem;

namespace Core.Systems
{
    public class PlayerButtonsSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                PlayerControllerTag
            >,
            Exc<
                InProgressTag<PlayerControllerTag>
            >> _activateFilter;

        private readonly EcsFilterInject<
            Inc<
                InProgressTag<PlayerControllerTag>
            >,
            Exc<
                PlayerControllerTag
            >> _deactivateFilter;

        private readonly EcsPoolInject<InProgressTag<PlayerControllerTag>> _progressPool;
        private readonly IButtonHandler[] _buttons;

        public PlayerButtonsSystem(Context context)
        {
            var world = context.Resolve<EcsWorld>();
            var playerActions = context.Resolve<PlayerInputs.PlayerActions>();

            _buttons = new IButtonHandler[]
            {
                new ButtonHandler<ButtonUseHealing>(world, playerActions.UseHealing),
                new ButtonHandler<ButtonRight>(world, playerActions.Right),
                new ButtonHandler<ButtonLeft>(world, playerActions.Left),
                new ButtonHandler<ButtonUp>(world, playerActions.Up),
                new ButtonHandler<ButtonDown>(world, playerActions.Down),
                new ButtonHandler<ButtonLightAttackTag>(world, playerActions.LightAttack),
                new ButtonHandler<ButtonStrongAttackTag>(world, playerActions.StrongAttack),
                new ButtonHandler<ButtonJumpTag>(world, playerActions.Jump),
                new ButtonHandler<ButtonDashTag>(world, playerActions.Dash),
                new ButtonHandler<LStickTag>(world, playerActions.LeftStick),
                new ButtonHandler<ButtonHorizontalTag>(world, playerActions.HorizontalPriority),
            };
        }

        public void Run(IEcsSystems systems)
        {
            for (int a = 0, iMax = _buttons.Length; a < iMax; a++)
                _buttons[a].Update();

            foreach (var i in _activateFilter.Value)
            {
                _progressPool.Value.Add(i);

                for (int a = 0, iMax = _buttons.Length; a < iMax; a++)
                    _buttons[a].ActivateInput(i);
            }

            foreach (var i in _deactivateFilter.Value)
            {
                _progressPool.Value.Del(i);

                for (int a = 0, iMax = _buttons.Length; a < iMax; a++)
                    _buttons[a].DeactivateInput(i);
            }
        }

        private interface IButtonHandler
        {
            void Update();
            void ActivateInput(int i);
            void DeactivateInput(int i);
        }

        private class ButtonHandler<T> : IButtonHandler where T : struct, IButtonComponent
        {
            private readonly EcsFilter eventPerformedFilter;
            private readonly EcsFilter eventCanceledFilter;

            private readonly EcsPool<EventButtonPerformed<T>> eventPerformedPool;
            private readonly EcsPool<EventButtonCanceled<T>> eventCanceledPool;
            private readonly EcsPool<T> streamingPool;
            private readonly InputAction _inputAction;

            internal ButtonHandler(EcsWorld world, InputAction inputAction)
            {
                _inputAction = inputAction;

                var controllerFilter = world.Filter<PlayerControllerTag>().End();
                eventPerformedFilter = world.Filter<EventButtonPerformed<T>>().End();
                eventCanceledFilter = world.Filter<EventButtonCanceled<T>>().End();

                eventPerformedPool = world.GetPool<EventButtonPerformed<T>>();
                eventCanceledPool = world.GetPool<EventButtonCanceled<T>>();
                streamingPool = world.GetPool<T>();

                inputAction.performed += _ =>
                {
                    foreach (var i in controllerFilter)
                    {
                        eventCanceledPool.DelIfExist(i);
                        eventPerformedPool.AddIfNotExist(i);
                        streamingPool.AddIfNotExist(i);
                    }
                };

                inputAction.canceled += _ =>
                {
                    foreach (var i in controllerFilter)
                    {
                        eventCanceledPool.AddIfNotExist(i);
                        eventPerformedPool.DelIfExist(i);
                        streamingPool.DelIfExist(i);
                    }
                };
            }

            public void Update()
            {
                foreach (var i in eventPerformedFilter)
                    eventPerformedPool.Del(i);

                foreach (var i in eventCanceledFilter)
                    eventCanceledPool.Del(i);
            }

            public void ActivateInput(int i)
            {
                if (_inputAction.WasPerformedThisFrame())
                {
                    eventCanceledPool.DelIfExist(i);
                    eventPerformedPool.AddIfNotExist(i);
                }

                if (_inputAction.IsPressed())
                    streamingPool.AddIfNotExist(i);
            }

            public void DeactivateInput(int i)
            {
                if (!streamingPool.Has(i))
                    return;

                eventCanceledPool.AddIfNotExist(i);
                eventPerformedPool.DelIfExist(i);
                streamingPool.Del(i);
            }
        }
    }
}