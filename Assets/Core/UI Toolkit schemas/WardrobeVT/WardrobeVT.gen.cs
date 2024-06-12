// -----------------------
// script auto-generated
// any changes to this file will be lost on next code generation
// com.quickeye.ui-toolkit-plus ver: 3.0.3
// -----------------------
using UnityEngine.UIElements;

namespace Core
{
    public class WardrobeVT
    {
        public static readonly string s_container = "container";
        public static readonly string s_item_container = "item_container";
        public static readonly string s_itemWrapper = "item-wrapper";
    
        public VisualElement hairContainer;
        public VisualElement fashionContainer;
    
        public WardrobeVT(VisualElement root)
        {
            hairContainer = root.Q<VisualElement>("HairContainer");
            fashionContainer = root.Q<VisualElement>("FashionContainer");
        }
    }
}
