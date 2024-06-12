using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace Core
{
    public class FocusableToggle : Toggle, IFocusableElement
    {
        public FocusableElement FocusableElement { get; }

        public FocusableToggle() => FocusableElement = new(this);

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<FocusableToggle, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : Toggle.UxmlTraits
        {
        }

        #endregion
    }
}