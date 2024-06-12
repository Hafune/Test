using JetBrains.Annotations;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.Lib
{
    public class Trigger : MonoConstruct, ITrigger
    {
        [SerializeField] private bool _reusable;
        [CanBeNull, SerializeField] private MonoBehaviour _task;
        private bool _isCompleted;
        private Context _context;
        private Payload _payload;

        private void OnValidate()
        {
            if (_task is not IMyTask)
                _task = null;

            _task = _task ? _task : (MonoBehaviour)GetComponentInChildren<IMyTask>();

            if (_task == this)
                _task = null;
        }

        protected override void Construct(Context context) => _context = context;

        private void OnEnable() => _isCompleted = false;

        private void OnTriggerEnter2D(Collider2D _)
        {
            if (_isCompleted)
                return;

            _isCompleted = !_reusable;

            if (_task)
                (_task as IMyTask)?.Begin(_context, _payload = Payload.GetPooled(), OnComplete);
        }

        private void OnDestroy() => _payload?.Dispose();

        private void OnComplete(IMyTask task) => _payload?.Dispose();
    }
}