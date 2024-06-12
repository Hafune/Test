using Core.Components;
using Core.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;
using Reflex;

namespace Core.Systems
{
    public class PickCollectableSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ActiveArea<CollectableAreaComponent>>> _filter;

        private readonly EcsFilterInject<Inc<CollectableGemComponent>> _collectableGemFilter;
        private readonly EcsFilterInject<Inc<CollectableKeySilverComponent>> _collectableKeySilverFilter;
        private readonly EcsFilterInject<Inc<CollectableKeyGoldComponent>> _collectableKeyGoldFilter;

        // private readonly EcsFilterInject<
        //     Inc<
        //         PlayerUniqueTag
        //     >,
        //     Exc<InProgressTag<ActionDeathComponent>>> _playerFilter;

        private readonly EcsPoolInject<CollectableGemComponent> _collectableGemPool;
        private readonly EcsPoolInject<CollectableKeySilverComponent> _collectableKeySilverPool;
        private readonly EcsPoolInject<CollectableKeyGoldComponent> _collectableKeyGoldPool;

        private readonly EcsPoolInject<EventRemoveEntity> _eventRemoveEntityPool;

        private readonly GemService _gemService;
        private readonly KeySilverService _keySilverService;
        private readonly KeyGoldService _keyGoldService;

        public PickCollectableSystem(Context _context)
        {
            _gemService = _context.Resolve<GemService>();
            _keySilverService = _context.Resolve<KeySilverService>();
            _keyGoldService = _context.Resolve<KeyGoldService>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                UpdateEntity(i);
        }

        private void UpdateEntity(int entity)
        {
            // var playerEntity = _playerFilter.Value.GetFirst();

            if (_collectableGemFilter.Value.HasEntity(entity))
            {
                var gem = _collectableGemPool.Value.Get(entity);
                _gemService.TryChangeValue(gem.count);
                _gemService.AddCollected(gem.instance.InstanceUuid);
            }
            else if (_collectableKeySilverFilter.Value.HasEntity(entity))
            {
                var key = _collectableKeySilverPool.Value.Get(entity);
                _keySilverService.TryChangeValue(1);
                _keySilverService.AddCollected(key.instance.InstanceUuid);
            }
            else if (_collectableKeyGoldFilter.Value.HasEntity(entity))
            {
                var key = _collectableKeyGoldPool.Value.Get(entity);
                _keyGoldService.TryChangeValue(1);
                _keyGoldService.AddCollected(key.instance.InstanceUuid);
            }

            _eventRemoveEntityPool.Value.AddIfNotExist(entity);
        }
    }
}