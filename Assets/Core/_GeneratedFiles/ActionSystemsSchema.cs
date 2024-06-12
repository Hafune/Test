//Файл генерируется в GenActionSystemsSchema
using System;
using Core.Components;
using Core.Systems;
using Leopotam.EcsLite;

namespace Core.Generated
{
    // @formatter:off
    public class ActionSystemsSchema
    {
        private readonly EcsPool<EventActionStart<ActionDeathComponent>> ActionDeathComponentPool;
        private readonly EcsPool<EventActionStart<ActionIdleComponent>> ActionIdleComponentPool;
        private readonly EcsPool<EventActionStart<ActionMoveComponent>> ActionMoveComponentPool;
        private readonly EcsPool<EventActionStart<ActionReviveComponent>> ActionReviveComponentPool;
        private readonly EcsPool<EventActionStart<NpcActionComponent>> NpcActionComponentPool;
        
        public ActionSystemsSchema(EcsWorld world)
        {
            ActionDeathComponentPool = world.GetPool<EventActionStart<ActionDeathComponent>>();
            ActionIdleComponentPool = world.GetPool<EventActionStart<ActionIdleComponent>>();
            ActionMoveComponentPool = world.GetPool<EventActionStart<ActionMoveComponent>>();
            ActionReviveComponentPool = world.GetPool<EventActionStart<ActionReviveComponent>>();
            NpcActionComponentPool = world.GetPool<EventActionStart<NpcActionComponent>>();
        }
        
        public void AddEventActionStart(ActionEnum actionEnum, int entity)
        {
            switch (actionEnum)
            {
                case ActionEnum.ActionDeathComponent: ActionDeathComponentPool.Add(entity); break;
                case ActionEnum.ActionIdleComponent: ActionIdleComponentPool.Add(entity); break;
                case ActionEnum.ActionMoveComponent: ActionMoveComponentPool.Add(entity); break;
                case ActionEnum.ActionReviveComponent: ActionReviveComponentPool.Add(entity); break;
                case ActionEnum.NpcActionComponent: NpcActionComponentPool.Add(entity); break;
            }
        }

        public static ActionEnum Get<T>(T c) where T : IActionComponent =>  
            c switch 
            {
                ActionDeathComponent => ActionEnum.ActionDeathComponent,
                ActionIdleComponent => ActionEnum.ActionIdleComponent,
                ActionMoveComponent => ActionEnum.ActionMoveComponent,
                ActionReviveComponent => ActionEnum.ActionReviveComponent,
                NpcActionComponent => ActionEnum.NpcActionComponent,
                _ => throw new ArgumentOutOfRangeException()
            };
    }
    // @formatter:on
}