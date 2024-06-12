using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class MovingPlatform : MonoBehaviour
    {
        private Vector2 _lastPosition;
        private HashSet<Rigidbody2D> _contacts = new();

        private void OnEnable() => _lastPosition = transform.position;

        private void OnCollisionEnter2D(Collision2D col)
        {
            _contacts.Add(col.rigidbody);
            enabled = true;
        }

        private void OnCollisionExit2D(Collision2D col)
        {
            _contacts.Remove(col.rigidbody);
            enabled = _contacts.Count > 0;
        }

        private void FixedUpdate()
        {
            Vector2 currentPosition = transform.position;
            Vector2 movement = currentPosition - _lastPosition;

            foreach (var body in _contacts)
            {
                body.velocity -= Physics2D.gravity * Time.deltaTime;
                body.position += movement;
            }

            _lastPosition = currentPosition;
        }
    }
}