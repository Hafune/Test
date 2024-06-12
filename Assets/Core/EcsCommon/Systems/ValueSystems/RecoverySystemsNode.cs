using Core.Components;
using Leopotam.EcsLite;

namespace Core.Systems
{
    public class RecoverySystemsNode
    {
        public IEcsSystem[] BuildSystems()
        {
            //Обновление UI
            return new IEcsSystem[]
            {
                new RecoveryValueSystem<
                    HitPointValueComponent,
                    HitPointMaxValueComponent>(),

                new RecoveryValueSystem<
                    ManaPointValueComponent,
                    ManaPointMaxValueComponent>()
            };
        }
    }
}