// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class WeaponsTabVT
    {
        public static readonly string s_focusableLabel = "focusable-label";
        public static readonly string s_item = "item";
        public static readonly string s_itemCost = "item-cost";
        public static readonly string s_itemIcon = "item-icon";
        public static readonly string s_itemTitle = "item-title";
        public static readonly string s_itemUsedIcon = "item-used-icon";
        public static readonly string s_myScrollView_unityDragger = "my-scroll-view #unity-dragger";
        public static readonly string s_myScrollView_unityHighButton = "my-scroll-view #unity-high-button";
        public static readonly string s_myScrollView_unityLowButton = "my-scroll-view #unity-low-button";
        public static readonly string s_myScrollView_unitySlider = "my-scroll-view #unity-slider";
        public static readonly string s_myScrollView_unityTracker = "my-scroll-view #unity-tracker";
        public static readonly string s_myScrollView_unityScrollerVertical = "my-scroll-view unity-scroller--vertical";
        public static readonly string s_navigationBar = "navigation-bar";
        public static readonly string s_navigationBarBackground = "navigation-bar-background";
        public static readonly string s_navigationBarButtonContainer = "navigation-bar-button-container";
        public static readonly string s_navigationBarButtonIcon = "navigation-bar-button-icon";
        public static readonly string s_textColorBase = "text-color-base";
        public static readonly string s_textColorBase_Label = "text-color-base Label";
        public static readonly string s_textFontBase = "text-font-base";
        public static readonly string s_titleBg = "title-bg";
        public static readonly string s_weaponsTab__descriptionContainer = "weapons-tab__description-container";
        public static readonly string s_weaponsTab__slotDescription__container = "weapons-tab__slot-description__container";
        public static readonly string s_weaponsTabSection = "weapons-tab-section";
    
        public ScrollView weapons;
        public VisualElement currentWeaponDescriptionContainer;
        public VisualElement currentWeaponIcon;
        public Label currentWeaponTitle;
        public Label currentWeaponDescription;
        public TemplateContainer currentWeaponSlotDescriptionVT;
        public VisualElement selectedWeaponDescriptionContainer;
        public VisualElement icon;
        public Label title;
        public Label description;
        public TemplateContainer slotDescriptionVT;
        public VisualElement navigationBar;
        public Label actionText;
        public Label cancelText;
        public VisualElement contextMenu;
    
        public WeaponsTabVT(VisualElement root)
        {
            weapons = root.Q<ScrollView>("Weapons");
            currentWeaponDescriptionContainer = root.Q<VisualElement>("CurrentWeaponDescriptionContainer");
            currentWeaponIcon = root.Q<VisualElement>("CurrentWeaponIcon");
            currentWeaponTitle = root.Q<Label>("CurrentWeaponTitle");
            currentWeaponDescription = root.Q<Label>("CurrentWeaponDescription");
            currentWeaponSlotDescriptionVT = root.Q<TemplateContainer>("CurrentWeaponSlotDescriptionVT");
            selectedWeaponDescriptionContainer = root.Q<VisualElement>("SelectedWeaponDescriptionContainer");
            icon = root.Q<VisualElement>("Icon");
            title = root.Q<Label>("Title");
            description = root.Q<Label>("Description");
            slotDescriptionVT = root.Q<TemplateContainer>("SlotDescriptionVT");
            navigationBar = root.Q<VisualElement>("NavigationBar");
            actionText = root.Q<Label>("ActionText");
            cancelText = root.Q<Label>("CancelText");
            contextMenu = root.Q<VisualElement>("ContextMenu");
        }
    }
}
