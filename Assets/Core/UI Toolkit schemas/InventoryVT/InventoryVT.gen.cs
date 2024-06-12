// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class InventoryVT
    {
        public static readonly string s_columnSection = "column-section";
        public static readonly string s_enegyLockedSpace = "enegy-locked-space";
        public static readonly string s_energyLockedIcon = "energy-locked-icon";
        public static readonly string s_enhancement = "enhancement";
        public static readonly string s_enhancementSelected = "enhancement-selected";
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
    
        public ScrollView inventoryItems;
        public VisualElement insertedEnhancements;
        public VisualElement energyLockedSpace1;
        public VisualElement energyLockedSpace2;
        public VisualElement energyLockedSpace3;
        public VisualElement energyLockedSpace4;
        public TemplateContainer inventoryPlayerStatesVT;
        public VisualElement icon;
        public Label description;
        public VisualElement navigationBar;
        public Label actionText;
        public Label cancelText;
        public VisualElement contextMenu;
    
        public InventoryVT(VisualElement root)
        {
            inventoryItems = root.Q<ScrollView>("InventoryItems");
            insertedEnhancements = root.Q<VisualElement>("InsertedEnhancements");
            energyLockedSpace1 = root.Q<VisualElement>("EnergyLockedSpace1");
            energyLockedSpace2 = root.Q<VisualElement>("EnergyLockedSpace2");
            energyLockedSpace3 = root.Q<VisualElement>("EnergyLockedSpace3");
            energyLockedSpace4 = root.Q<VisualElement>("EnergyLockedSpace4");
            inventoryPlayerStatesVT = root.Q<TemplateContainer>("InventoryPlayerStatesVT");
            icon = root.Q<VisualElement>("Icon");
            description = root.Q<Label>("Description");
            navigationBar = root.Q<VisualElement>("NavigationBar");
            actionText = root.Q<Label>("ActionText");
            cancelText = root.Q<Label>("CancelText");
            contextMenu = root.Q<VisualElement>("ContextMenu");
        }
    }
}
