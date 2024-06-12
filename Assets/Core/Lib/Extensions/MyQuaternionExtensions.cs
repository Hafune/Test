using UnityEngine;

namespace Lib
{
    public static class MyQuaternionExtensions
    {
        public static Quaternion RotateTowards(this Quaternion from, Vector3 to, float maxDegreesDelta) =>
            Quaternion.RotateTowards(from, Quaternion.LookRotation(to), maxDegreesDelta);

        public static Quaternion RotateTowards2D(Vector2 from, Vector2 to, float maxDegreesDelta)
        {
            var direction = from.RotatedToward(to, maxDegreesDelta);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.Euler(0f, 0f, angle - 90);
        }
    }
}