using System.Collections;
using Core.Services;
using Core.Views.MainMenu;
using Lib;
using UnityEngine;

namespace Core.Views
{
    public class MapItemTooltipView : AbstractUIDocumentView
    {
        private MapItemTooltipVT _root;
        private TooltipService _tooltipService;
        private Camera _camera;
        private Transform _followTransform;
        private SlotDescriptionElement _slotDescription;

        protected override void Awake()
        {
            base.Awake();
            _root = new MapItemTooltipVT(RootVisualElement);
            _slotDescription = new SlotDescriptionVT(_root.slotDescriptionVT).mainContainer;
            _camera = Context.Resolve<Camera>();
            _tooltipService = Context.Resolve<TooltipService>();
            _tooltipService.OnShowItemTooltip += ShowTooltip;
            _tooltipService.OnClose += Hide;
        }

        private void ShowTooltip()
        {
            _root.icon.SetBackgroundImage(_tooltipService.MapItemIcon);
            _root.title.text = _tooltipService.MapItemTitle;
            _root.description.text = _tooltipService.MapItemDescription;
            _slotDescription.Refresh(_tooltipService.MapItemSlotValues, _tooltipService.MapItemSlotTags);
            _followTransform = _tooltipService.DescriptionFollowTransform;

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
                    _followTransform.position,
                    SpriteAlignment.BottomCenter
                );
            }
        }

        private void Hide() => DisplayNone();
    }
}