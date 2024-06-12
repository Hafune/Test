using System.Collections.Generic;
using System.Linq;
using Core.Components;
using Core.Generated;
using Core.Lib;
using Core.Systems;
using Leopotam.EcsLite;
using Reflex;
using UnityEngine;
using VInspector;
using Voody.UniLeo.Lite;

namespace Core.ExternalEntityLogics
{
    public class EntityProjectileLauncher2D : AbstractEntityLogic
    {
        [SerializeField] private ConvertToEntity _prefab;
        [SerializeField] private SpreadPattern[] _patterns;
        [SerializeField] private float _force;
        [SerializeField] private bool _useRandomRange;

        [SerializeField, ShowIf(nameof(_useRandomRange))]
        private float _randomAngleRange;

        private Context _context;
        private IEnumerable<(Emitter emitter, BuildEntityData)> _enumerator;
        private Vector3 _startEuler;
        private ChildEntityBuilder _childEntityBuilder;
        private int _entity;
        private EcsPool<EventSetupVelocity> _eventSetupVelocityPool;

        protected override void Construct(Context context) => _context = context;

        private void Awake()
        {
            _eventSetupVelocityPool = _context.Resolve<ComponentPools>().EventSetupVelocity;
            _childEntityBuilder = new(_context);
            _startEuler = transform.localRotation.eulerAngles;
            _enumerator = _patterns.SelectMany(i => i.EmittersList
                .Select(emitter => (emitter, _childEntityBuilder
                    .BuildEvent(_prefab, emitter.transform, _entity))));
        }

        public override void Run(int entity)
        {
            _entity = entity;

            if (_useRandomRange)
                transform.localRotation =
                    Quaternion.Euler(_startEuler.x, _startEuler.y,
                        _startEuler.z - _randomAngleRange / 2 + Random.value * _randomAngleRange);

            foreach (var (emitter, data) in _enumerator)
            {
                var velocity = emitter.transform.right * _force;
                data.AddComponent(new EventSetupVelocity { velocity = velocity }, _eventSetupVelocityPool);
            }
        }
    }
}