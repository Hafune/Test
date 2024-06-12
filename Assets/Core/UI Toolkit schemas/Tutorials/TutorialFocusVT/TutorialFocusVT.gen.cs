// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class TutorialFocusVT
    {
        public static readonly string s_fade = "fade";
        public static readonly string s_textColorBase = "text-color-base";
        public static readonly string s_textColorBase_Label = "text-color-base Label";
        public static readonly string s_textFontBase = "text-font-base";
        public static readonly string s_textSize = "text-size";
        public static readonly string s_tint = "tint";
        public static readonly string s_tintShadow = "tint-shadow";
        public static readonly string s_transition = "transition";
    
        public VisualElement top;
        public VisualElement left;
        public VisualElement center;
        public VisualElement right;
        public VisualElement bottom;
        public VisualElement container;
        public Label label;
    
        public TutorialFocusVT(VisualElement root)
        {
            top = root.Q<VisualElement>("Top");
            left = root.Q<VisualElement>("Left");
            center = root.Q<VisualElement>("Center");
            right = root.Q<VisualElement>("Right");
            bottom = root.Q<VisualElement>("Bottom");
            container = root.Q<VisualElement>("Container");
            label = root.Q<Label>("Label");
        }
    }
}
