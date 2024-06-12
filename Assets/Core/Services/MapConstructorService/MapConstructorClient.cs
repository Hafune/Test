using Lib;
using Reflex;

namespace Core
{
    public class MapConstructorClient : MonoConstruct
    {
        private Context _context;
        private MapConstructorService _mapConstructorService;

        protected override void Construct(Context context) => _context = context;

        private void Awake() => _context.Resolve<MapConstructorService>();

        private void Start() => _context.Resolve<MapConstructorService>().MapCreated(default);
    }
}