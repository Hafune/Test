using Core.Components;
using Core.Generated;
using Core.Services;
using Lib;
using Reflex;
using UnityEngine;
using Voody.UniLeo.Lite;

namespace Core
{
    public class AbilitiesServiceClient : MonoConstruct
    {
        [SerializeField] private ConvertToEntity _convertToEntity;
        private Context _context;
        // private AbilitiesService _abilitiesService;
        //
        // private ComponentPools _pools;
        //
        protected override void Construct(Context context) => _context = context;
        //
        // private void Awake()
        // {
        //     _abilitiesService = _context.Resolve<AbilitiesService>();
        //     _pools = _context.Resolve<ComponentPools>();
        // }
        //
        // private void OnEnable()
        // {
        //     _convertToEntity.OnEntityWasConnected += RefreshEntity;
        //     _abilitiesService.OnChange += RefreshEntity;
        //     RefreshEntity(null);
        // }
        //
        // private void OnDisable()
        // {
        //     _convertToEntity.OnEntityWasConnected -= RefreshEntity;
        //     _abilitiesService.OnChange -= RefreshEntity;
        // }
        //
        // private void RefreshEntity() => RefreshEntity(null);
        //
        // private void RefreshEntity(ConvertToEntity _)
        // {
        //     if (_convertToEntity.RawEntity == -1)
        //         return;
        //
        //     var entity = _convertToEntity.RawEntity;
        //
        //     _pools.AbilitiesSlot.GetOrInitialize(entity) = _abilitiesService.CurrentSlot;
        //     _pools.EventRefreshAbilitiesSlot.AddIfNotExist(entity);
        // }
    }
}