using System;
using Reflex;
using UnityEngine;

namespace Core.Lib
{
    public class TaskDestroyGameObject : MonoBehaviour, IMyTask
    {
        [SerializeField] private GameObject _target;
        public bool InProgress { get; }

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            Destroy(_target);
            onComplete?.Invoke(this);
        }
    }
}