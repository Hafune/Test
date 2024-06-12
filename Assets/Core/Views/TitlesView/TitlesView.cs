using System;
using System.Collections;
using Core.Services;
using Core.Views.MainMenu;
using LurkingNinja.MyGame.Internationalization;
using UnityEngine;

namespace Core.Views
{
    public class TitlesView : AbstractUIDocumentView
    {
        private TitlesVT _root;
        private Action _callback;
        private UiInputWrapper _input;
        private AddressableService _addressableService;

        protected override void Awake()
        {
            base.Awake();
            _root = new TitlesVT(RootVisualElement);
            new UIElementLocalization(RootVisualElement, I18N.FinalTitles.Table);

            _addressableService = Context.Resolve<AddressableService>();

            _input = Context.Resolve<UiInputsService>().BuildUiInput();
            _input.Actions.Submit.performed += _ =>
            {
                _input.DisableInput();
                _callback?.Invoke();
            };
        }

        public void Show(Action callback)
        {
            _callback = callback;
            DisplayFlex();
            _root.background.AddToClassList(TitlesVT.s_fade_in);
            _root.textContianer.AddToClassList(TitlesVT.s_end_text_position);
            _addressableService.OnSceneLoaded += Hide;
            StartCoroutine(Wait());
        }

        private void Hide()
        {
            DisplayNone();
            _addressableService.OnSceneLoaded -= Hide;
            _root.background.RemoveFromClassList(TitlesVT.s_fade_in);
            _root.textContianer.RemoveFromClassList(TitlesVT.s_end_text_position);
        }

        private IEnumerator Wait()
        {
            yield return new WaitForSeconds(3);
            _root.navigationContainer.AddToClassList(TitlesVT.s_navigation_fade_out);
            _input.EnableInput();
        }
    }
}