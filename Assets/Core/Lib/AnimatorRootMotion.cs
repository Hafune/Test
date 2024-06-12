using Lib;
using UnityEngine;

namespace Core.Lib
{
    public class AnimatorRootMotion : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private bool _useLastVelocity;

        private Vector2 _deltaPosition;
        private Vector2 _lastVelocity;
        private Vector2 _preLastVelocity;
        private bool _forwardMoveIsDisabled;

        private void OnValidate()
        {
            _animator = _animator ? _animator : GetComponent<Animator>();
            _rigidbody = _rigidbody ? _rigidbody : GetComponent<Rigidbody2D>();
        }

        private void Awake() => _animator.applyRootMotion = false;

        private void OnEnable() => _animator.applyRootMotion = true;

        private void OnDisable()
        {
            _animator.applyRootMotion = false;

            if (_useLastVelocity)
                _rigidbody.velocity = _preLastVelocity;
        }

        public void DisableForwardMove() => _forwardMoveIsDisabled = true;

        public void EnableForwardMove() => _forwardMoveIsDisabled = false;

        private void OnAnimatorMove()
        {
            if (!enabled)
                return;

            Vector2 delta = _animator.deltaPosition;

            if (_forwardMoveIsDisabled && transform.right.x.Sign() == delta.x.Sign())
                delta.x = 0;

            _deltaPosition += delta;
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + _deltaPosition);
            _preLastVelocity = _lastVelocity;
            _lastVelocity = _deltaPosition / Time.deltaTime;
            _deltaPosition = Vector2.zero;
        }
    }
}