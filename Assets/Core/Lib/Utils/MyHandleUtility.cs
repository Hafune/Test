using UnityEngine;

namespace Lib
{
    //Копии методов UnityEditor.HandleUtility
    public static class MyHandleUtility
    {
        public static float DistancePointLine(Vector3 point, Vector3 lineStart, Vector3 lineEnd) => Vector3.Magnitude(ProjectPointLine(point, lineStart, lineEnd) - point);
        
        public static float DistancePointLineSqrt(Vector3 point, Vector3 lineStart, Vector3 lineEnd) => Vector3.SqrMagnitude(ProjectPointLine(point, lineStart, lineEnd) - point);
        
        public static Vector3 ProjectPointLine(
            Vector3 point,
            Vector3 lineStart,
            Vector3 lineEnd)
        {
            Vector3 rhs = point - lineStart;
            Vector3 vector3 = lineEnd - lineStart;
            float magnitude = vector3.magnitude;
            Vector3 lhs = vector3;
            if (magnitude > 9.99999997475243E-07)
                lhs /= magnitude;
            float num = Mathf.Clamp(Vector3.Dot(lhs, rhs), 0.0f, magnitude);
            return lineStart + lhs * num;
        }
    }
}