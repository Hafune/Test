using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Lib;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core
{
    public class FocusableElement
    {
        public class NavigationContextEvent : NavigationEventBase<NavigationContextEvent>
        {
        }

        public static void SendContextMenuEvent(VisualElement rootVisualElement)
        {
            using var e = EventBase<NavigationContextEvent>.GetPooled();
            rootVisualElement.SendEvent(e);
        }

        public event Action OnSubmit;
        // public event Action OnSubmitHeld;
        public event Action OnContextMenu;
        public event Action OnFocus;

        public bool IsMouseEnter { get; private set; }
        public Vector2 mouseWarpOffset = new(.5f, 0);

        private readonly VisualElement _ele;
        private readonly List<VisualElement> _pickingElements = new();

        public FocusableElement(VisualElement ele)
        {
            _ele = ele;
            _ele.tabIndex = 0;

            void FindPickings(VisualElement element)
            {
                if (element.pickingMode == PickingMode.Position)
                    _pickingElements.Add(element);

                foreach (var child in element.hierarchy.Children())
                    FindPickings(child);
            }

            FindPickings(ele);

            ele.RegisterCallback<NavigationContextEvent>(evt =>
            {
                if (ele.focusController.focusedElement != ele)
                    return;

                evt.StopPropagation();
                OnContextMenu?.Invoke();
            });
            ele.RegisterCallback<ClickEvent>(evt =>
            {
                if (evt.button == 0)
                    OnSubmit?.Invoke();
                else
                    OnContextMenu?.Invoke();
            });
            ele.RegisterCallback<FocusEvent>(_ =>
            {
                if (IsMouseEnter)
                    return;

                ele.schedule.Execute(WarpCursorPosition);
                OnFocus?.Invoke();
            });
            ele.RegisterCallback<MouseEnterEvent>(_ =>
            {
                IsMouseEnter = true;
                ele.Focus();
                OnFocus?.Invoke();
            });
            ele.RegisterCallback<DetachFromPanelEvent>(_ => ele.Blur());
            ele.RegisterCallback<MouseLeaveEvent>(_ => IsMouseEnter = false);
            ele.RegisterCallback<NavigationSubmitEvent>(_ => OnSubmit?.Invoke());

            ele.schedule.Execute(RefreshActive);
        }

        public void SetActive(bool isActive)
        {
            _ele.focusable = isActive;

            foreach (var element in _pickingElements)
                element.pickingMode = isActive ? PickingMode.Position : PickingMode.Ignore;
        }

        private void WarpCursorPosition() => _ele.WarpCursorPosition(mouseWarpOffset);

        private void RefreshActive() => SetActive(FindFocusableNodeRecursive(_ele)?.IsActive == true);

        [CanBeNull]
        private static FocusableNodeElement FindFocusableNodeRecursive(VisualElement ele)
        {
            while (true)
            {
                if (ele is FocusableNodeElement element)
                    return element;

                if (ele.parent is null)
                    return null;

                ele = ele.parent;
            }
        }
    }
}