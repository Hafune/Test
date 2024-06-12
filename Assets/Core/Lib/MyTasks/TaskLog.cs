using System;
using Lib;
using Reflex;
using UnityEngine;
using VInspector;

namespace Core.Lib
{
    public class TaskLog : MonoBehaviour, IMyTask
    {
        [SerializeField] private string _message;

        public bool InProgress => false;

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            Debug.Log(_message);
            onComplete?.Invoke(this);
        }
    }
}