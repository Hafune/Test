using Core.Services;

namespace Core.Views.MainMenu
{
    public class HudView : AbstractUIDocumentView
    {
        private HudVT _root;

        protected override void Awake()
        {
            base.Awake();
            _root = new HudVT(RootVisualElement);

            var gemService = Context.Resolve<GemService>();
            gemService.OnCountChange += value => _root.gemCount.text = value.ToString();
            _root.gemCount.text = gemService.Count.ToString();

            var playerStateService = Context.Resolve<PlayerStateService>();
            playerStateService.OnShowHud += DisplayFlex;
            playerStateService.OnHideHud += DisplayNone;
        }
    }
}