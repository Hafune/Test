using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Lib;

namespace Core.Systems
{
    //Система даёт управлять мобом в порядке теста
    public class DebugEnemySystem : IEcsRunSystem
    {
        private EcsWorldInject _world;

        private readonly EcsFilterInject<Inc<EventInit, DebugEnemyTag>> _hasInitFilter;

        //add
        private readonly EcsPoolInject<PlayerControllerTag> _playerControllerTagPool;
        private readonly EcsPoolInject<PlayerInputMemoryComponent> _playerInputMemoryComponentPool;
        private readonly EcsPoolInject<PlayerUniqueTag> _playerUniqueTagPool;
        private readonly EcsPoolInject<EventVirtualCameraFollowSetup> _eventVirtualCameraFollowSetupPool;

        //del
        private readonly EcsPoolInject<BehaviorTreeComponent> _behaviorTreeComponentPool;
        private readonly EcsPoolInject<BehaviourActivateAreaComponent> _behaviourActivateAreaComponentPool;
        private readonly EcsPoolInject<EnemyTag> _enemyTagPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _hasInitFilter.Value)
            {
                //add
                _playerControllerTagPool.Value.AddIfNotExist(i);
                _playerInputMemoryComponentPool.Value.AddIfNotExist(i);
                _playerUniqueTagPool.Value.AddIfNotExist(i);
                _eventVirtualCameraFollowSetupPool.Value.AddIfNotExist(i);

                //del
                _behaviorTreeComponentPool.Value.DelIfExist(i);
                _behaviourActivateAreaComponentPool.Value.DelIfExist(i);
                _enemyTagPool.Value.DelIfExist(i);
            }
        }
    }
}