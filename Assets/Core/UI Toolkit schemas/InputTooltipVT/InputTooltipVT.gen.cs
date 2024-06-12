// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class InputTooltipVT
    {
    
    
        public VisualElement tooltip;
        public Label description;
        public Label subDescription;
    
        public InputTooltipVT(VisualElement root)
        {
            tooltip = root.Q<VisualElement>("Tooltip");
            description = root.Q<Label>("Description");
            subDescription = root.Q<Label>("SubDescription");
        }
    }
}
