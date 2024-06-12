using System;
using System.Collections;
using Core.Components;
using Leopotam.EcsLite;
using Reflex;
using UnityEngine;

namespace Core.Tasks
{
    public class TaskWaitPlayerReady : MonoBehaviour, IMyTask
    {
        public bool InProgress { get; private set; }

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
            => StartCoroutine(StartSpawn(context, onComplete));

        private IEnumerator StartSpawn(Context context, Action<IMyTask> onComplete)
        {
            var filter = context.Resolve<EcsWorld>().Filter<PlayerUniqueTag>().Exc<EventInit>().End();
            InProgress = true;

            while (filter.GetEntitiesCount() == 0)
                yield return null;

            InProgress = false;

            onComplete?.Invoke(this);
        }
    }
}