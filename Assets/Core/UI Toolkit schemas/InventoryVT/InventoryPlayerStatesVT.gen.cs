// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class InventoryPlayerStatesVT
    {
        public static readonly string s_bar = "bar";
        public static readonly string s_energyContainer = "energy-container";
        public static readonly string s_hitPointBar_unityLabel = "hit-point-bar unity-label";
        public static readonly string s_hitPointBar_unityProgressBar__background = "hit-point-bar unity-progress-bar__background";
        public static readonly string s_hitPointBar_unityProgressBar__container = "hit-point-bar unity-progress-bar__container";
        public static readonly string s_hitPointBar_unityProgressBar__progress = "hit-point-bar unity-progress-bar__progress";
        public static readonly string s_label_container = "label_container";
        public static readonly string s_manaPointBar_unityProgressBar__progress = "mana-point-bar unity-progress-bar__progress";
        public static readonly string s_state_container = "state_container";
        public static readonly string s_state_icon = "state_icon";
        public static readonly string s_state_value = "state_value";
        public static readonly string s_textColorBase = "text-color-base";
        public static readonly string s_textColorBase_Label = "text-color-base Label";
        public static readonly string s_textFontBase = "text-font-base";
        public static readonly string s_titleBg = "title-bg";
    
        public Label energyUsed;
        public Label energyTotal;
        public ProgressBar hpBar;
        public Label hp;
        public Label hpMax;
        public ProgressBar mpBar;
        public Label mp;
        public Label mpMax;
        public Label damage;
        public Label defence;
        public Label criticalChanse;
        public Label criticalDamage;
        public Label gemCount;
    
        public InventoryPlayerStatesVT(VisualElement root)
        {
            energyUsed = root.Q<Label>("EnergyUsed");
            energyTotal = root.Q<Label>("EnergyTotal");
            hpBar = root.Q<ProgressBar>("HpBar");
            hp = root.Q<Label>("Hp");
            hpMax = root.Q<Label>("HpMax");
            mpBar = root.Q<ProgressBar>("MpBar");
            mp = root.Q<Label>("Mp");
            mpMax = root.Q<Label>("MpMax");
            damage = root.Q<Label>("Damage");
            defence = root.Q<Label>("Defence");
            criticalChanse = root.Q<Label>("CriticalChanse");
            criticalDamage = root.Q<Label>("CriticalDamage");
            gemCount = root.Q<Label>("GemCount");
        }
    }
}
