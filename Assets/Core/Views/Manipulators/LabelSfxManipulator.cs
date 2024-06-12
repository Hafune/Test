using Core.Services;
using Reflex;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core
{
    public class LabelSfxManipulator : IManipulator
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

        public LabelSfxManipulator(Context context) => _context = context;

        private void RegisterCallbacks()
        {
            var uiSfx = _context.Resolve<UiSfxTemplate>();
            var audioSourceService = _context.Resolve<AudioSourceService>();
            var submitSourcePrefab = uiSfx.SubmitSourceContainer.GetComponent<AudioSource>();
            var selectSourcePrefab = uiSfx.SelectSourceContainer.GetComponent<AudioSource>();
            
            _target.RegisterCallback<NavigationSubmitEvent>(_ => audioSourceService.PlayOneShotUI(submitSourcePrefab));
            _target.RegisterCallback<ClickEvent>(_ => audioSourceService.PlayOneShotUI(submitSourcePrefab));
            _target.RegisterCallback<FocusEvent>(_ => audioSourceService.PlayOneShotUI(selectSourcePrefab));
        }
    }
}