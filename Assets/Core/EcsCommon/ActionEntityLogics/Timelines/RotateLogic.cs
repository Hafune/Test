using Core.Systems;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class RotateLogic : AbstractEntityLogic
    {
        private enum Direction
        {
            Left,
            Right
        }

        [SerializeField] private Transform _root;
        [SerializeField] private Direction _direction = Direction.Right;

        private void OnValidate() => _root = transform.root;

        public override void Run(int entity)
        {
            var rotation = _root.transform.rotation;
            rotation.y = _direction == Direction.Left ? 180 : 0;
            _root.transform.rotation = rotation;
        }
    }
}