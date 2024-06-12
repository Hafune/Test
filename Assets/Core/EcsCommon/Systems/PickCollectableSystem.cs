using Core.Components;
using Core.Generated;
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

        private readonly EcsFilterInject<Inc<ItemsStackValueComponent, TransformComponent>> _itemsStackFilter;
        private readonly EcsFilterInject<Inc<CollectableGemComponent>> _collectableGemFilter;
        private readonly EcsFilterInject<Inc<CollectableKeySilverComponent>> _collectableKeySilverFilter;
        private readonly EcsFilterInject<Inc<CollectableKeyGoldComponent>> _collectableKeyGoldFilter;

        private readonly EcsFilterInject<
            Inc<
                PlayerUniqueTag
            >,
            Exc<InProgressTag<ActionDeathComponent>>> _playerFilter;

        private readonly ComponentPools _pools;
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
            if (_itemsStackFilter.Value.HasEntity(entity))
            {
                var playerEntity = _playerFilter.Value.GetFirst();
                var stack = _pools.ItemsStackValue.Get(playerEntity);
                stack.items.Add(_pools.Transform.Get(entity).convertToEntity.TemplateId);
                stack.itemsStackClient.Update(stack.items);
            }
            else if (_collectableGemFilter.Value.HasEntity(entity))
            {
                var gem = _pools.CollectableGem.Get(entity);
                _gemService.TryChangeValue(gem.count);
                _gemService.AddCollected(gem.instance.InstanceUuid);
            }
            else if (_collectableKeySilverFilter.Value.HasEntity(entity))
            {
                var key = _pools.CollectableKeySilver.Get(entity);
                _keySilverService.TryChangeValue(1);
                _keySilverService.AddCollected(key.instance.InstanceUuid);
            }
            else if (_collectableKeyGoldFilter.Value.HasEntity(entity))
            {
                var key = _pools.CollectableKeyGold.Get(entity);
                _keyGoldService.TryChangeValue(1);
                _keyGoldService.AddCollected(key.instance.InstanceUuid);
            }

            _pools.EventRemoveEntity.AddIfNotExist(entity);
        }
    }
}