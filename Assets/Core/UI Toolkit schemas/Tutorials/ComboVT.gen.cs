// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class ComboVT
    {
        public static readonly string s_button = "button";
        public static readonly string s_combo = "combo";
        public static readonly string s_combo_visible = "combo_visible";
        public static readonly string s_my_text = "my_text";
        public static readonly string s_success = "success";
        public static readonly string s_textColorBase = "text-color-base";
        public static readonly string s_textColorBase_Label = "text-color-base Label";
        public static readonly string s_textFontBase = "text-font-base";
        public static readonly string s_un_success = "un_success";
    
        public VisualElement combo;
        public VisualElement success;
    
        public ComboVT(VisualElement root)
        {
            combo = root.Q<VisualElement>("Combo");
            success = root.Q<VisualElement>("Success");
        }
    }
}
