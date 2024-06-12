// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class MagicGemMenuVT
    {
        public static readonly string s_background = "background";
        public static readonly string s_description = "description";
        public static readonly string s_frame = "frame";
        public static readonly string s_icon = "icon";
        public static readonly string s_navigationBar = "navigation-bar";
        public static readonly string s_navigationBarBackground = "navigation-bar-background";
        public static readonly string s_navigationBarButtonContainer = "navigation-bar-button-container";
        public static readonly string s_navigationBarButtonIcon = "navigation-bar-button-icon";
        public static readonly string s_textColorBase = "text-color-base";
        public static readonly string s_textColorBase_Label = "text-color-base Label";
        public static readonly string s_textFontBase = "text-font-base";
        public static readonly string s_titleBg = "title-bg";
        public static readonly string s_titleText = "title-text";
    
        public Core.FocusableVisualElement attackGem;
        public Core.FocusableVisualElement defenceGem;
        public Core.FocusableVisualElement hitpointGem;
    
        public MagicGemMenuVT(VisualElement root)
        {
            attackGem = root.Q<Core.FocusableVisualElement>("attack_gem");
            defenceGem = root.Q<Core.FocusableVisualElement>("defence_gem");
            hitpointGem = root.Q<Core.FocusableVisualElement>("hitpoint_gem");
        }
    }
}
