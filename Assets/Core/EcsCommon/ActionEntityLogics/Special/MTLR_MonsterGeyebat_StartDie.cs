using Core.Components;
using Core.Systems;
using Leopotam.EcsLite;

namespace Core.ExternalEntityLogics
{
    public class MTLR_MonsterGeyebat_StartDie : AbstractEntityLogic
    {
        private EcsPool<EventActionStart<ActionDeathComponent>> _eventStartActionDiePool;

        private void Awake() => _eventStartActionDiePool =
            Context.Resolve<EcsWorld>().GetPool<EventActionStart<ActionDeathComponent>>();

        public override void Run(int entity) => _eventStartActionDiePool.Add(entity);
    }
}