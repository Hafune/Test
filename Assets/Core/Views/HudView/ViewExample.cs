using Core.Views.MainMenu;

namespace Core.Views
{
    public class ViewExample : AbstractUIDocumentView
    {
        private HudVT _root;
    
        // @formatter:off
        private void Start()
        {
            _root = new HudVT(RootVisualElement);
            // var score = _root.score;
            // var firstWeaponLevel = _root.firstWeaponLevel;
            // var secondWeaponLevel = _root.secondWeaponLevel;
            // var firstWeaponName = _root.firstWeaponName;
            // var tension = _root.tension;
    
            var worldMessages = Context.Resolve<EcsEngine>().WorldMessages;
    
            // worldMessages.BuildUiEntityWithLink<Player1UniqueTag>()
            //     .Add<ScoreValueComponent>(value => score.text = value.ToString())
            //     // .Add<WeaponFirstPowerComponent>(value => firstWeaponLevel.text = value.ToString())
            //     // .Add<WeaponSecondPowerComponent>(value => secondWeaponLevel.text = value.ToString())
            //     // .Add<ActionPlayerTensionAttackComponent>(FormatUiValuesUtility.PercentInt, value => tension.text = value.ToString())
            //     .Add<AbilityPlayerTensionAttackComponent>(FormatUiValuesUtility.PercentInt, value => tension.text = value.ToString())
            //     .Add<NameValueComponent>(value => firstWeaponName.text = value.ToString());
        }
        // @formatter:on
    
        public void Show() => DisplayFlex();
    
        public void Hide() => DisplayNone();
    }
}