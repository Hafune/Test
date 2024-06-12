// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class DialogVT
    {
        public static readonly string s_icon = "icon";
        public static readonly string s_textColorBase = "text-color-base";
        public static readonly string s_textColorBase_Label = "text-color-base Label";
        public static readonly string s_textContainer = "text-container";
        public static readonly string s_textFontBase = "text-font-base";
        public static readonly string s_titleUnderline = "title-underline";
        public static readonly string s_window = "window";
    
        public Label title;
        public Label message;
        public VisualElement icon;
    
        public DialogVT(VisualElement root)
        {
            title = root.Q<Label>("Title");
            message = root.Q<Label>("Message");
            icon = root.Q<VisualElement>("Icon");
        }
    }
}
