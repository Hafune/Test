// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class ActionSlotVT
    {
        public static readonly string s_actionContainer = "action-container";
        public static readonly string s_actionIcon = "action-icon";
        public static readonly string s_actionIconContainer = "action-icon-container";
        public static readonly string s_actionReloadProgress = "action-reload-progress";
    
        public VisualElement actionIcon;
        public VisualElement actionReload;
    
        public ActionSlotVT(VisualElement root)
        {
            actionIcon = root.Q<VisualElement>("ActionIcon");
            actionReload = root.Q<VisualElement>("ActionReload");
        }
    }
}
