// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class SlotDescriptionVT
    {
        public static readonly string s_slotDescription__container = "slot-description__container";
        public static readonly string s_slotDescription__iconNameContainer = "slot-description__icon-name-container";
        public static readonly string s_slotDescription__row = "slot-description__row";
        public static readonly string s_slotDescription__tagDescroption = "slot-description__tag-descroption";
        public static readonly string s_slotDescription__value = "slot-description__value";
        public static readonly string s_slotDescription__valueIcon = "slot-description__value-icon";
        public static readonly string s_slotDescription__valueName = "slot-description__value-name";
        public static readonly string s_textColorBase = "text-color-base";
        public static readonly string s_textColorBase_Label = "text-color-base Label";
        public static readonly string s_textFontBase = "text-font-base";
    
        public Core.SlotDescriptionElement mainContainer;
        public VisualElement container;
        public VisualElement row;
        public VisualElement valueIcon;
        public Label valueName;
        public Label value;
        public VisualElement tagContainer;
        public Label tagText;
    
        public SlotDescriptionVT(VisualElement root)
        {
            mainContainer = root.Q<Core.SlotDescriptionElement>("MainContainer");
            container = root.Q<VisualElement>("Container");
            row = root.Q<VisualElement>("Row");
            valueIcon = root.Q<VisualElement>("ValueIcon");
            valueName = root.Q<Label>("ValueName");
            value = root.Q<Label>("Value");
            tagContainer = root.Q<VisualElement>("TagContainer");
            tagText = root.Q<Label>("TagText");
        }
    }
}
