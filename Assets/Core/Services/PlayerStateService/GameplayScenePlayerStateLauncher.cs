using Core.Services;
using Lib;
using Reflex;

namespace Core.Views
{
    public class GameplayScenePlayerStateLauncher : MonoConstruct
    {
        private Context _context;

        protected override void Construct(Context context) => _context = context;

        private void Start() => _context.Resolve<PlayerStateService>().EnableState();
    }
}