using System;
using UnityEngine;
using System.Collections.Generic;

namespace Core
{
    public class SpatialHashing2D<T> where T : MonoBehaviour, ISpatialHashItem
    {
        private readonly float _cellSize;
        private readonly Dictionary<int, List<T>> grid = new();

        public SpatialHashing2D(float cellSize)
        {
            _cellSize = cellSize;
        }

        private void AddObject(T obj, Vector2Int key)
        {
            obj.LastCell = key;
            var hash = CalcHash(key);
            if (!grid.TryGetValue(hash, out var list))
            {
                list = new List<T>(4);
                list.Add(obj);
                grid.Add(hash, list);
            }
            else
            {
                grid[hash].Add(obj);
            }
        }

        public void Remove(T obj) => RemoveObject(obj, obj.LastCell);

        private void RemoveObject(T obj, Vector2Int key)
        {
            if (!grid.TryGetValue(CalcHash(key), out var list))
                return;

            list.Remove(obj);
        }

        public void FindObjects(List<T> objects, Vector2 position, float radius, float innerRadius = 0)
        {
            var minCellX = (int)Math.Floor((position.x - radius) / _cellSize);
            var maxCellX = (int)Math.Floor((position.x + radius) / _cellSize);
            var minCellY = (int)Math.Floor((position.y - radius) / _cellSize);
            var maxCellY = (int)Math.Floor((position.y + radius) / _cellSize);

            var innerMinCellX = (int)Math.Floor((position.x - innerRadius) / _cellSize);
            var innerMaxCellX = (int)Math.Floor((position.x + innerRadius) / _cellSize);
            var innerMinCellY = (int)Math.Floor((position.y - innerRadius) / _cellSize);
            var innerMaxCellY = (int)Math.Floor((position.y + innerRadius) / _cellSize);

            for (var x = minCellX; x <= maxCellX; x++)
            {
                for (var y = minCellY; y <= maxCellY; y++)
                {
                    if (x < innerMaxCellX && x > innerMinCellX && y < innerMaxCellY && y > innerMinCellY)
                        continue;

                    if (!grid.TryGetValue(CalcHash(new Vector2Int(x, y)), out var list))
                        continue;

                    var count = list!.Count;

                    for (byte i = 0; i < count; i++)
                        objects.Add(list[i]);
                }
            }
        }

        public void Clear() => grid.Clear();

        public void Update(T obj)
        {
            var position = obj.transform.position / _cellSize;
            var key = new Vector2Int((int)Math.Floor(position.x), (int)Math.Floor(position.z));

            if (key == obj.LastCell)
                return;

            RemoveObject(obj, obj.LastCell);
            AddObject(obj, key);
        }

        private int CalcHash(Vector2Int v) => v.x + v.y * 100000;
    }
}