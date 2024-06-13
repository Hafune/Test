using System;
using Reflex;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Tasks
{
    public class TaskRandomRotation : MonoBehaviour, IMyTask
    {
        [SerializeField] private Transform _target;
        
        public bool InProgress { get; private set; }

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            var rotation = _target.eulerAngles;
            rotation.y = Random.Range(0f, 360f);
            _target.eulerAngles = rotation;

            onComplete?.Invoke(this);
        }
    }
}