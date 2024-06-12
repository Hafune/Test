// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class WeaponsTabItemVT
    {
        public static readonly string s_focusableLabel = "focusable-label";
        public static readonly string s_weaponsTab__itemContainer = "weapons-tab__item-container";
        public static readonly string s_weaponsTab__itemGemCount = "weapons-tab__item-gem-count";
        public static readonly string s_weaponsTab__itemGemIcon = "weapons-tab__item-gem-icon";
        public static readonly string s_weaponsTab__itemIcon = "weapons-tab__item-icon";
        public static readonly string s_weaponsTab__itemInfoContainer = "weapons-tab__item-info-container";
        public static readonly string s_weaponsTab__itemTitle = "weapons-tab__item-title";
        public static readonly string s_weaponsTab__itemUsedIcon = "weapons-tab__item-used-icon";
    
        public VisualElement item;
        public VisualElement usedIcon;
        public VisualElement icon;
        public Label title;
        public Label cost;
    
        public WeaponsTabItemVT(VisualElement root)
        {
            item = root.Q<VisualElement>("Item");
            usedIcon = root.Q<VisualElement>("UsedIcon");
            icon = root.Q<VisualElement>("Icon");
            title = root.Q<Label>("Title");
            cost = root.Q<Label>("Cost");
        }
    }
}
