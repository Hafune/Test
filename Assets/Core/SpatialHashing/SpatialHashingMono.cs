using UnityEngine;

namespace Core
{
    public class SpatialHashingMono : MonoBehaviour
    {
        [SerializeField] private float _cellSize = 1;

        private SpatialHashing2D<SpatialHashItem> _root;

        public SpatialHashing2D<SpatialHashItem> Root => _root;

        private void Awake() => _root = new SpatialHashing2D<SpatialHashItem>(_cellSize);
    }
}