using System.Linq;
using Core.Systems;
using Leopotam.EcsLite;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Core.Lib
{
    public class AddComponentsLogic : AbstractEntityLogic
    {
        [SerializeField] private BaseComponentTemplate[] _componentSO;
        private EcsWorld _world;
        private IBaseProvider[] _baseProviders;
        private BaseMonoProvider[] _baseMonoProviders;

        private void Awake()
        {
            _world = Context.Resolve<EcsWorld>();
            _baseProviders = _componentSO.Select(i => i.Build()).ToArray();
            _baseMonoProviders = GetComponents<BaseMonoProvider>();
        }

        public override void Run(int entity)
        {
            for (int i = 0, iMax = _baseProviders.Length; i < iMax; i++)
                _baseProviders[i].Attach(entity, _world, true);

            for (int i = 0, iMax = _baseMonoProviders.Length; i < iMax; i++)
                _baseMonoProviders[i].Attach(entity, _world, true);
        }
    }
}