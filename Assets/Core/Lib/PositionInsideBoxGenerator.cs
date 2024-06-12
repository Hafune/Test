using Lib;
using UnityEngine;

namespace Core.Lib
{
    [RequireComponent(typeof(BoxCollider))]
    public class PositionInsideBoxGenerator : MonoBehaviour, IPositionGenerator
    {
        private BoxCollider _box;

        private void Awake()
        {
            _box = GetComponent<BoxCollider>();
            _box.enabled = false;
        }

        public Vector3 GeneratePositionXZ()
        {
            var position = _box.GetRandomPointInside();
            position.y = transform.position.y;

            return position;
        }
    }
}