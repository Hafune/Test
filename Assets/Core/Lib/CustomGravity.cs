namespace Core.Lib
{
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class CustomGravity : MonoBehaviour
    {
        public float gravity = Physics.gravity.y;
        public float gravityScale = 1.0f;

        private Rigidbody _rigidbody;

        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        private void FixedUpdate() => _rigidbody.AddForce(gravity * gravityScale * Vector3.up, ForceMode.Acceleration);
    }
}