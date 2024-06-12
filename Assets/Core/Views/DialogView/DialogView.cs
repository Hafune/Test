using System;
using System.Collections;
using Core.Services;
using Lib;
using LurkingNinja.MyGame.Internationalization;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Views.MainMenu
{
    public class DialogView : AbstractUIDocumentView
    {
        private UiInputWrapper _uiActions;
        private DialogVT _root;
        private DialogService _dialogService;
        private VisualElement _icon;
        private Label _title;
        private Label _message;
        private bool _skip;
        private float _charsPerSecond = 30;

        protected override void Awake()
        {
            base.Awake();
            _root = new DialogVT(RootVisualElement);
            new UIElementLocalization(RootVisualElement, I18N.DialogVT.Table);
            _icon = _root.icon;
            _message = _root.message;
            _title = _root.title;

            _uiActions = Context.Resolve<UiInputsService>().BuildUiInput();
            _uiActions.Actions.Submit.performed += _ => _skip = true;
            _uiActions.Actions.Cancel.performed += _ => _skip = true;
        }

        private void Start()
        {
            _dialogService = Context.Resolve<DialogService>();
            _dialogService.OnShow += Show;
        }

        private void Show()
        {
            DisplayFlex();
            _uiActions.EnableInput();
            RootVisualElement.schedule.Execute(Focus);
            StartCoroutine(ReadMessages(_dialogService.Dialog));
        }

        private void Hide()
        {
            _uiActions.DisableInput();
            DisplayNone();
            _dialogService.DialogComplete();
        }

        private void Focus() => RootVisualElement.FocusFirstFocusable();

        private IEnumerator ReadMessages(DialogData dialog)
        {
            var index = 0;
            var messages = dialog.Messages;

            while (messages.Count > index)
            {
                var message = messages[index++];
                _icon.SetBackgroundImage(message.icon);
                _title.text = message.Title;
                yield return ReadMessage(message.Message);

                yield return null;
            }

            Hide();
        }

        private IEnumerator ReadMessage(string message)
        {
            _skip = false;
            float messageStartTime = Time.time;
            int charsCount = 0;
            int previousCount = 0;

            while (charsCount < message.Length && !_skip)
            {
                yield return null;
                
                charsCount = (int)((Time.time - messageStartTime) * _charsPerSecond);

                if (previousCount == charsCount)
                    continue;
                
                _message.text = message[..Math.Clamp(charsCount, 0, message.Length - 1)];
                previousCount = charsCount;
            }

            _skip = false;
            _message.text = message;

            while (!_skip)
                yield return null;
        }
    }
}
