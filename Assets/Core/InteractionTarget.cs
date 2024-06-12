using Core.Components;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using UnityEngine;
using UnityEngine.EventSystems;
using Voody.UniLeo.Lite;

namespace Core
{
    [DisallowMultipleComponent, RequireComponent(typeof(ConvertToEntity), typeof(PlayerMouseDetector))]
    public class InteractionTarget : MonoConstruct
    {
        [SerializeField] private PlayerMouseDetector _playerMouseDetector;
        [SerializeField] private ConvertToEntity _convertToEntity;
        private Context _context;
        private EcsPool<EventInteractionTargetClick> _interactionTargetClickPool;

        private void OnValidate()
        {
            _playerMouseDetector = GetComponent<PlayerMouseDetector>();
            _convertToEntity = GetComponent<ConvertToEntity>();
        }

        protected override void Construct(Context context) => _context = context;

        private void Awake()
        {
            var ecsEngine = _context.Resolve<EcsEngine>();
            _interactionTargetClickPool = ecsEngine.World.GetPool<EventInteractionTargetClick>();
            _playerMouseDetector.PointerDown += SetupEntityAsTarget;
        }

        private void SetupEntityAsTarget(PointerEventData _) => _interactionTargetClickPool.Add(_convertToEntity.RawEntity);
    }
}

