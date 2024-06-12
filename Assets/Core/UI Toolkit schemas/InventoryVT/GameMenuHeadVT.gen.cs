// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class GameMenuHeadVT
    {
        public static readonly string s_backgroundImage = "background-image";
        public static readonly string s_chips = "chips";
        public static readonly string s_chipsSelected = "chips-selected";
        public static readonly string s_equipment = "equipment";
        public static readonly string s_equipmentSelected = "equipment-selected";
        public static readonly string s_icon = "icon";
        public static readonly string s_inputIcon = "input-icon";
        public static readonly string s_settings = "settings";
        public static readonly string s_settingsSelected = "settings-selected";
        public static readonly string s_skills = "skills";
        public static readonly string s_skillsSelected = "skills-selected";
        public static readonly string s_textColorBase = "text-color-base";
        public static readonly string s_textColorBase_Label = "text-color-base Label";
        public static readonly string s_textFontBase = "text-font-base";
        public static readonly string s_titleBg = "title-bg";
    
        public VisualElement gameMenu;
        public VisualElement header;
        public VisualElement chips;
        public VisualElement equipment;
        public VisualElement skills;
        public VisualElement settings;
        public Label tabTitle;
        public TemplateContainer inventoryVT;
        public TemplateContainer playerAbilitiesVT;
        public TemplateContainer weaponsTabVT;
        public TemplateContainer settingsVT;
        public TemplateContainer popConfirmVT;
    
        public GameMenuHeadVT(VisualElement root)
        {
            gameMenu = root.Q<VisualElement>("GameMenu");
            header = root.Q<VisualElement>("Header");
            chips = root.Q<VisualElement>("Chips");
            equipment = root.Q<VisualElement>("Equipment");
            skills = root.Q<VisualElement>("Skills");
            settings = root.Q<VisualElement>("Settings");
            tabTitle = root.Q<Label>("TabTitle");
            inventoryVT = root.Q<TemplateContainer>("InventoryVT");
            playerAbilitiesVT = root.Q<TemplateContainer>("PlayerAbilitiesVT");
            weaponsTabVT = root.Q<TemplateContainer>("WeaponsTabVT");
            settingsVT = root.Q<TemplateContainer>("SettingsVT");
            popConfirmVT = root.Q<TemplateContainer>("PopConfirmVT");
        }
    }
}
