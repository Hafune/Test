using UnityEngine;

namespace Core
{
    public interface ISpatialHashItem
    {
        public Vector2Int LastCell { get; set; }
        public float Radius { get; set; }
    }
}