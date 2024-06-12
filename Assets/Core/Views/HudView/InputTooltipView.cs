using System.Collections;
using Core.Services;
using Core.Views.MainMenu;
using Lib;
using UnityEngine;

namespace Core.Views.HudView
{
    public class InputTooltipView : AbstractUIDocumentView
    {
        private InputTooltipVT _root;
        private PlayerStateService _playerStateService;
        private TooltipService _tooltipService;
        private Camera _camera;
        private Transform _followPoint;

        protected override void Awake()
        {
            base.Awake();
            _root = new InputTooltipVT(RootVisualElement);
            _camera = Context.Resolve<Camera>();
            _playerStateService = Context.Resolve<PlayerStateService>();

            _tooltipService = Context.Resolve<TooltipService>();
            _tooltipService.OnShowInputTooltip += ShowTooltip;
            _tooltipService.OnClose += Hide;
        }

        private void ShowTooltip()
        {
            _root.description.text = _tooltipService.InputDescription;
            _root.subDescription.text = _tooltipService.InputSubDescription;
            _followPoint = _playerStateService.PlayerTransform;

            Show();
        }

        private void Show()
        {
            if (RootVisualElement.IsDisplayFlex())
                return;

            _root.tooltip.style.opacity = 0;
            DisplayFlex();
            StartCoroutine(FollowForPoint());
        }

        private IEnumerator FollowForPoint()
        {
            while (RootVisualElement.IsDisplayFlex())
            {
                yield return null;
                _root.tooltip.style.opacity = 1;
                _root.tooltip.StyleTranslateByWorldPosition(
                    _camera,
                    _followPoint.position,
                    SpriteAlignment.TopCenter
                );
            }
        }

        private void Hide() => DisplayNone();
    }
}