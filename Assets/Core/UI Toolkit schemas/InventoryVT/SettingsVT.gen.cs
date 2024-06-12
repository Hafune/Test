// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class SettingsVT
    {
        public static readonly string s_focusableLabel = "focusable-label";
        public static readonly string s_fontSize = "font-size";
        public static readonly string s_navigationBar = "navigation-bar";
        public static readonly string s_navigationBarBackground = "navigation-bar-background";
        public static readonly string s_navigationBarButtonContainer = "navigation-bar-button-container";
        public static readonly string s_navigationBarButtonIcon = "navigation-bar-button-icon";
        public static readonly string s_settingsContainer = "settings-container";
        public static readonly string s_slider = "slider";
        public static readonly string s_slider_unityBaseField__input = "slider unity-base-field__input";
        public static readonly string s_textColorBase = "text-color-base";
        public static readonly string s_textColorBase_Label = "text-color-base Label";
        public static readonly string s_textFontBase = "text-font-base";
    
        public Core.FocusableSlider masterVolume;
        public Core.FocusableSlider backgroundVolume;
        public Core.FocusableSlider sfxVolume;
        public Core.FocusableVisualElement language;
        public VisualElement languageLeftArrow;
        public VisualElement languageRightArrow;
        public Core.FocusableToggle sSAO;
        public Core.FocusableToggle shadows;
        public Core.FocusableSlider drawingDistance;
        public Core.FocusableSlider renderScale;
    
        public SettingsVT(VisualElement root)
        {
            masterVolume = root.Q<Core.FocusableSlider>("MasterVolume");
            backgroundVolume = root.Q<Core.FocusableSlider>("BackgroundVolume");
            sfxVolume = root.Q<Core.FocusableSlider>("SfxVolume");
            language = root.Q<Core.FocusableVisualElement>("Language");
            languageLeftArrow = root.Q<VisualElement>("LanguageLeftArrow");
            languageRightArrow = root.Q<VisualElement>("LanguageRightArrow");
            sSAO = root.Q<Core.FocusableToggle>("SSAO");
            shadows = root.Q<Core.FocusableToggle>("Shadows");
            drawingDistance = root.Q<Core.FocusableSlider>("DrawingDistance");
            renderScale = root.Q<Core.FocusableSlider>("RenderScale");
        }
    }
}
