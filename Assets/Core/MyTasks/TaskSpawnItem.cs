using System;
using Core.Services;
using Reflex;
using UnityEngine;

namespace Core
{
    public class TaskSpawnItem : MonoBehaviour, IMyTask
    {
        [SerializeField] private ItemDatabaseEnum[] _items;
        [SerializeField] private Transform _point;

        public bool InProgress => false;

        public void Begin(
            Context context,
            Payload payload,
            Action<IMyTask> onComplete = null)
        {
            var itemsService = context.Resolve<ItemsService>();

            foreach (var item in _items)
                itemsService.SpawnItem(_point.position, item);

            onComplete?.Invoke(this);
        }
    }
}