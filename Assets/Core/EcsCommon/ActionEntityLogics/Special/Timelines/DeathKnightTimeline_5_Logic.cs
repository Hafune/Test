using Core.Systems;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class DeathKnightTimeline_5_Logic : AbstractEntityLogic
    {
        [SerializeField] private Transform _rootTransform;
        [SerializeField] private Transform _raycastPoint;
        [SerializeField, Min(0)] private float _wallOffset = 3f;
        [SerializeField, Min(0)] private float _bossRoomWidth = 50f;

        public override void Run(int entity)
        {
            var leftDistance = Physics2D.Raycast(
                    _raycastPoint.position,
                    Vector2.left,
                    _bossRoomWidth,
                    _raycastPoint.gameObject.layer)
                .distance;
            var rightDistance = Physics2D.Raycast(
                    _raycastPoint.position,
                    Vector2.right,
                    _bossRoomWidth,
                    _raycastPoint.gameObject.layer)
                .distance;

            if (leftDistance > rightDistance)
            {
                _rootTransform.position -= new Vector3(leftDistance - _wallOffset, 0, 0);
                _rootTransform.rotation = Quaternion.identity;
            }
            else
            {
                _rootTransform.position += new Vector3(rightDistance - _wallOffset, 0, 0);
                _rootTransform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
}