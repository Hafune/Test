// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class BossHitPointBarVT
    {
        public static readonly string s_bar = "bar";
        public static readonly string s_bar_unityLabel = "bar unity-label";
        public static readonly string s_bar_unityProgressBar__background = "bar unity-progress-bar__background";
        public static readonly string s_bar_unityProgressBar__container = "bar unity-progress-bar__container";
        public static readonly string s_bar_unityProgressBar__progress = "bar unity-progress-bar__progress";
        public static readonly string s_container = "container";
        public static readonly string s_textColorBase = "text-color-base";
        public static readonly string s_textColorBase_Label = "text-color-base Label";
        public static readonly string s_textFontBase = "text-font-base";
    
        public ProgressBar bar;
    
        public BossHitPointBarVT(VisualElement root)
        {
            bar = root.Q<ProgressBar>("Bar");
        }
    }
}
