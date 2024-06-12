using Core.Systems;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ClampVelocityXLogic : AbstractEntityLogic
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _maxXForce;

        private void OnValidate() => _rigidbody = _rigidbody ? _rigidbody : GetComponentInParent<Rigidbody2D>();

        public override void Run(int entity)
        {
            var velocity = _rigidbody.velocity;
            _rigidbody.velocity = new Vector2(Mathf.Clamp(velocity.x, -_maxXForce, _maxXForce), velocity.y);
        }
    }
}