using UnityEngine;

namespace Core
{
    public class SpatialHashingCellMono : MonoBehaviour
    {
        [SerializeField] private float _cellSize = .5f;

        private SpatialHashingCell2D<SpatialHashItemCell> _root;

        public SpatialHashingCell2D<SpatialHashItemCell> Root => _root;

        private void Awake() => _root = new SpatialHashingCell2D<SpatialHashItemCell>(_cellSize);
    }
}