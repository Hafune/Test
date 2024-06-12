using Core.Components;
using Leopotam.EcsLite;

namespace Core.Systems
{
    public class RecoveryActionSystemsNode
    {
        public IEcsSystem[] BuildSystems()
        {
            //Обновление UI
            return new IEcsSystem[]
            {
                // new ReloadActionSystem<ActionDashComponent>()
            };
        }
    }
}