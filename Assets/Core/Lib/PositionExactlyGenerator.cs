using UnityEngine;

namespace Core.Lib
{
    public class PositionExactlyGenerator : MonoBehaviour, IPositionGenerator
    {
        public Vector3 GeneratePositionXZ() => transform.position;
    }
}