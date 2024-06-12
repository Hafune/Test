using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Lib
{
    public static class MyVisualElementExtensions
    {
        public static void WarpCursorPosition(this VisualElement ele, Vector2? offset = null)
        {
#if !UNITY_WEBGL
            offset ??= Vector2.zero;

            var bounds = ele.worldBound;
            var _offset = bounds.size / 2f * offset.Value;
            var pos = bounds.center + _offset;
            pos.y = Screen.height - pos.y;
            Mouse.current?.WarpCursorPosition(pos);
#endif
        }

        public static bool IsDisplayFlex(this VisualElement ele) =>
            ele.style.display.value == DisplayStyle.Flex;

        public static void SetDisplay(this VisualElement ele, bool enable)
        {
            if (enable)
                ele.DisplayFlex();
            else
                ele.DisplayNone();
        }

        public static void DisplayFlex(this VisualElement ele) =>
            ele.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);

        public static void DisplayNone(this VisualElement ele) =>
            ele.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);

        public static bool FocusFirstFocusable(this VisualElement ele)
        {
            var firstEle = FindFirstFocusable(ele);

            if (firstEle is null)
                return false;

            firstEle.Focus();
            return true;
        }

        public static bool IsFocused(this VisualElement ele) =>
            ele.panel.focusController.focusedElement == ele;

        [CanBeNull]
        private static VisualElement FindFirstFocusable(VisualElement parent)
        {
            return parent
                .Children()
                .Where(ele => ele.visible)
                .Select(ele => ele.focusable ? ele : FindFirstFocusable(ele))
                .FirstOrDefault(focusable =>
                    focusable is not null &&
                    InDisplayInHierarchy(focusable) &&
                    IsVisibleInHierarchy(focusable));
        }

        private static bool IsVisibleInHierarchy(VisualElement ele)
        {
            if (!ele.visible)
                return false;

            return ele.parent is null || IsVisibleInHierarchy(ele.parent);
        }

        private static bool InDisplayInHierarchy(VisualElement ele) =>
            IsDisplayFlex(ele) && (ele.parent is null || InDisplayInHierarchy(ele.parent));

        public static void SetBackgroundImage(this VisualElement ele, Sprite backgroundImage) =>
            ele.style.backgroundImage = new StyleBackground(backgroundImage);

        public static void SetBackgroundColor(this VisualElement ele, Color color) =>
            ele.style.backgroundColor = new StyleColor(color);

        public static void StyleTranslateByWorldPosition(
            this VisualElement ele,
            Camera camera,
            Vector3 pos,
            SpriteAlignment align = SpriteAlignment.Center)
        {
            var position = RuntimePanelUtils.CameraTransformWorldToPanel(ele.panel, pos, camera);
            var style = ele.resolvedStyle;
            var width = style.width;
            var height = style.height;

            var offset = align switch
            {
                SpriteAlignment.Center => new Vector2(-width / 2, -height / 2),
                SpriteAlignment.TopLeft => new Vector2(-width, -height),
                SpriteAlignment.TopCenter => new Vector2(-width / 2, -height),
                SpriteAlignment.TopRight => new Vector2(0, -height),
                SpriteAlignment.LeftCenter => new Vector2(-width, -height / 2),
                SpriteAlignment.RightCenter => new Vector2(0, -height / 2),
                SpriteAlignment.BottomLeft => new Vector2(-width, 0),
                SpriteAlignment.BottomCenter => new Vector2(-width / 2, 0),
                SpriteAlignment.BottomRight => Vector2.zero,
                SpriteAlignment.Custom => Vector2.zero,
                _ => throw new ArgumentOutOfRangeException(nameof(align), align, null)
            };

            var x = position.x + offset.x;
            var y = position.y + offset.y;

            var parentStyle = ele.parent.resolvedStyle;
            x = Math.Max(0, x);
            y = Math.Max(0, y);
            x = Math.Min(parentStyle.width - style.width, x);
            y = Math.Min(parentStyle.height - style.height, y);

            StyleTranslate styleTranslate = new();
            styleTranslate.value = new(x, y);
            ele.style.translate = styleTranslate;
        }

        public static VisualElement SimpleCopyHierarchy(this VisualElement original)
        {
            var copy = (VisualElement)Activator.CreateInstance(original.GetType());
            copy.name = original.name;

            if (original.GetClasses() is List<string> classList)
            {
                int c = classList.Count;
                for (int i = 0; i < c; i++)
                    copy.AddToClassList(classList[i]);
            }

            foreach (var child in original.Children())
                copy.Add(SimpleCopyHierarchy(child));

            return copy;
        }

        public static void RegisterCallbackPermanent<T0, T1>(this VisualElement ele, Action callback)
            where T0 : EventBase<T0>, new()
            where T1 : EventBase<T1>, new()
        {
            ele.RegisterCallback<T0>(_ => callback());
            ele.RegisterCallback<T1>(_ => callback());
        }
    }
}