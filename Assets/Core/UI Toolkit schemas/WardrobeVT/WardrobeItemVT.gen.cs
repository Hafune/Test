// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class WardrobeItemVT
    {
        public static readonly string s_container = "container";
    
        public Core.FocusableVisualElement item;
        public VisualElement selected;
        public VisualElement icon;
    
        public WardrobeItemVT(VisualElement root)
        {
            item = root.Q<Core.FocusableVisualElement>("Item");
            selected = root.Q<VisualElement>("Selected");
            icon = root.Q<VisualElement>("Icon");
        }
    }
}
