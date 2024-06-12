using Core.Views.MainMenu;
using Lib;
using LurkingNinja.MyGame.Internationalization;
using UnityEngine.UIElements;

namespace Core.Views.LoadingView
{
    public class LoadingView : AbstractUIDocumentView
    {
        private LoadingVT _root;

        protected override void Awake()
        {
            base.Awake();
            _root = new LoadingVT(RootVisualElement);
            new UIElementLocalization(RootVisualElement, I18N.LoadingVT.Table);

            DisplayFlex();
            _root.loadingContainer.DisplayNone();
            _root.spinner.DisplayNone();
            _root.save.DisplayNone();

            var addressableService = Context.Resolve<AddressableService>();
            addressableService.OnNextSceneWillBeLoaded += () =>
            {
                _root.loadingContainer.DisplayFlex();
                _root.loadingBar.title = string.Empty;
                _root.loadingBar.value = 0;
            };
            addressableService.OnSceneLoaded += _root.loadingContainer.DisplayNone;
            addressableService.OnLoadingPercentChange += value =>
            {
                _root.loadingBar.value = value;
                // _root.loadingBar.title = ConvertBytesToMegabytes(addressableService.TotalLoadingValue * value) + " MB";
                _root.loadingBar.title = FormatUiValuesUtility.ToPercentInt(value) + "%";
            };

            var playerDataService = Context.Resolve<PlayerDataService>();

            void HideAfterTransition(TransitionEndEvent _)
            {
                _root.save.UnregisterCallback<TransitionEndEvent>(HideAfterTransition);
                _root.save.RemoveFromClassList(LoadingVT.s_savedHide);
                _root.save.DisplayNone();
            }

            playerDataService.OnSaveEnd += () =>
            {
                if (!_root.save.IsDisplayFlex())
                    return;
                
                _root.save.AddToClassList(LoadingVT.s_savedHide);
                _root.save.RegisterCallback<TransitionEndEvent>(HideAfterTransition);
            };
        }

        private static string ConvertBytesToMegabytes(float bytes) => (bytes / 1024f / 1024f).ToString("0.00");
    }
}