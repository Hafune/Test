using Core.Services;
using Reflex;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core
{
    public class InventoryItemSfxManipulator : IManipulator
    {
        private readonly Context _context;
        private VisualElement _target;

        public VisualElement target
        {
            get => _target;
            set
            {
                _target = value;
                RegisterCallbacks();
            }
        }

        public InventoryItemSfxManipulator(Context context) => _context = context;

        private void RegisterCallbacks()
        {
            var uiSfx = _context.Resolve<UiSfxTemplate>();
            var audioSourceService = _context.Resolve<AudioSourceService>();
            var triggerSourcePrefab = uiSfx.TriggerSourceContainer.GetComponent<AudioSource>();
            var selectSourcePrefab = uiSfx.SelectSourceContainer.GetComponent<AudioSource>();
            
            _target.RegisterCallback<NavigationSubmitEvent>(_ => audioSourceService.PlayOneShotUI(triggerSourcePrefab));
            _target.RegisterCallback<ClickEvent>(_ => audioSourceService.PlayOneShotUI(triggerSourcePrefab));
            _target.RegisterCallback<FocusEvent>(_ => audioSourceService.PlayOneShotUI(selectSourcePrefab));
        }
    }
}