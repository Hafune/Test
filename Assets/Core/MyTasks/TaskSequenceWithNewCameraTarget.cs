using System;
using Cinemachine;
using Reflex;
using UnityEngine;

namespace Core.Lib
{
    public class TaskSequenceWithNewCameraTarget : TaskSequence
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;
        private Action<IMyTask> _onComplete;
        private CinemachineVirtualCamera _virtualCamera;
        private Transform _oldTarget;
        private Vector3 _oldOffset;
        private CinemachineFramingTransposer _transposer;

        private void OnValidate() => _target = _target ? _target : transform;

        public override void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            _virtualCamera = context.Resolve<CinemachineVirtualCamera>();
            _oldTarget = _virtualCamera.Follow;
            _transposer = _virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            _oldOffset = _transposer.m_TrackedObjectOffset;
            _virtualCamera.Follow = _target;
            _transposer.m_TrackedObjectOffset = _offset;

            _onComplete = onComplete;
            base.Begin(context, payload, Callback);
        }

        private void Callback(IMyTask myTask)
        {
            _virtualCamera.Follow = _oldTarget;
            _virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = _oldOffset;
            _onComplete?.Invoke(this);
        }
    }
}