using Core.Systems;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class LookOnTargetLogic : AbstractEntityLogic
    {
        [SerializeField] private float _maxAngleDifference;
        private Transform _target;
        private Vector3 _startLocalEulerAngles;

        private void OnTriggerEnter2D(Collider2D col) => _target = col.transform;

        private void Awake() => _startLocalEulerAngles = transform.localEulerAngles;

        public override void Run(int entity)
        {
            transform.localEulerAngles = _startLocalEulerAngles;
            var targetPosition = (Vector2)_target.position;
            var position = (Vector2)transform.position;
            var right = (Vector2)transform.right;

            var totalRight = right.RotatedToward(targetPosition - position, _maxAngleDifference);
            transform.right = totalRight;
        }
    }
}