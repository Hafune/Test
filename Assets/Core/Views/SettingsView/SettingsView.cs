using System;
using Core.Services;
using Lib;
using LurkingNinja.MyGame.Internationalization;
using Reflex;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core
{
    public class SettingsView : ITabElement
    {
        private readonly SettingsVT _root;
        private readonly VisualElement _rootVisualElement;
        private float _step = .04f;
        private readonly Context _context;

        public SettingsView(Context context, VisualElement rootVisualElement)
        {
            _context = context;
            _rootVisualElement = rootVisualElement;

            var audioSourceService = _context.Resolve<AudioSourceService>();
            var graphicsSettingsService = _context.Resolve<GraphicsSettingsService>();

            _root = new SettingsVT(_rootVisualElement);
            var _ = new UIElementLocalization(_rootVisualElement, I18N.SettingsVT.Table);

            SetupSlider(
                _root.masterVolume,
                change: audioSourceService.ChangeMasterVolume,
                getValue: () => audioSourceService.MasterVolume,
                onChange: null);

            SetupSlider(
                _root.backgroundVolume,
                change: audioSourceService.ChangeBackgroundVolume,
                getValue: () => audioSourceService.BackgroundVolume,
                onChange: null);

            var uiSfx = _context.Resolve<UiSfxTemplate>();
            var submitSourcePrefab = uiSfx.SubmitSourceContainer.GetComponent<AudioSource>();
            float time = Time.time;
            float playDelay = .1f;

            SetupSlider(
                _root.sfxVolume,
                change: audioSourceService.ChangeSFXVolume,
                getValue: () => audioSourceService.SFXVolume,
                onChange: () =>
                {
                    if (Time.time - time < playDelay)
                        return;

                    time = Time.time;
                    audioSourceService.PlayOneShotUI(submitSourcePrefab);
                });

            SetupSlider(
                _root.drawingDistance,
                change: graphicsSettingsService.ChangeDrawingDistance,
                getValue: () => graphicsSettingsService.DrawingDistance,
                onChange: null);

            SetupSlider(
                _root.renderScale,
                change: graphicsSettingsService.ChangeResolutionQuality,
                getValue: () => graphicsSettingsService.RenderScale,
                onChange: null);

            var triggerSourcePrefab = uiSfx.TriggerSourceContainer.GetComponent<AudioSource>();
            var selectSourcePrefab = uiSfx.SelectSourceContainer.GetComponent<AudioSource>();
            var localizationService = _context.Resolve<LocalizationService>();

            _root.language.RegisterCallback<FocusEvent>(_ => audioSourceService.PlayOneShotUI(selectSourcePrefab));
            _root.language.RegisterCallback<NavigationMoveEvent>(evt =>
            {
                switch (evt.direction)
                {
                    case NavigationMoveEvent.Direction.Left:
                        audioSourceService.PlayOneShotUI(triggerSourcePrefab);
                        localizationService.SelectPrevious();
                        evt.StopPropagation();
                        evt.PreventDefault();
                        break;
                    case NavigationMoveEvent.Direction.Right:
                        audioSourceService.PlayOneShotUI(triggerSourcePrefab);
                        localizationService.SelectNext();
                        evt.StopPropagation();
                        evt.PreventDefault();
                        break;
                }
            });

            _root.languageLeftArrow.pickingMode = PickingMode.Position;
            _root.languageLeftArrow.RegisterCallback<ClickEvent>(_ => localizationService.SelectPrevious());

            _root.languageRightArrow.pickingMode = PickingMode.Position;
            _root.languageRightArrow.RegisterCallback<ClickEvent>(_ => localizationService.SelectNext());

            _root.sSAO.AddManipulator(new ToggleSfxManipulator(_context));
            _root.sSAO.RegisterValueChangedCallback(evt =>
                graphicsSettingsService.ChangeSSAO(evt.newValue));

            _root.sSAO.RegisterCallback<NavigationMoveEvent>(evt =>
            {
                switch (evt.direction)
                {
                    case NavigationMoveEvent.Direction.Left:
                        _root.sSAO.value = !graphicsSettingsService.SSAO;
                        graphicsSettingsService.ChangeSSAO(_root.sSAO.value);
                        break;
                    case NavigationMoveEvent.Direction.Right:
                        _root.sSAO.value = !graphicsSettingsService.SSAO;
                        graphicsSettingsService.ChangeSSAO(_root.sSAO.value);
                        break;
                }
            });

            _root.shadows.AddManipulator(new ToggleSfxManipulator(_context));
            _root.shadows.RegisterValueChangedCallback(evt =>
                graphicsSettingsService.ChangeShadow(evt.newValue));

            _root.shadows.RegisterCallback<NavigationMoveEvent>(evt =>
            {
                switch (evt.direction)
                {
                    case NavigationMoveEvent.Direction.Left:
                        _root.shadows.value = !graphicsSettingsService.Shadows;
                        graphicsSettingsService.ChangeShadow(_root.shadows.value);
                        break;
                    case NavigationMoveEvent.Direction.Right:
                        _root.shadows.value = !graphicsSettingsService.Shadows;
                        graphicsSettingsService.ChangeShadow(_root.shadows.value);
                        break;
                }
            });

            audioSourceService.OnMasterVolumeChanged += _root.masterVolume.SetValueWithoutNotify;
            _root.masterVolume.SetValueWithoutNotify(audioSourceService.MasterVolume);

            audioSourceService.OnBackgroundVolumeChanged += _root.backgroundVolume.SetValueWithoutNotify;
            _root.backgroundVolume.SetValueWithoutNotify(audioSourceService.BackgroundVolume);

            audioSourceService.OnSFXVolumeChanged += _root.sfxVolume.SetValueWithoutNotify;
            _root.sfxVolume.SetValueWithoutNotify(audioSourceService.SFXVolume);

            graphicsSettingsService.OnSsaoChange += _root.sSAO.SetValueWithoutNotify;
            _root.sSAO.SetValueWithoutNotify(graphicsSettingsService.SSAO);

            graphicsSettingsService.OnShadowChange += _root.shadows.SetValueWithoutNotify;
            _root.shadows.SetValueWithoutNotify(graphicsSettingsService.Shadows);

            graphicsSettingsService.OnDrawingDistanceChange += _root.drawingDistance.SetValueWithoutNotify;
            _root.drawingDistance.SetValueWithoutNotify(graphicsSettingsService.DrawingDistance);

            graphicsSettingsService.OnRenderScaleChange += _root.renderScale.SetValueWithoutNotify;
            _root.renderScale.SetValueWithoutNotify(graphicsSettingsService.RenderScale);
        }

        public void Show()
        {
            _rootVisualElement.DisplayFlex();
            _root.masterVolume.Focus();
        }

        public void Hide() => _rootVisualElement.DisplayNone();

        private void SetupSlider(Slider slider, Action<float, bool> change, Func<float> getValue, Action onChange)
        {
            //костыль что бы событие RegisterValueChangedCallback не перекрывало изменения навигации
            //внутри слайдера знаение шага захардкожено
            bool navigationIsActive = false;
            slider.AddManipulator(new SliderSfxManipulator(_context));
            slider.RegisterCallback<NavigationMoveEvent>(evt =>
            {
                switch (evt.direction)
                {
                    case NavigationMoveEvent.Direction.Left:
                        navigationIsActive = true;
                        change(getValue() - _step, true);
                        evt.PreventDefault();
                        evt.StopPropagation();
                        break;
                    case NavigationMoveEvent.Direction.Right:
                        navigationIsActive = true;
                        change(getValue() + _step, true);
                        evt.PreventDefault();
                        evt.StopPropagation();
                        break;
                }
            });

            slider.RegisterValueChangedCallback(evt =>
            {
                if (!navigationIsActive)
                    change(evt.newValue, true);

                navigationIsActive = false;
                onChange?.Invoke();
            });
        }
    }
}