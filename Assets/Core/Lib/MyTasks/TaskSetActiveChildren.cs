using System;
using JetBrains.Annotations;
using Reflex;
using UnityEngine;

namespace Core.Lib
{
    public class TaskSetActiveChildren : MonoBehaviour, IMyTask
    {
        [SerializeField] private bool _activeState = true;
        [CanBeNull, SerializeField] private GameObject _outsideTarget;
        // [SerializeField] private bool _changeParent;

        // [SerializeField, ShowIf(nameof(_changeParent))]
        // private Transform _parent;

        public bool InProgress => false;

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            // if (_changeParent)
            //     transform.parent = _parent;

            if (_outsideTarget)
                foreach (Transform child in _outsideTarget.transform)
                    child.gameObject.SetActive(_activeState);
            else
                foreach (Transform child in transform)
                    child.gameObject.SetActive(_activeState);

            onComplete?.Invoke(this);
        }
    }
}