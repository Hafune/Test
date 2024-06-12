using Core.Services;
using Reflex;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core
{
    public class ToggleSfxManipulator : IManipulator
    {
        private readonly Context _context;
        private Toggle _target;

        public VisualElement target
        {
            get => _target;
            set
            {
                _target = (Toggle)value;
                RegisterCallbacks();
            }
        }

        public ToggleSfxManipulator(Context context) => _context = context;

        private void RegisterCallbacks()
        {
            var uiSfx = _context.Resolve<UiSfxTemplate>();
            var audioSourceService = _context.Resolve<AudioSourceService>();
            var triggerSourcePrefab = uiSfx.TriggerSourceContainer.GetComponent<AudioSource>();
            var selectSourcePrefab = uiSfx.SelectSourceContainer.GetComponent<AudioSource>();

            _target.RegisterValueChangedCallback(_ => audioSourceService.PlayOneShotUI(triggerSourcePrefab));
            _target.RegisterCallback<FocusEvent>(_ => audioSourceService.PlayOneShotUI(selectSourcePrefab));
        }
    }
}