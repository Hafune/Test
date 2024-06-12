using Core.Services;
using UnityEngine.UIElements;

namespace Core.Views.MainMenu
{
    public class BossHitPointBarView : AbstractUIDocumentView
    {
        private BossHitPointBarVT _root;
        private BossHitPointBarService _bossBarService;
        private ProgressBar _bar;

        protected override void Awake()
        {
            base.Awake();
            _root = new BossHitPointBarVT(RootVisualElement);
            _bar = _root.bar;
        }

        private void Start()
        {
            _bossBarService = Context.Resolve<BossHitPointBarService>();
            _bossBarService.OnShow += Show;
            _bossBarService.OnHide += Hide;
        }

        private void Show()
        {
            UpdateBar();
            DisplayFlex();
            _bossBarService.OnChange += UpdateBar;
        }

        private void Hide()
        {
            _bossBarService.OnChange -= UpdateBar;
            DisplayNone();
        }

        private void UpdateBar()
        {
            _bar.highValue = _bossBarService.ValueMax;
            _bar.value = _bossBarService.Value;
        }
    }
}