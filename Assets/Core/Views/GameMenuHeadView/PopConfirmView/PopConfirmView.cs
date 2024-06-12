using System;
using Lib;
using LurkingNinja.MyGame.Internationalization;
using Reflex;
using UnityEngine.UIElements;

namespace Core
{
    public class PopConfirmView
    {
        private Action _onSubmit;
        private readonly VisualElement _rootVisualElement;
        private readonly TextElement _messageElement;
        private readonly UiFocusableService _uIFocusableService;
        private readonly PopConfirmVT _root;

        public PopConfirmView(Context context, VisualElement rootVisualElement)
        {
            _uIFocusableService = context.Resolve<UiFocusableService>();
            _rootVisualElement = rootVisualElement;
            new UIElementLocalization(_rootVisualElement, I18N.ItemContextMenuVT.Table);
            _root = new PopConfirmVT(_rootVisualElement);
            _messageElement = _root.message;
            _root.submitButton.RegisterCallback<ClickEvent>(OnSubmit);
            _root.cancelButton.RegisterCallback<ClickEvent>(OnCancel);
            _rootVisualElement.RegisterCallback<NavigationSubmitEvent>(OnSubmit);
            _rootVisualElement.RegisterCallback<NavigationCancelEvent>(OnCancel);

            _rootVisualElement.DisplayNone();
        }

        public void Show(in string message, Action onSubmit)
        {
            _onSubmit = onSubmit;
            _messageElement.text = message;
            _rootVisualElement.DisplayFlex();
            _uIFocusableService.AddLayer(_rootVisualElement);
            _root.popConfirmWrapper.Focus();
        }

        private void OnSubmit(EventBase evt = null)
        {
            OnCancel();
            _onSubmit!();
        }

        private void OnCancel(EventBase evt = null)
        {
            evt?.StopPropagation();
            _rootVisualElement.DisplayNone();
            _uIFocusableService.RemoveLayer();
        }
    }
}