using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace Core
{
    public class FocusableNodeElement : VisualElement
    {
        public bool IsActive { get; private set; } = true;

        public FocusableNodeElement()
        {
            RegisterCallback<PointerDownEvent>(evt =>
            {
                evt.PreventDefault();
                evt.StopPropagation();
            });
            RegisterCallback<PointerUpEvent>(evt =>
            {
                evt.PreventDefault();
                evt.StopPropagation();
            });
        }

        public void SetActive(bool active)
        {
            IsActive = active;
            pickingMode = active ? PickingMode.Position : PickingMode.Ignore;
            RefreshFocusableRecursive(this);
        }

        private void RefreshFocusableRecursive(VisualElement ele)
        {
            foreach (var child in ele.Children())
            {
                if (child is FocusableNodeElement)
                    return;

                RefreshFocusableRecursive(child);

                if (child is not IFocusableElement element)
                    continue;

                element.FocusableElement.SetActive(IsActive);
            }
        }

        #region UXML

        [Preserve]
        public new class UxmlFactory : UxmlFactory<FocusableNodeElement, UxmlTraits>
        {
        }

        [Preserve]
        public new class UxmlTraits : VisualElement.UxmlTraits
        {
        }

        #endregion
    }
}