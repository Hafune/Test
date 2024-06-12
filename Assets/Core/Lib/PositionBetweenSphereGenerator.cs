using Lib;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Lib
{
    [RequireComponent(typeof(SphereCollider))]
    public class PositionBetweenSphereGenerator : MonoBehaviour, IPositionGenerator
    {
        private SphereCollider _sphere0;
        private SphereCollider _sphere1;

        private void Awake()
        {
            var spheres = GetComponents<SphereCollider>();
            _sphere0 = spheres[0];
            _sphere1 = spheres[1];

            spheres.ForEach(s => s.enabled = false);
        }

        public Vector3 GeneratePositionXZ()
        {
            var r0 = _sphere0.radius * _sphere0.transform.lossyScale.x;
            var r1 = _sphere1.radius * _sphere1.transform.lossyScale.x;

            var minRadius = Mathf.Min(r0, r1);
            var maxRadius = Mathf.Max(r0, r1);

            var distance = maxRadius - minRadius + minRadius;
            var position2d = Random.insideUnitCircle.normalized;
            position2d *= distance;

            var position = new Vector3(position2d.x, 0, position2d.y);

            return position + transform.position;
        }
    }
}