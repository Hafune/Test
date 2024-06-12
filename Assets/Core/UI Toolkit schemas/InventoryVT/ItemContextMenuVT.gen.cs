// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class ItemContextMenuVT
    {
        public static readonly string s_button = "button";
        public static readonly string s_buttonIcon = "button-icon";
        public static readonly string s_buttonText = "button-text";
        public static readonly string s_optionsText = "options-text";
    
        public VisualElement itemContextMenuWrapper;
        public VisualElement itemContextMenu;
        public Core.FocusableNodeElement container;
    
        public ItemContextMenuVT(VisualElement root)
        {
            itemContextMenuWrapper = root.Q<VisualElement>("ItemContextMenuWrapper");
            itemContextMenu = root.Q<VisualElement>("ItemContextMenu");
            container = root.Q<Core.FocusableNodeElement>("Container");
        }
    }
}
