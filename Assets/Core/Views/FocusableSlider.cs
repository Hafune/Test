using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace Core
{
    public class FocusableSlider : Slider, IFocusableElement
    {
        public FocusableElement FocusableElement { get; }

        public FocusableSlider() => FocusableElement = new(this);

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<FocusableSlider, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : Slider.UxmlTraits
        {
        }

        #endregion
    }
}