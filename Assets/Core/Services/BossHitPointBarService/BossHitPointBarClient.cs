using Core.Components;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Core.Services
{
    public class BossHitPointBarClient : MonoConstruct
    {
        [SerializeField] private ConvertToEntity _convertToEntity;
        private BossHitPointBarService _service;
        private Context _context;
        private EcsPool<UiValue<HitPointValueComponent>> _hitPointValuePool;
        private EcsPool<UiValue<HitPointMaxValueComponent>> _hitPointMaxValuePool;

        private void OnValidate() => _convertToEntity =
            _convertToEntity ? _convertToEntity : GetComponentInParent<ConvertToEntity>();

        protected override void Construct(Context context) => _context = context;

        private void Awake()
        {
            _service = _context.Resolve<BossHitPointBarService>();
            var world = _context.Resolve<EcsWorld>();
            _hitPointValuePool = world.GetPool<UiValue<HitPointValueComponent>>();
            _hitPointMaxValuePool = world.GetPool<UiValue<HitPointMaxValueComponent>>();
        }

        private void OnEnable()
        {
            _convertToEntity.OnEntityWasConnected += Reload;
            Reload(_convertToEntity);
        }

        private void OnDisable()
        {
            _convertToEntity.OnEntityWasConnected -= Reload;
            _service.Hide();
        }

        private void Reload(ConvertToEntity _)
        {
            var entity = _convertToEntity.RawEntity;

            if (entity == -1)
                return;

            ref var value = ref _hitPointValuePool.GetOrInitialize(_convertToEntity.RawEntity);
            ref var valueMax = ref _hitPointMaxValuePool.GetOrInitialize(_convertToEntity.RawEntity);
            value.update -= UpdateValue;
            value.update += UpdateValue;
            valueMax.update -= UpdateMaxValue;
            valueMax.update += UpdateMaxValue;

            _service.Show();
        }

        private void UpdateValue<T>(T c) where T : struct, IValue => _service.ChangeValue(c.value);
        private void UpdateMaxValue<T>(T c) where T : struct, IValue => _service.ChangeValueMax(c.value);
    }
}