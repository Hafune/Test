using System;
using JetBrains.Annotations;
using Reflex;
using UnityEngine;

namespace Core.Tasks
{
    public class TaskSetActive : MonoBehaviour, IMyTask
    {
        [SerializeField] private bool _activeState = true;
        [CanBeNull, SerializeField] private GameObject _outsideTarget;

        public bool InProgress => false;

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            if (_outsideTarget)
                _outsideTarget.SetActive(_activeState);
            else
                gameObject.SetActive(_activeState);

            onComplete?.Invoke(this);
        }
    }
}