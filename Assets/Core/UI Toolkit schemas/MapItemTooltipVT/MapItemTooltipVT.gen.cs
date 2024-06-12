// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class MapItemTooltipVT
    {
        public static readonly string s_mapItemTooltip = "map-item-tooltip";
        public static readonly string s_mapItemTooltip__description = "map-item-tooltip__description";
        public static readonly string s_mapItemTooltip__icon = "map-item-tooltip__icon";
        public static readonly string s_mapItemTooltip__iconTitleContainer = "map-item-tooltip__icon-title-container";
        public static readonly string s_mapItemTooltip__slotContainer = "map-item-tooltip__slot-container";
        public static readonly string s_mapItemTooltip__title = "map-item-tooltip__title";
        public static readonly string s_textColorBase = "text-color-base";
        public static readonly string s_textColorBase_Label = "text-color-base Label";
        public static readonly string s_textFontBase = "text-font-base";
    
        public VisualElement tooltip;
        public VisualElement icon;
        public Label title;
        public Label description;
        public TemplateContainer slotDescriptionVT;
    
        public MapItemTooltipVT(VisualElement root)
        {
            tooltip = root.Q<VisualElement>("Tooltip");
            icon = root.Q<VisualElement>("Icon");
            title = root.Q<Label>("Title");
            description = root.Q<Label>("Description");
            slotDescriptionVT = root.Q<TemplateContainer>("SlotDescriptionVT");
        }
    }
}
