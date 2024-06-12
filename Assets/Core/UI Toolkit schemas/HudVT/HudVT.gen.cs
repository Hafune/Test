// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class HudVT
    {
        public static readonly string s_bloodsplit = "bloodsplit";
        public static readonly string s_inputContainer = "input-container";
        public static readonly string s_inputIcon = "input-icon";
        public static readonly string s_inputPlus = "input-plus";
        public static readonly string s_itemProgressBar = "item-progress-bar";
        public static readonly string s_itemProgressBar_unityLabel = "item-progress-bar unity-label";
        public static readonly string s_itemProgressBar_unityProgressBar__background = "item-progress-bar unity-progress-bar__background";
        public static readonly string s_itemProgressBar_unityProgressBar__container = "item-progress-bar unity-progress-bar__container";
        public static readonly string s_itemProgressBar_unityProgressBar__progress = "item-progress-bar unity-progress-bar__progress";
        public static readonly string s_magicGemContainer = "magic-gem-container";
        public static readonly string s_magicGemIcon = "magic-gem-icon";
        public static readonly string s_manaText = "mana-text";
        public static readonly string s_state_container = "state_container";
        public static readonly string s_state_icon = "state_icon";
        public static readonly string s_state_value = "state_value";
        public static readonly string s_textColorBase = "text-color-base";
        public static readonly string s_textColorBase_Label = "text-color-base Label";
        public static readonly string s_textFontBase = "text-font-base";
    
        public VisualElement manaBarWheel;
        public VisualElement manaSeparator;
        public ProgressBar hitPointBar;
        public Label damage;
        public Label defence;
        public Label criticalChanse;
        public Label criticalDamage;
        public Label manaValue;
        public Label manaMaxValue;
        public TemplateContainer weaponSlot;
        public TemplateContainer specialSlot;
        public TemplateContainer dashSlot;
        public TemplateContainer healingPotionSlot;
        public Label healingPotionCount;
        public VisualElement attackGemContainer;
        public Label attackGemText;
        public VisualElement defenceGemContainer;
        public Label defenceGemText;
        public VisualElement hitpointGemContainer;
        public Label hitpointGemText;
        public Label goldKeyCount;
        public Label silverKeyCount;
        public Label gemCount;
    
        public HudVT(VisualElement root)
        {
            manaBarWheel = root.Q<VisualElement>("ManaBarWheel");
            manaSeparator = root.Q<VisualElement>("ManaSeparator");
            hitPointBar = root.Q<ProgressBar>("HitPointBar");
            damage = root.Q<Label>("Damage");
            defence = root.Q<Label>("Defence");
            criticalChanse = root.Q<Label>("CriticalChanse");
            criticalDamage = root.Q<Label>("CriticalDamage");
            manaValue = root.Q<Label>("ManaValue");
            manaMaxValue = root.Q<Label>("ManaMaxValue");
            weaponSlot = root.Q<TemplateContainer>("WeaponSlot");
            specialSlot = root.Q<TemplateContainer>("SpecialSlot");
            dashSlot = root.Q<TemplateContainer>("DashSlot");
            healingPotionSlot = root.Q<TemplateContainer>("HealingPotionSlot");
            healingPotionCount = root.Q<Label>("HealingPotionCount");
            attackGemContainer = root.Q<VisualElement>("AttackGemContainer");
            attackGemText = root.Q<Label>("AttackGemText");
            defenceGemContainer = root.Q<VisualElement>("DefenceGemContainer");
            defenceGemText = root.Q<Label>("DefenceGemText");
            hitpointGemContainer = root.Q<VisualElement>("HitpointGemContainer");
            hitpointGemText = root.Q<Label>("HitpointGemText");
            goldKeyCount = root.Q<Label>("GoldKeyCount");
            silverKeyCount = root.Q<Label>("SilverKeyCount");
            gemCount = root.Q<Label>("GemCount");
        }
    }
}
