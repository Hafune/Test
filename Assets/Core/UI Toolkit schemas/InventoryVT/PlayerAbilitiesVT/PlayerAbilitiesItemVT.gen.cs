// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class PlayerAbilitiesItemVT
    {
        public static readonly string s_itemContainer = "item-container";
        public static readonly string s_itemIcon = "item-icon";
        public static readonly string s_itemInfoContainer = "item-info-container";
        public static readonly string s_itemLevel = "item-level";
        public static readonly string s_itemProgressContainer = "item-progress-container";
        public static readonly string s_itemTitle = "item-title";
        public static readonly string s_textColorBase = "text-color-base";
        public static readonly string s_textColorBase_Label = "text-color-base Label";
        public static readonly string s_textFontBase = "text-font-base";
        public static readonly string s_titleBg = "title-bg";
    
        public Core.FocusableVisualElement item;
        public VisualElement icon;
        public Label title;
        public Label level;
        public Label cost;
    
        public PlayerAbilitiesItemVT(VisualElement root)
        {
            item = root.Q<Core.FocusableVisualElement>("Item");
            icon = root.Q<VisualElement>("Icon");
            title = root.Q<Label>("Title");
            level = root.Q<Label>("Level");
            cost = root.Q<Label>("Cost");
        }
    }
}
