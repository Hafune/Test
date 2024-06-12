using System;
using Core.Components;
using Core.Generated;
using Core.Systems;
using Leopotam.EcsLite;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Lib
{
    public class TaskAddStartActionEventToPlayer : MonoBehaviour, IMyTask
    {
        [SerializeField] private ActionEnum _actionEnum;
        public bool InProgress => false;

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            context
                .Resolve<ActionSystemsService>()
                .AddStartEvent(_actionEnum, context.Resolve<EcsWorld>().Filter<PlayerUniqueTag>().End().GetFirst(),
                    true);

            onComplete?.Invoke(this);
        }
    }
}