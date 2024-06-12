using System;
using Core.Services;
using Core.Services.SpriteForInputService;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Views.MainMenu
{
    public class TutorialFocusView : AbstractUIDocumentView
    {
        private TutorialFocusVT _root;
        private UiInputsService _uiInputsService;
        private InputSpritesService _inputSpritesService;
        private Label _label;
        private VisualElement _container;
        private float _left;
        private float _right;
        private float _top;
        private float _bottom;
        private string _tempText;
        private StyleLength _leftWidth;
        private StyleLength _rightWidth;
        private StyleLength _topHeight;
        private StyleLength _bottomHeight;
        private string _text;
        private Camera _camera;

        private Rect WorldBounds => RootVisualElement.panel.visualTree.worldBound;

        protected override void Awake()
        {
            base.Awake();
            _root = new TutorialFocusVT(RootVisualElement);
            _uiInputsService = Context.Resolve<UiInputsService>();

            _label = _root.label;
            _container = _root.container;
            _inputSpritesService = Context.Resolve<InputSpritesService>();
            _leftWidth = _root.left.style.width;
            _rightWidth = _root.right.style.width;
            _topHeight = _root.top.style.height;
            _bottomHeight = _root.bottom.style.height;
            _camera = Context.Resolve<Camera>();
        }

        public void Show()
        {
            Reset();
            DisplayFlex();
            _uiInputsService.StopAll();
            _inputSpritesService.OnChange += UpdateTextInputSprites;
            UpdateTextInputSprites();
            _root.left.AddToClassList(TutorialFocusVT.s_tintShadow);
            _root.right.AddToClassList(TutorialFocusVT.s_tintShadow);
            _root.top.AddToClassList(TutorialFocusVT.s_tintShadow);
            _root.bottom.AddToClassList(TutorialFocusVT.s_tintShadow);
            _root.center.AddToClassList(TutorialFocusVT.s_tintShadow);
        }

        public void Hide()
        {
            DisplayNone();
            _uiInputsService.RestoreAll();
            _inputSpritesService.OnChange -= UpdateTextInputSprites;
            _root.left.RemoveFromClassList(TutorialFocusVT.s_tintShadow);
            _root.right.RemoveFromClassList(TutorialFocusVT.s_tintShadow);
            _root.top.RemoveFromClassList(TutorialFocusVT.s_tintShadow);
            _root.bottom.RemoveFromClassList(TutorialFocusVT.s_tintShadow);
            _root.center.RemoveFromClassList(TutorialFocusVT.s_tintShadow);
        }

        public void Select(VisualElement ele) => Select(ele.worldBound);

        public void Select(Rect bounds)
        {
            var offsetX = 5f;
            var offsetY = 10f;
            var global = WorldBounds;

            _left = (bounds.x - offsetX) / global.width;
            _right = 1f - (bounds.x + bounds.width + offsetX) / global.width;
            _top = (bounds.y - offsetY) / global.height;
            _bottom = 1f - (bounds.y + bounds.height + offsetY) / global.height;

            _root.left.style.width = new StyleLength { value = Length.Percent(_left * 100) };
            _root.right.style.width = new StyleLength { value = Length.Percent(_right * 100) };
            _root.top.style.height = new StyleLength { value = Length.Percent(_top * 100) };
            _root.bottom.style.height = new StyleLength { value = Length.Percent(_bottom * 100) };

            UpdateTextPosition();
        }

        public void ChangeText(string text)
        {
            _text = text;
            _label.text = _text;
            _tempText = _text;
        }

        private void UpdateTextPosition()
        {
            float top = 0f;
            float bottom = 0f;
            float left = 0f;
            float right = 0f;

            bool isMaxByHorizontal = Math.Max(_top, _bottom) > Math.Max(_left, _right);

            if (isMaxByHorizontal)
            {
                if (_top < _bottom)
                {
                    top = 1f - _bottom;
                }
                else
                {
                    bottom = 1f - _top;
                }
            }
            else
            {
                if (_right < _left)
                {
                    right = 1f - _left;
                }
                else
                {
                    left = 1f - _right;
                }
            }

            _container.style.top = new StyleLength(Length.Percent(top * 100));
            _container.style.bottom = new StyleLength(Length.Percent(bottom * 100));
            _container.style.right = new StyleLength(Length.Percent(right * 100));
            _container.style.left = new StyleLength(Length.Percent(left * 100));
        }

        private void UpdateTextInputSprites()
        {
            _tempText = _label.text;
            _label.text = string.Empty;
            _label.schedule.Execute(ScheduleDelayed1);
        }

        private void Reset()
        {
            _root.left.style.width = _leftWidth;
            _root.right.style.width = _rightWidth;
            _root.top.style.height = _topHeight;
            _root.bottom.style.height = _bottomHeight;
        }

        private void ScheduleDelayed1() => _label.schedule.Execute(ScheduleDelayed2);
        private void ScheduleDelayed2() => _label.text = _tempText;

        public Vector2 TransformWorldToPanel(Vector3 position) =>
            RuntimePanelUtils.CameraTransformWorldToPanel(RootVisualElement.panel, position, _camera);
    }
}