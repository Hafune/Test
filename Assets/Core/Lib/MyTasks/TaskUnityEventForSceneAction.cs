using System;
using Reflex;
using UnityEngine;
using UnityEngine.Events;

namespace Core.Tasks
{
    [Obsolete(@"Использовать только для одноразовой логики (пример туториалы, катсцены)
Не использовать для кор логики!!! 
")]
    public class TaskUnityEventForSceneAction : MonoBehaviour, IMyTask
    {
        [SerializeField] private UnityEvent _event;

        public bool InProgress => false;

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            _event.Invoke();
            onComplete?.Invoke(this);
        }
    }
}