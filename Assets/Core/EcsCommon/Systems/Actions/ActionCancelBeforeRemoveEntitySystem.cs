using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Unity.VisualScripting;

namespace Core.Systems
{
    public class ActionCancelBeforeRemoveEntitySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                TransformComponent,
                ActionCurrentComponent,
                EventRemoveEntity
            >> _filter;

        private readonly EcsPoolInject<TransformComponent> _transformPool;
        private readonly EcsPoolInject<ActionCurrentComponent> _actionCurrentPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                if (!_transformPool.Value.Get(i).transform.IsDestroyed())
                    _actionCurrentPool.Value.Get(i).currentAction?.Cancel(i);
        }
    }
}