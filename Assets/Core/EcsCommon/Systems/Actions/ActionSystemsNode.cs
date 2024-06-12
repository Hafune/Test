using System.Collections.Generic;
using Core.Components;
using Core.Generated;
using Leopotam.EcsLite;
using Reflex;

namespace Core.Systems
{
    public class ActionSystemsNode
    {
        public IEnumerable<IEcsSystem> BuildSystems(Context context)
        {
            var systems = new List<IEcsSystem>();

            //Системы экшенов
            //

            systems.AddRange(new IEcsSystem[]
            {
                //Срабатывание timeline события.
                new EventTimelineActionSystem(),
                //
                new PlaybackAndRecordingActionsSystem(),
                new ActionDeathSystem(),

                new ActionReviveSystem(),

                new NpcActionSystem(),
                new ActionMoveSystem(),
                new ActionIdleSystem(),
                //-----------
                //Система BTree проверяющая что нужный экшен не начался
                new EventBehaviorTreeActionFailCheckSystem(),
                //-----------
                new DelHere<
                    EventActionComplete>(),
                new DelHere<
                    EventTimelineAction,
                    EventLanding,
                    EventDeTouchWall>(),
                //система для оповещения о старте экшена или о провале старта
                // new DispatchActionStatusSystem(),
            });
            return systems;
        }
    }
}