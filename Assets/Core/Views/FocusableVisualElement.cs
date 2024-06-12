using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace Core
{
    //available in version 2023.2 and up.
    //https://forum.unity.com/threads/custom-ui-builder-attribute-fields.945491/
    //[UXMLElement]
    public class FocusableVisualElement : VisualElement, IFocusableElement
    {
        // [UXMLAttribute]
        // public float value { get; set; }
        public FocusableElement FocusableElement { get; }

        public FocusableVisualElement() => FocusableElement = new(this);

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<FocusableVisualElement, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}