// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class BedMenuInHubVT
    {
        public static readonly string s_option = "option";
        public static readonly string s_window = "window";
    
        public Core.FocusableLabel returnBack;
        public Core.FocusableLabel restartLevel;
    
        public BedMenuInHubVT(VisualElement root)
        {
            returnBack = root.Q<Core.FocusableLabel>("ReturnBack");
            restartLevel = root.Q<Core.FocusableLabel>("RestartLevel");
        }
    }
}
