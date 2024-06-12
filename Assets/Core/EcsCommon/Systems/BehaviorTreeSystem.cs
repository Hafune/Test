using BehaviorDesigner.Runtime;
using Core.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Reflex;
using UnityEngine;

namespace Core.Systems
{
    public class BehaviorTreeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<
            Inc<
                BehaviorTreeComponent,
                InProgressTag<BehaviorTreeComponent>
            >,
            Exc<InProgressTag<ActionDeathComponent>>> _filter;

        private readonly EcsFilterInject<
            Inc<
                BehaviorTreeComponent,
                BehaviourActivateAreaComponent,
                PlayerInRangeTag,
                AnimatorComponent
            >,
            Exc<
                InProgressTag<BehaviorTreeComponent>,
                InProgressTag<ActionDeathComponent>
            >> _activateFilter;

        private readonly EcsFilterInject<
            Inc<
                InProgressTag<BehaviorTreeComponent>,
                BehaviourActivateAreaComponent,
                ActionCurrentComponent,
                AnimatorComponent
            >,
            Exc<
                PlayerInRangeTag
            >> _deactivateFilter;

        private readonly EcsFilterInject<
            Inc<
                BehaviorTreeComponent,
                EventBehaviorTreeActivate
            >,
            Exc<
                InProgressTag<BehaviorTreeComponent>,
                InProgressTag<ActionDeathComponent>
            >> _activateManuallyFilter;
        
        private readonly EcsFilterInject<
            Inc<
                InProgressTag<BehaviorTreeComponent>,
                ActionCurrentComponent,
                AnimatorComponent,
                EventBehaviorTreeDeactivate
            >> _cancelManuallyFilter;
        
        private readonly EcsFilterInject<Inc<EventBehaviorTreeActivate>> _eventBehaviorTreeActivateFilter;
        private readonly EcsFilterInject<Inc<EventBehaviorTreeDeactivate>> _eventBehaviorTreeDeactivateFilter;

        private readonly EcsPoolInject<ActionCurrentComponent> _actionCurrentPool;
        private readonly EcsPoolInject<AnimatorComponent> _animatorPool;
        private readonly EcsPoolInject<BehaviourActivateAreaComponent> _behaviourActivateAreaPool;
        private readonly EcsPoolInject<BehaviorTreeComponent> _behaviorTreePool;
        private readonly EcsPoolInject<EventBehaviorTreeActivate> _eventBehaviorTreeActivatePool;
        private readonly EcsPoolInject<EventBehaviorTreeDeactivate> _eventBehaviorTreeDeactivatePool;
        private readonly EcsPoolInject<InProgressTag<BehaviorTreeComponent>> _inProgressPool;
        private readonly BehaviorManager _behaviorManager;

        public BehaviorTreeSystem(Context context) => _behaviorManager = context.Resolve<BehaviorManager>();

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _filter.Value)
                _behaviorManager.Tick(_behaviorTreePool.Value.Get(i).tree);
            
            foreach (var i in _activateFilter.Value)
            {
                _inProgressPool.Value.Add(i);
                _animatorPool.Value.Get(i).animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
                var activateArea = _behaviourActivateAreaPool.Value.Get(i);
                activateArea.collider.radius = activateArea.baseRadius * 10;
                _behaviorTreePool.Value.Get(i).tree.EnableBehavior();
            }
            
            foreach (var i in _activateManuallyFilter.Value)
            {
                _inProgressPool.Value.Add(i);
                _animatorPool.Value.Get(i).animator.cullingMode = AnimatorCullingMode.AlwaysAnimate;
                _behaviorTreePool.Value.Get(i).tree.EnableBehavior();
            }
            
            foreach (var i in _deactivateFilter.Value)
            {
                _inProgressPool.Value.Del(i);
                _behaviorTreePool.Value.Get(i).tree.DisableBehavior();
                _actionCurrentPool.Value.Get(i).isCompleted = true;
                
                var activateArea = _behaviourActivateAreaPool.Value.Get(i);
                activateArea.collider.radius = activateArea.baseRadius;
                _animatorPool.Value.Get(i).animator.cullingMode = AnimatorCullingMode.CullCompletely;
            }
            
            foreach (var i in _cancelManuallyFilter.Value)
            {
                _inProgressPool.Value.Del(i);
                _behaviorTreePool.Value.Get(i).tree.DisableBehavior();
                _actionCurrentPool.Value.Get(i).isCompleted = true;
                _animatorPool.Value.Get(i).animator.cullingMode = AnimatorCullingMode.CullCompletely;
            }

            foreach (var i in _eventBehaviorTreeActivateFilter.Value)
                _eventBehaviorTreeActivatePool.Value.Del(i);

            foreach (var i in _eventBehaviorTreeDeactivateFilter.Value)
                _eventBehaviorTreeDeactivatePool.Value.Del(i);
        }
    }
}