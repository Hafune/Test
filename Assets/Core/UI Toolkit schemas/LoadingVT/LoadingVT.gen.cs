// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class LoadingVT
    {
        public static readonly string s_container = "container";
        public static readonly string s_itemProgressBar = "item-progress-bar";
        public static readonly string s_itemProgressBar_unityLabel = "item-progress-bar unity-label";
        public static readonly string s_itemProgressBar_unityProgressBar__background = "item-progress-bar unity-progress-bar__background";
        public static readonly string s_itemProgressBar_unityProgressBar__container = "item-progress-bar unity-progress-bar__container";
        public static readonly string s_itemProgressBar_unityProgressBar__progress = "item-progress-bar unity-progress-bar__progress";
        public static readonly string s_loadingContainer = "loading-container";
        public static readonly string s_loadingLabel = "loading-label";
        public static readonly string s_saved = "saved";
        public static readonly string s_savedHide = "saved-hide";
        public static readonly string s_spinner = "spinner";
        public static readonly string s_textColorBase = "text-color-base";
        public static readonly string s_textColorBase_Label = "text-color-base Label";
        public static readonly string s_textFontBase = "text-font-base";
    
        public VisualElement loadingContainer;
        public ProgressBar loadingBar;
        public VisualElement spinner;
        public Label save;
    
        public LoadingVT(VisualElement root)
        {
            loadingContainer = root.Q<VisualElement>("LoadingContainer");
            loadingBar = root.Q<ProgressBar>("LoadingBar");
            spinner = root.Q<VisualElement>("Spinner");
            save = root.Q<Label>("Save");
        }
    }
}
