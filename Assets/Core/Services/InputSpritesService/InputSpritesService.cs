using System;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

namespace Core.Services.SpriteForInputService
{
    public class InputSpritesService : MonoBehaviour
    {
        private enum InputTypes
        {
            Gamepad,
            Keyboard
        }

        public enum InputKeys
        {
            Submit,
            Cancel,
            ContextMenu,
            Up,
            Down,
            Right,
            Left,
            Start,
            Select,
            RightTrigger,
            LeftTrigger,
            LightAttack,
            StrongAttack,
            Jump,
            Dash
        }

        public event Action OnChange;

        [SerializeField] private PanelSettings _panelSettings;
        [SerializeField] private SpriteAsset _textAssetGamepad;
        [SerializeField] private SpriteAsset _textAssetKeyboard;
        [SerializeField] private TMP_SpriteAsset _gamepadButtons;
        [SerializeField] private TMP_SpriteAsset _keyboardButtons;
        [SerializeField] private InputSpritesData _gamepadSprites;
        [SerializeField] private InputSpritesData _keyboardSprites;
        private PlayerInputs.GamepadKeyboardReactionActions _gamepadKeyboardReaction;
        private InputTypes _inputType = InputTypes.Gamepad;

        private void Awake()
        {
            _gamepadKeyboardReaction = new PlayerInputs().GamepadKeyboardReaction;
            _gamepadKeyboardReaction.Enable();
            _panelSettings.textSettings.defaultSpriteAsset = _textAssetGamepad;

            _gamepadKeyboardReaction.AnyGamepadKey.performed += _ =>
            {
                if (_inputType == InputTypes.Gamepad)
                    return;

                _inputType = InputTypes.Gamepad;
                _panelSettings.textSettings.defaultSpriteAsset = _textAssetGamepad;
                OnChange?.Invoke();
            };

            _gamepadKeyboardReaction.AnyKeyboardKey.performed += _ =>
            {
                if (_inputType == InputTypes.Keyboard)
                    return;

                _inputType = InputTypes.Keyboard;
                _panelSettings.textSettings.defaultSpriteAsset = _textAssetKeyboard;
                OnChange?.Invoke();
            };
        }

        public TMP_SpriteAsset GetSpriteAsset()
        {
            return _inputType switch
            {
                InputTypes.Gamepad => _gamepadButtons,
                InputTypes.Keyboard => _keyboardButtons,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public Sprite GetSprite(InputKeys key)
        {
            return _inputType switch
            {
                InputTypes.Gamepad => _gamepadSprites.GetSprite(key),
                InputTypes.Keyboard => _keyboardSprites.GetSprite(key),
                _ => null
            };
        }
    }
}