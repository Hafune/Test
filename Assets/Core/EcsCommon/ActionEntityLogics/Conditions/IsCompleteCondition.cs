using Core.Components;
using Leopotam.EcsLite;

namespace Core.Systems
{
    public class IsCompleteCondition : AbstractActionEntityCondition
    {
        private EcsPool<ActionCurrentComponent> _pool;

        private void Awake() => _pool = Context.Resolve<EcsWorld>().GetPool<ActionCurrentComponent>();

        public override bool Check(int entity) => _pool.Get(entity).isCompleted;
    }
}