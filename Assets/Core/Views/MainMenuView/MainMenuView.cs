using System.Collections;
using Core.Services;
using Core.Views.MainMenu;
using Lib;
using LurkingNinja.MyGame.Internationalization;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core
{
    public class MainMenuView : AbstractUIDocumentView, IAbstractGlobalState
    {
        [SerializeField] private SceneField _newGameScene;

        private MainMenuVT _root;
        private UiFocusableService _uiFocusableService;
        private GlobalStateService _globalStateService;
        private Coroutine _coroutine;
        private SceneCheckpointsService _sceneCheckpointsService;

        protected override void Awake()
        {
            base.Awake();
            _root = new MainMenuVT(RootVisualElement);

            new UIElementLocalization(_root.mainMenuContainer, I18N.MainMenuVT.Table);
            new UIElementLocalization(_root.navigationBar, I18N.MainMenuVT.Table);
            new UIElementLocalization(_root.settingsVT, I18N.SettingsVT.Table);
            new SettingsView(Context, _root.settingsVT);

            _uiFocusableService = Context.Resolve<UiFocusableService>();
            _sceneCheckpointsService = Context.Resolve<SceneCheckpointsService>();
            var addressableService = Context.Resolve<AddressableService>();
            var playerStateService = Context.Resolve<PlayerStateService>();
            var playerDataService = Context.Resolve<PlayerDataService>();
            var audioSourceService = Context.Resolve<AudioSourceService>();
            const float fadeOutTime = 3f;

            void Run()
            {
                _root.mainMenuContainer.DisplayNone();
                audioSourceService.FadeBackground(fadeOutTime);

                if (_sceneCheckpointsService.LoadLastCheckpointIfExist())
                    return;

                playerStateService.sceneStartPosition = ScenePositionsEnum.StartPosition;
                addressableService.LoadSceneAsync(_newGameScene);
            }

            _root.continueGame.RegisterCallbackPermanent<ClickEvent, NavigationSubmitEvent>(Run);
            _root.newGame.RegisterCallbackPermanent<ClickEvent, NavigationSubmitEvent>(() =>
            {
                playerDataService.Reset();
                Run();
            });

            _root.settings.RegisterCallbackPermanent<ClickEvent, NavigationSubmitEvent>(() =>
            {
                _root.settingsContainer.DisplayFlex();
                _uiFocusableService.AddLayer(_root.settingsContainer);
                _root.settingsContainer.FocusFirstFocusable();
            });

            _root.settingsContainer.RegisterCallback<NavigationCancelEvent>(evt =>
            {
                evt.StopPropagation();
                CloseSettings();
            });

            _globalStateService = Context.Resolve<GlobalStateService>();
            var popConfirmView = new PopConfirmView(Context, _root.popConfirmVT);

            void ResetPlayerData()
            {
                playerDataService.Reset();
                RefreshOptions();
            }

            _root.removeButton.RegisterCallback<ClickEvent>(_ =>
                popConfirmView.Show(I18N.MainMenuVT.remove, ResetPlayerData));

            var localizationService = Context.Resolve<LocalizationService>();
            
            void ChangeLocale(LocalizationService.MyLocales e)
            {
                localizationService.ChangeLocale(e);
                RefreshOptions();
            }

            _root.langRu.RegisterCallback<ClickEvent>(_ => ChangeLocale(LocalizationService.MyLocales.ru));
            _root.langEn.RegisterCallback<ClickEvent>(_ => ChangeLocale(LocalizationService.MyLocales.en));
        }

        public void EnableState()
        {
            _root.mainMenuContainer.DisplayFlex();
            _coroutine ??= StartCoroutine(RotateBackgroundImage());

            if (!_globalStateService.ChangeActiveState(this))
                return;

            DisplayFlex();
            _uiFocusableService.AddLayer(_root.mainMenuContainer);
            RefreshOptions();
        }

        private void RefreshOptions()
        {
            _root.continueGame.DisplayNone();
            _root.newGame.DisplayNone();

            if (_sceneCheckpointsService.IsNewGame())
            {
                _root.newGame.DisplayFlex();
                _root.newGame.Focus();
            }
            else
            {
                _root.continueGame.DisplayFlex();
                _root.continueGame.Focus();
            }
        }

        public void DisableState()
        {
            _uiFocusableService.RemoveLayer();
            DisplayNone();

            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        private void CloseSettings()
        {
            _uiFocusableService.RemoveLayer();
            _root.settingsContainer.DisplayNone();
        }

        private IEnumerator RotateBackgroundImage()
        {
            var t = _root.penta.transform;
            var startZ = t.rotation.eulerAngles.z;

            while (true)
            {
                t.rotation = Quaternion.Euler(new Vector3(0, 0, (startZ += Time.deltaTime)));
                yield return null;
            }
        }
    }
}