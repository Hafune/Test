using System;
using Reflex;
using UnityEngine;

namespace Core.Lib
{
    public class TaskChangeParent : MonoBehaviour, IMyTask
    {
        [SerializeField] private Transform _nextParent;
        [SerializeField] private Transform _target;

        public bool InProgress => false;

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            _target.parent = _nextParent;
            onComplete?.Invoke(this);
        }
    }
}