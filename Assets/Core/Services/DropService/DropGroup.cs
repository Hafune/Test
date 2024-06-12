using System;
using GD.MinMaxSlider;
using UnityEngine;

namespace Core
{
    [Serializable]
    public struct DropGroup
    {
        [MinMaxSlider(0,10)]
        public Vector2Int itemsCount;
        [SerializeField] public DropItem[] items;
    }

    [Serializable]
    public struct DropItem
    {
        [SerializeField] public ItemDatabaseEnum item;
        [SerializeField] public float chance;
    }
}