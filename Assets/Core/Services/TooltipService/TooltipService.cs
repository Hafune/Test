using System;
using System.Collections.Generic;
using Core.Components;
using Core.EcsCommon.ValueSlotComponents;
using Core.Generated;
using Core.Systems;
using Lib;
using LurkingNinja.MyGame.Internationalization;
using Reflex;
using UnityEngine;

namespace Core.Services
{
    public class TooltipService : MonoConstruct
    {
        public event Action OnShowItemTooltip;
        public event Action OnShowInputTooltip;
        public event Action OnClose;

        public Transform DescriptionFollowTransform { get; private set; }
        public Sprite MapItemIcon { get; private set; }
        public string MapItemTitle { get; private set; }
        public string MapItemDescription { get; private set; }
        public IReadOnlyList<ValueData> MapItemSlotValues { get; private set; }
        public IReadOnlyList<SlotTagEnum> MapItemSlotTags { get; private set; }
        public string InputDescription { get; private set; }
        public string InputSubDescription { get; private set; }

        private Context _context;
        private PlayerStateService _playerStateService;
        private bool _isActiveShowInputTooltip;
        private bool _isActiveShowItemTooltip;

        protected override void Construct(Context context) => _context = context;

        // private void Awake()
        // {
        //     _playerStateService = _context.Resolve<PlayerStateService>();
        //     OnShowInputTooltip += () => _isActiveShowInputTooltip = true;
        //     OnShowItemTooltip += () => _isActiveShowItemTooltip = true;
        //
        //     var mapService = _context.Resolve<MapService>();
        //     var teleportService = _context.Resolve<TeleportService>();
        //     _enhancementService = _context.Resolve<EnhancementService>();
        //
        //     void OnOpenLocal()
        //     {
        //         if (!_isActiveShowInputTooltip)
        //             return;
        //
        //         OnClose?.Invoke();
        //     }
        //
        //     mapService.OnOpen += OnOpenLocal;
        //     teleportService.OnOpen += OnOpenLocal;
        //
        //     void OnCloseLocal()
        //     {
        //         if (_isActiveShowInputTooltip)
        //             OnShowInputTooltip?.Invoke();
        //
        //         if (_isActiveShowItemTooltip)
        //             OnShowItemTooltip?.Invoke();
        //     }
        //
        //     mapService.OnClose += OnCloseLocal;
        //     teleportService.OnClose += OnCloseLocal;
        // }

        // public void ShowWeaponTooltip(Weapons model, string title, Transform position)
        // {
        //     DescriptionFollowTransform = position;
        //     InputDescription = I18N.InputTooltip.pick;
        //     InputSubDescription = I18N.InputTooltip.hold_to_break_crystals;
        //     MapItemIcon = model.Icon;
        //     MapItemTitle = title;
        //     MapItemDescription = I18N.WeaponDescriptions.Table.GetLocalizedString(model.name);
        //     MapItemSlotValues = SlotUtility.CalculateValues(model.Values);
        //     MapItemSlotTags = model.Tags ?? (IReadOnlyList<SlotTagEnum>)Array.Empty<SlotTagEnum>();
        //     OnShowInputTooltip?.Invoke();
        //     OnShowItemTooltip?.Invoke();
        // }
        //
        // public void ShowEnhancementTooltip(Enhancements model, Transform position)
        // {
        //     DescriptionFollowTransform = position;
        //     InputDescription = I18N.InputTooltip.pick;
        //     InputSubDescription = I18N.InputTooltip.hold_to_break_crystals;
        //     MapItemIcon = model.Icon;
        //     MapItemTitle = _enhancementService.GetModelName(model);
        //     MapItemDescription = _enhancementService.GetModelDescription(model);
        //     MapItemSlotValues = SlotUtility.CalculateValues(model.Values);
        //     MapItemSlotTags = model.Tags ?? (IReadOnlyList<SlotTagEnum>)Array.Empty<SlotTagEnum>();
        //     OnShowInputTooltip?.Invoke();
        //     OnShowItemTooltip?.Invoke();
        // }

        public void ShowTeleportTooltip()
        {
            DescriptionFollowTransform = _playerStateService.PlayerTransform;
            InputDescription = I18N.InputTooltip.teleport;
            InputSubDescription = string.Empty;
            OnShowInputTooltip?.Invoke();
        }

        public void ShowWardrobeTooltip()
        {
            DescriptionFollowTransform = _playerStateService.PlayerTransform;
            InputDescription = I18N.InputTooltip.wardrobe;
            InputSubDescription = string.Empty;
            OnShowInputTooltip?.Invoke();
        }

        public void ShowChestTooltip()
        {
            DescriptionFollowTransform = _playerStateService.PlayerTransform;
            InputDescription = I18N.InputTooltip.chest;
            InputSubDescription = string.Empty;
            OnShowInputTooltip?.Invoke();
        }

        public void ShowMagicGemTooltip()
        {
            DescriptionFollowTransform = _playerStateService.PlayerTransform;
            InputDescription = I18N.InputTooltip.pick;
            InputSubDescription = string.Empty;
            OnShowInputTooltip?.Invoke();
        }

        public void ShowBedTooltip()
        {
            DescriptionFollowTransform = _playerStateService.PlayerTransform;
            InputDescription = I18N.InputTooltip.bed;
            InputSubDescription = string.Empty;
            OnShowInputTooltip?.Invoke();
        }

        public void HideTooltip()
        {
            _isActiveShowInputTooltip = false;
            _isActiveShowItemTooltip = false;
            OnClose?.Invoke();
        }
    }
}