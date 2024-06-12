using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    public class AddScoreOnDeathSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                AddScoreOnDeathValueComponent,
                EventDeath
            >> _filter;

        private readonly EcsFilterInject<Inc<PlayerUniqueTag, ScoreValueComponent>> _playerFilter;

        private readonly EcsPoolInject<AddScoreOnDeathValueComponent> _addScoreOnDeathPool;
        private readonly EcsPoolInject<ScoreValueComponent> _scoreValuePool;
        private readonly EcsPoolInject<EventValueUpdated<ScoreValueComponent>> _eventRefreshScoreValuePool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                UpdateEntity(i);
        }

        private void UpdateEntity(int entity)
        {
            var playerEntity = _playerFilter.Value.GetFirst();
            var drop = _addScoreOnDeathPool.Value.Get(entity);
            _scoreValuePool.Value.Get(playerEntity).value += drop.value;
            _eventRefreshScoreValuePool.Value.AddIfNotExist(playerEntity);
        }
    }
}