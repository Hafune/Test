using System;
using UnityEngine;
using System.Collections.Generic;

namespace Core
{
    public class SpatialHashingCell2DByDictionary<T> where T : MonoBehaviour, ISpatialHashItem
    {
        private readonly float _cellSize;

        private readonly Dictionary<int, int> grid = new();

        public SpatialHashingCell2DByDictionary(float cellSize)
        {
            _cellSize = cellSize;
        }

        public void Add(T obj)
        {
            grid.TryAdd(CalcHash(obj.LastCell), 0);
            grid[CalcHash(obj.LastCell)]++;
        }

        public void Remove(T obj)
        {
            grid[CalcHash(obj.LastCell)]--;
        }

        public bool FindObjects(Vector2Int position, int radius)
        {
            var minCellX = Mathf.FloorToInt(position.x - radius);
            var maxCellX = Mathf.FloorToInt(position.x + radius);
            var minCellY = Mathf.FloorToInt(position.y - radius);
            var maxCellY = Mathf.FloorToInt(position.y + radius);

            for (int x = minCellX; x <= maxCellX; x++)
            for (int y = minCellY; y <= maxCellY; y++)
                if (grid.TryGetValue(CalcHash(new Vector2Int(x, y)), out int i) && i > 0)
                    return true;

            return false;
        }

        // public void Clear() => grid.Clear();

        public void Update(T obj)
        {
            var key = CalculateCell(obj.transform.position);

            if (key == obj.LastCell)
                return;

            grid[CalcHash(obj.LastCell)]--;
            obj.LastCell = key;
            Add(obj);
        }

        public void UpdateByNextPosition(T obj, Vector3 pos)
        {
            var key = CalculateCell(pos);

            if (key == obj.LastCell)
                return;

            grid[CalcHash(obj.LastCell)]--;
            obj.LastCell = key;
            Add(obj);
        }

        public Vector2Int CalculateCell(Vector3 pos)
        {
            var position = pos / _cellSize;
            return new((int)Math.Floor(position.x), (int)Math.Floor(position.z));
        }

        public Vector3 CellToWorldPosition(Vector2Int pos)
        {
            var position = (Vector2)pos * _cellSize;
            return new Vector3((int)Math.Floor(position.x), 0, (int)Math.Floor(position.y)) +
                   Vector3.one * _cellSize / 2;
        }

        private int CalcHash(Vector2Int v) => v.x + v.y * 100000;
    }
}