using System;
using Reflex;
using UnityEngine;

namespace Core.Lib
{
    public class TaskSetEnable : MonoBehaviour, IMyTask
    {
        [SerializeField] private Behaviour _behaviour;
        [SerializeField] private bool _enabled;

        public bool InProgress => false;

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            _behaviour.enabled = _enabled;
            onComplete?.Invoke(this);
        }
    }
}