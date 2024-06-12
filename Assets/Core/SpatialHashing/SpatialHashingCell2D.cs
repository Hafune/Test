using System;
using UnityEngine;

namespace Core
{
    public class SpatialHashingCell2D<T> where T : MonoBehaviour, ISpatialHashItem
    {
        private readonly float _cellSize;

        private const int sizeX = 1023;
        private const int sizeY = 1023;
        private readonly byte[,] grid = new byte[sizeX + 1, sizeY + 1];

        public SpatialHashingCell2D(float cellSize) => _cellSize = cellSize;

        public void Add(T obj) => grid[obj.LastCell.x, obj.LastCell.y]++;

        public void Remove(T obj) => grid[obj.LastCell.x, obj.LastCell.y]--;

        public bool HasObjects(Vector2Int position, int radius)
        {
            var minCellX = Mathf.FloorToInt(position.x - radius);
            var maxCellX = Mathf.FloorToInt(position.x + radius);
            var minCellY = Mathf.FloorToInt(position.y - radius);
            var maxCellY = Mathf.FloorToInt(position.y + radius);

            for (int x = Math.Max(minCellX, 0), maxX = Math.Min(maxCellX, sizeX); x <= maxX; x++)
            for (int y = Math.Max(minCellY, 0), maxY = Math.Min(maxCellY, sizeY); y <= maxY; y++)
                if (grid[x, y] > 0)
                    return true;

            return false;
        }

        public void UpdateByNextPosition(T obj, Vector3 pos)
        {
            var key = CalculateCell(pos);

            if (key == obj.LastCell)
                return;

            grid[obj.LastCell.x, obj.LastCell.y]--;
            obj.LastCell = key;
            Add(obj);
        }

        public Vector2Int CalculateCell(Vector3 pos)
        {
            pos += new Vector3(100, 0, 500);
            var position = pos / _cellSize;
            return new((int)Math.Floor(position.x), (int)Math.Floor(position.z));
        }
    }
}