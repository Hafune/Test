using Core.Components;
using Leopotam.EcsLite;

namespace Core.Systems
{
    public class ActionCompleteLogic : AbstractEntityLogic
    {
        private EcsPool<ActionCurrentComponent> _pool;
        private void Awake() => _pool = Context.Resolve<EcsWorld>().GetPool<ActionCurrentComponent>();

        public override void Run(int entity) => _pool.Get(entity).isCompleted = true;
    }
}