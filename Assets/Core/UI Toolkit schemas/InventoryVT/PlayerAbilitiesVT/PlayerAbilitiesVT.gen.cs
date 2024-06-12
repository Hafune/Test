// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class PlayerAbilitiesVT
    {
        public static readonly string s_abilitiesTab = "abilities-tab";
        public static readonly string s_abilitiesTab__descriptionContainer = "abilities-tab__description-container";
        public static readonly string s_itemWrapper = "item-wrapper";
        public static readonly string s_navigationBar = "navigation-bar";
        public static readonly string s_navigationBarBackground = "navigation-bar-background";
        public static readonly string s_navigationBarButtonContainer = "navigation-bar-button-container";
        public static readonly string s_navigationBarButtonIcon = "navigation-bar-button-icon";
        public static readonly string s_scrollView = "scroll-view";
        public static readonly string s_scrollView_unityScrollView__contentContainer = "scroll-view unity-scroll-view__content-container";
        public static readonly string s_textColorBase = "text-color-base";
        public static readonly string s_textColorBase_Label = "text-color-base Label";
        public static readonly string s_textFontBase = "text-font-base";
        public static readonly string s_titleBg = "title-bg";
    
        public ScrollView container;
        public VisualElement icon;
        public Label title;
        public Label description;
        public Label gemsCount;
        public VisualElement navigation;
    
        public PlayerAbilitiesVT(VisualElement root)
        {
            container = root.Q<ScrollView>("Container");
            icon = root.Q<VisualElement>("Icon");
            title = root.Q<Label>("Title");
            description = root.Q<Label>("Description");
            gemsCount = root.Q<Label>("GemsCount");
            navigation = root.Q<VisualElement>("Navigation");
        }
    }
}
