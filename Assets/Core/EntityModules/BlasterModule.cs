using Core.Components;
using Core.ExternalEntityLogics;
using Core.Generated;
using Core.Systems;
using Reflex;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Core.EntityModules
{
    public class BlasterModule : AbstractEntityModule
    {
        [SerializeField] private ConvertToEntity _prefab;
        [SerializeField] private ProjectileEmitters2D _emitters;

        private Context _context;
        private ComponentPools _pools;
        private float _shotDelay = .25f;
        private float _currentDelay;
        private int _entity;
        private ChildEntityBuilder _childEntityBuilder;

        protected override void Construct(Context context) => _context = context;

        private void Awake()
        {
            _pools = _context.Resolve<ComponentPools>();
            _childEntityBuilder = new(_context);
        }

        public override void StartLogic(int entity) => _currentDelay = 0;

        public override void UpdateLogic(int entity)
        {
            if ((_currentDelay -= Time.deltaTime * _pools.AttackSpeedValue.Get(entity).value) > 0)
                return;

            _currentDelay += _shotDelay;
            _entity = entity;

            _emitters.ForEachEmitter(LaunchOne);
        }

        private void LaunchOne(Transform emitter)
        {
            var data = _childEntityBuilder.BuildEvent(_prefab, emitter, _entity);
            var velocity = emitter.right * 60;

            data.AddComponent(new EventSetupVelocity { velocity = velocity }, _pools.EventSetupVelocity);
        }
    }
}