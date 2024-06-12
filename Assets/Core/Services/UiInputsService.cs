using System.Collections.Generic;
using Reflex;
using UnityEngine.EventSystems;

namespace Core.Services
{
    public class UiInputsService : IInitializableService
    {
        private EventSystem _eventSystem;
        private readonly List<PlayerInputs.UIActions> _uiActions = new();
        private readonly List<bool> _stateBeforeDisable = new();
        private bool _isInputsEnabled = true;

        public void InitializeService(Context context)
        {
            _eventSystem = context.Resolve<EventSystem>();
        }

        public UiInputWrapper BuildUiInput()
        {
            var input = new PlayerInputs().UI;
            _uiActions.Add(input);
            _stateBeforeDisable.Add(false);
            return new UiInputWrapper(input, this);
        }

        public void EnableInput(PlayerInputs.UIActions inputs) => SetEnable(inputs, true);
        public void DisableInput(PlayerInputs.UIActions inputs) => SetEnable(inputs, false);

        private void SetEnable(PlayerInputs.UIActions inputs, bool state)
        {
            var index = _uiActions.FindIndex(i => i.Map == inputs.Map);

            if (!_isInputsEnabled)
                _stateBeforeDisable[index] = state;
            else if (state)
                _uiActions[index].Enable();
            else
                _uiActions[index].Disable();
        }

        public void StopAll()
        {
            for (int i = 0, iMax = _uiActions.Count; i < iMax; i++)
            {
                _stateBeforeDisable[i] = _uiActions[i].enabled;
                _uiActions[i].Disable();
            }

            _eventSystem.enabled = false;
            _isInputsEnabled = false;
        }

        public void RestoreAll()
        {
            for (int i = 0, iMax = _uiActions.Count; i < iMax; i++)
                if (_stateBeforeDisable[i])
                    _uiActions[i].Enable();
                else
                    _uiActions[i].Disable();

            _eventSystem.enabled = true;
            _isInputsEnabled = true;
        }
    }
    
    public class UiInputWrapper
    {
        public PlayerInputs.UIActions Actions { get; }

        private readonly UiInputsService _uiInputsService;

        public UiInputWrapper(PlayerInputs.UIActions actions, UiInputsService uiInputsService)
        {
            Actions = actions;
            _uiInputsService = uiInputsService;
        }

        public void EnableInput() => _uiInputsService.EnableInput(Actions);
        public void DisableInput() => _uiInputsService.DisableInput(Actions);
    }
}
