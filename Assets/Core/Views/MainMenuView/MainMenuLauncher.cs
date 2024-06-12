#if YANDEX_GAMES
using Agava.YandexGames;
#endif 
using Lib;
using Reflex;

namespace Core.Views
{
    public class MainMenuLauncher : MonoConstruct
    {
        private Context _context;

        protected override void Construct(Context context) => _context = context;

        private void Start()
        {
#if YANDEX_GAMES
            YandexGamesSdk.GameReady();
#endif 
            _context.Resolve<MainMenuView>().EnableState();
        }
    }
}