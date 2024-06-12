using Core.Services;
using Reflex;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core
{
    public class SliderSfxManipulator : IManipulator
    {
        private readonly Context _context;
        private Slider _target;

        public VisualElement target
        {
            get => _target;
            set
            {
                _target = (Slider)value;
                RegisterCallbacks();
            }
        }

        public SliderSfxManipulator(Context context) => _context = context;

        private void RegisterCallbacks()
        {
            var uiSfx = _context.Resolve<UiSfxTemplate>();
            var audioSourceService = _context.Resolve<AudioSourceService>();
            var selectSourcePrefab = uiSfx.SelectSourceContainer.GetComponent<AudioSource>();

            _target.RegisterCallback<FocusEvent>(_ => audioSourceService.PlayOneShotUI(selectSourcePrefab));
        }
    }
}