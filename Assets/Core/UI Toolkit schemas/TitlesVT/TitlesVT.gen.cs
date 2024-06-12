// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class TitlesVT
    {
        public static readonly string s_background = "background";
        public static readonly string s_end_text_position = "end_text_position";
        public static readonly string s_fade_in = "fade_in";
        public static readonly string s_navigation_fade_out = "navigation_fade_out";
        public static readonly string s_navigation_hidden = "navigation_hidden";
        public static readonly string s_navigationBar = "navigation-bar";
        public static readonly string s_navigationBarBackground = "navigation-bar-background";
        public static readonly string s_navigationBarButtonContainer = "navigation-bar-button-container";
        public static readonly string s_navigationBarButtonIcon = "navigation-bar-button-icon";
        public static readonly string s_start_text_position = "start_text_position";
        public static readonly string s_text = "text";
        public static readonly string s_text_size = "text_size";
        public static readonly string s_textColorBase = "text-color-base";
        public static readonly string s_textColorBase_Label = "text-color-base Label";
        public static readonly string s_textFontBase = "text-font-base";
    
        public VisualElement background;
        public VisualElement textContianer;
        public VisualElement navigationContainer;
    
        public TitlesVT(VisualElement root)
        {
            background = root.Q<VisualElement>("Background");
            textContianer = root.Q<VisualElement>("TextContianer");
            navigationContainer = root.Q<VisualElement>("NavigationContainer");
        }
    }
}
