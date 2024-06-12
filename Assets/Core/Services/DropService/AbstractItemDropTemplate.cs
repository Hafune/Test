using GD.MinMaxSlider;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Services
{
    public abstract class AbstractItemDropTemplate : AbstractDropTemplate
    {
        [SerializeField] private EntitiesTable _table;
        [SerializeField, MinMaxSlider(1, 5)] protected Vector2Int _levels;
        [SerializeField, MinMaxSlider(0, 10)] private Vector2Int _itemsCount;
        [SerializeField] private float _chance;

        public override bool Contains(int id) => _table.Contains(id);

        protected void SpawnDrop(Vector3 position, float chanceScale, ItemsService itemsService, IDropItem[] models)
        {
            int currentCount = 0;
            int min = _itemsCount.x;
            int max = _itemsCount.y;

            if (min > 0)
            {
                while (currentCount < min)
                {
                    itemsService.SpawnItem(position,
                        (ItemDatabaseEnum)models[Random.Range(0, models.Length)].globalId);
                    currentCount++;
                }
            }

            for (int i = 0; i < max - min; i++)
            {
                var roll = Random.value;

                if (roll > _chance * chanceScale)
                    continue;

                itemsService.SpawnItem(position,
                    (ItemDatabaseEnum)models[Random.Range(0, models.Length)].globalId);
            }
        }
    }
}