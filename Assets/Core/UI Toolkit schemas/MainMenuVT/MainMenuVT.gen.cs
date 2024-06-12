// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class MainMenuVT
    {
        public static readonly string s_container = "container";
        public static readonly string s_focusableLabel = "focusable-label";
        public static readonly string s_iconContainer = "icon-container";
        public static readonly string s_languageIcon = "language-icon";
        public static readonly string s_mainMenuItem = "main-menu-item";
        public static readonly string s_mainMenuSettingsContainer = "main-menu-settings-container";
        public static readonly string s_navigationBar = "navigation-bar";
        public static readonly string s_navigationBarBackground = "navigation-bar-background";
        public static readonly string s_navigationBarButtonContainer = "navigation-bar-button-container";
        public static readonly string s_navigationBarButtonIcon = "navigation-bar-button-icon";
        public static readonly string s_textColorBase = "text-color-base";
        public static readonly string s_textColorBase_Label = "text-color-base Label";
        public static readonly string s_textFontBase = "text-font-base";
    
        public VisualElement penta;
        public Core.FocusableNodeElement mainMenuContainer;
        public Core.FocusableLabel continueGame;
        public Core.FocusableLabel newGame;
        public Core.FocusableLabel settings;
        public VisualElement languageIcons;
        public VisualElement langRu;
        public VisualElement langEn;
        public VisualElement navigationBar;
        public VisualElement settingsContainer;
        public TemplateContainer settingsVT;
        public VisualElement removeButton;
        public TemplateContainer popConfirmVT;
    
        public MainMenuVT(VisualElement root)
        {
            penta = root.Q<VisualElement>("Penta");
            mainMenuContainer = root.Q<Core.FocusableNodeElement>("MainMenuContainer");
            continueGame = root.Q<Core.FocusableLabel>("ContinueGame");
            newGame = root.Q<Core.FocusableLabel>("NewGame");
            settings = root.Q<Core.FocusableLabel>("Settings");
            languageIcons = root.Q<VisualElement>("LanguageIcons");
            langRu = root.Q<VisualElement>("LangRu");
            langEn = root.Q<VisualElement>("LangEn");
            navigationBar = root.Q<VisualElement>("NavigationBar");
            settingsContainer = root.Q<VisualElement>("SettingsContainer");
            settingsVT = root.Q<TemplateContainer>("SettingsVT");
            removeButton = root.Q<VisualElement>("RemoveButton");
            popConfirmVT = root.Q<TemplateContainer>("PopConfirmVT");
        }
    }
}
