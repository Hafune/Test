using System;
using Reflex;
using UnityEngine;

namespace Core.Lib
{
    public class TaskOutside : MonoBehaviour, IMyTask
    {
        [SerializeField] private GameObject _outsideTask;

        public bool InProgress => false;

        private void OnValidate()
        {
            if (_outsideTask?.GetComponent<IMyTask>() is null)
                _outsideTask = null;
        }

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
            =>
                _outsideTask.GetComponent<IMyTask>().Begin(context, payload, onComplete);
    }
}