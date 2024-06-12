using Core.Components;
using Core.Lib;
using Core.Services;
using Lib;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core.Views.MainMenu
{
    public class HudView : AbstractUIDocumentView
    {
        [SerializeField] private Sprite[] _manaSeparators;

        private HudVT _root;
        private readonly Glossary<string> _stringCache = new();

        // protected override void Awake()
        // {
        //     base.Awake();
        //     _root = new HudVT(RootVisualElement);
        //     _weaponService = Context.Resolve<WeaponService>();
        //
        //     var goldKeyService = Context.Resolve<KeyGoldService>();
        //     goldKeyService.OnCountChange += value => _root.goldKeyCount.text = value.ToString();
        //     _root.goldKeyCount.text = goldKeyService.Count.ToString();
        //
        //     var silverKeyService = Context.Resolve<KeySilverService>();
        //     silverKeyService.OnCountChange += value => _root.silverKeyCount.text = value.ToString();
        //     _root.silverKeyCount.text = silverKeyService.Count.ToString();
        //
        //     var gemService = Context.Resolve<GemService>();
        //     gemService.OnCountChange += value => _root.gemCount.text = value.ToString();
        //     _root.gemCount.text = gemService.Count.ToString();
        //
        //     var playerStateService = Context.Resolve<PlayerStateService>();
        //     playerStateService.OnShowHud += Show;
        //     playerStateService.OnHideHud += Hide;
        //
        //     _mapService = Context.Resolve<MapService>();
        //     _teleportService = Context.Resolve<TeleportService>();
        //
        //     _root.attackGemContainer.DisplayNone();
        //     _root.defenceGemContainer.DisplayNone();
        //     _root.hitpointGemContainer.DisplayNone();
        //
        //     var magicGemService = Context.Resolve<MagicGemService>();
        //     magicGemService.OnChange += () =>
        //     {
        //         if (magicGemService.AttackGemCount != 0)
        //         {
        //             _root.attackGemContainer.DisplayFlex();
        //             _root.attackGemText.text = magicGemService.AttackGemCount.ToString();
        //         }
        //
        //         if (magicGemService.DefenceGemCount != 0)
        //         {
        //             _root.defenceGemContainer.DisplayFlex();
        //             _root.defenceGemText.text = magicGemService.DefenceGemCount.ToString();
        //         }
        //
        //         if (magicGemService.HitPointGemCount != 0)
        //         {
        //             _root.hitpointGemContainer.DisplayFlex();
        //             _root.hitpointGemText.text = magicGemService.HitPointGemCount.ToString();
        //         }
        //     };
        // }

        private void Show()
        {
            DisplayFlex();
        }

        private void Hide()
        {
            DisplayNone();
        }

        // private void Start()
        // {
        //     var worldMessages = Context.Resolve<EcsEngine>().WorldMessages;
        //     float _mp = 1;
        //     float _mpMax = 1;
        //     float _maxWheelAngle = 180f;
        //     float _wheelRotationOffset = 180f;
        //
        //     var abilitiesService = Context.Resolve<AbilitiesService>();
        //     _root.manaSeparator.SetBackgroundImage(
        //         _manaSeparators[abilitiesService.GetCurrentLevel(Abilities.mana_level)]);
        //     
        //     abilitiesService.OnChange += () =>
        //         _root.manaSeparator.SetBackgroundImage(
        //             _manaSeparators[abilitiesService.GetCurrentLevel(Abilities.mana_level)]);
        //
        //
        //     var dashSlot = new ActionSlotVT(_root.dashSlot);
        //     dashSlot.actionReload.style.height = new StyleLength(Length.Percent(0));
        //
        //     var weapon = _weaponService.CurrentWeapon;
        //     
        //     var weaponSlot = new ActionSlotVT(_root.weaponSlot);
        //     weaponSlot.actionReload.style.height = new StyleLength(Length.Percent(0));
        //     weaponSlot.actionIcon.SetBackgroundImage(weapon?.Icon);
        //     
        //     var specialSlot = new ActionSlotVT(_root.specialSlot);
        //     specialSlot.actionReload.style.height = new StyleLength(Length.Percent(0));
        //     specialSlot.actionIcon.SetBackgroundImage(weapon?.SpecialIcon);
        //     
        //     var healingSlot = new ActionSlotVT(_root.healingPotionSlot);
        //     healingSlot.actionReload.style.height = new StyleLength(Length.Percent(0));
        //     healingSlot.actionIcon.SetBackgroundImage(Abilities.healing_potion.Icon);
        //
        //     _weaponService.OnChange += () =>
        //     {
        //         var _weapon = _weaponService.CurrentWeapon;
        //         weaponSlot.actionIcon.SetBackgroundImage(_weapon?.Icon);
        //         specialSlot.actionIcon.SetBackgroundImage(_weapon?.SpecialIcon);
        //     };
        //
        //     void UpdateManaWheel() => _root.manaBarWheel.transform.rotation =
        //         Quaternion.Euler(0, 0, _mp / _mpMax * _maxWheelAngle - _wheelRotationOffset);
        //
        //     void UpdateHitPointTitle() =>
        //         _root.hitPointBar.title = $"{_root.hitPointBar.value}/{_root.hitPointBar.highValue}";
        //
        //     worldMessages.BuildUiEntityWithLink<PlayerUniqueTag>()
        //         .Add<HitPointValueComponent>(value =>
        //         {
        //             _root.hitPointBar.value = (int)value;
        //             UpdateHitPointTitle();
        //         })
        //         .Add<HitPointMaxValueComponent>(value =>
        //         {
        //             _root.hitPointBar.highValue = (int)value;
        //             UpdateHitPointTitle();
        //         })
        //         .Add<HealingPotionValueComponent>(value => { _root.healingPotionCount.text = value.ToString(); })
        //         .Add<ManaPointValueComponent>(value =>
        //         {
        //             _mp = value;
        //             _root.manaValue.text = GetCached(value);
        //             UpdateManaWheel();
        //         })
        //         .Add<ManaPointMaxValueComponent>(value =>
        //         {
        //             _mpMax = value;
        //             _root.manaMaxValue.text = GetCached(value);
        //             UpdateManaWheel();
        //         })
        //         .Add<DamageValueComponent>(value =>
        //             _root.damage.text = ((int)value).ToString())
        //         .Add<DefenceValueComponent>(value =>
        //             _root.defence.text = ((int)value).ToString())
        //         .Add<CriticalChanceValueComponent>(value =>
        //             _root.criticalChanse.text = FormatUiValuesUtility.ToPercentInt(value) + "%")
        //         .Add<CriticalDamageValueComponent>(value =>
        //             _root.criticalDamage.text = FormatUiValuesUtility.ToPercentInt(value) + "%")
        //         .Add<ReloadActionValueComponent<ActionDashComponent>>(value =>
        //             dashSlot.actionReload.style.height = new StyleLength(Length.Percent(100 - value * 100)))
        //         .Add<ReloadActionValueComponent<ActionSpecialAttackComponent>>(value =>
        //             specialSlot.actionReload.style.height = new StyleLength(Length.Percent(100 - value * 100)));
        // }

        private string GetCached(float value)
        {
            if (_stringCache.TryGetValue((int)value, out var v))
                return v;

            v = ((int)value).ToString();
            _stringCache.Add((int)value, v);

            return v;
        }
    }
}