using Lib;
using Reflex;
using UnityEngine;

namespace Core.Services
{
    public class DifficultScaleClient : MonoConstruct
    {
        [SerializeField, Min(1)] private float _scale = 1;
        private Context _context;

        protected override void Construct(Context context) => _context = context;

        private void Start() => _context.Resolve<DifficultScaleService>().SetSceneScale(_scale);
    }
}