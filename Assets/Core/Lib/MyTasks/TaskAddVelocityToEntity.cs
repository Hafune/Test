using System;
using Reflex;
using UnityEngine;
using Voody.UniLeo.Lite;
using Random = UnityEngine.Random;

namespace Core.Tasks
{
    public class TaskAddVelocityToEntity : MonoBehaviour, IMyTask
    {
        [SerializeField] private float _min;
        [SerializeField] private float _max;
        public bool InProgress { get; private set; }
        private Action<IMyTask> _onComplete;

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            payload
                .Get<ConvertToEntity>()
                .gameObject
                .GetComponent<Rigidbody>().velocity = transform.forward * Random.Range(_min, _max);
            
            onComplete?.Invoke(this);
        }
    }
}