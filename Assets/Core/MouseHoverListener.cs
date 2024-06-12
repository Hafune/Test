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
    public class MouseHoverListener : MonoConstruct
    {
        [SerializeField] private PlayerMouseDetector playerMouseDetector;
        [SerializeField] private ConvertToEntity _convertToEntity;
        private Context _context;
        private EcsPool<MouseHoverTag> _mouseHoverPool;

        private void OnValidate()
        {
            playerMouseDetector = GetComponent<PlayerMouseDetector>();
            _convertToEntity = GetComponent<ConvertToEntity>();
        }

        protected override void Construct(Context context) => _context = context;

        private void Awake()
        {
            playerMouseDetector.PointerEnter += PointerEnter;
            playerMouseDetector.PointerExit += PointerExit;
        }

        private void Start()
        {
            _mouseHoverPool = _context.Resolve<EcsEngine>().World.GetPool<MouseHoverTag>();
        }

        private void PointerEnter(PointerEventData _)
        {
            if (_convertToEntity.RawEntity > -1)
                _mouseHoverPool.AddIfNotExist(_convertToEntity.RawEntity);
        }

        private void PointerExit(PointerEventData _)
        {
            if (_convertToEntity.RawEntity > -1)
                _mouseHoverPool.DelIfExist(_convertToEntity.RawEntity);
        }
    }
}