using BehaviorDesigner.Runtime.Tasks;
using Core.Components;
using Leopotam.EcsLite;
using Lib;

namespace Core.BehaviorTree
{
    [TaskCategory("Actions")]
    public class SelectEnemyTarget : AbstractEntityAction
    {
        private EcsPool<EventSelectEnemyTarget> _eventSelectEnemyTargetPool;

        public override void OnAwake()
        {
            base.OnAwake();
            _eventSelectEnemyTargetPool = World.GetPool<EventSelectEnemyTarget>();
        }

        public override TaskStatus OnUpdate()
        {
            _eventSelectEnemyTargetPool.AddIfNotExist(RawEntity);
            return TaskStatus.Success;
        }
    }
}