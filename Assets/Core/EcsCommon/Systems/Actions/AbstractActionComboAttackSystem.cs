using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Reflex;

namespace Core.Systems
{
    //EventActionStart<E>,InProgressTag<T>,
    public abstract class AbstractActionComboAttackSystem<E, T> : AbstractActionSystem<T>, IEcsRunSystem
        where E : struct, IActionComponent
        where T : struct, IActionComponent
    {
        protected EcsFilterInject<
            Inc<
                T,
                EventActionStart<E>,
                TransformComponent,
                ActionCurrentComponent
            >,
            Exc<InProgressTag<T>>> _activateFilter;

        private readonly RotateToDesiredDirectionFunction _rotateToDesiredDirectionFunction;

        protected AbstractActionComboAttackSystem(Context context) =>
            _rotateToDesiredDirectionFunction = new(context);

        public virtual void Run(IEcsSystems systems)
        {
            foreach (var i in _activateFilter.Value)
            {
                if (!_actionPool.Value.Get(i).logic.CheckConditionLogic(i))
                    continue;

                _rotateToDesiredDirectionFunction.UpdateEntity(i);
                BeginActionProgress(i);
            }
        }
    }
}