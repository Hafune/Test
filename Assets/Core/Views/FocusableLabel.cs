using System;
using Core.InputSprites;
using Reflex;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace Core
{
    public class FocusableLabel : Label, IFocusableElement, IContextElement
    {
        public FocusableElement FocusableElement { get; }

        public FocusableLabel() => FocusableElement = new(this);

#if UNITY_EDITOR
        private bool _hasManipulator;
#endif
        public void SetupContext(Context context)
        {
#if UNITY_EDITOR
            if (_hasManipulator)
                throw new Exception("Манипулятор уже добавлен");
#endif
            this.AddManipulator(new LabelSfxManipulator(context));
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<FocusableLabel, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : Label.UxmlTraits
        {
        }

        #endregion
    }
}