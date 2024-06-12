using System;
using Lib;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Services
{
    [CreateAssetMenu(menuName = "Game Config/" + nameof(DropTemplate))]
    public class DropTemplate : AbstractDropTemplate
    {
        [SerializeField] private EntitiesTable _table;
        [SerializeField] private DropGroup[] data;

        public override bool Contains(int id) => _table.Contains(id);

        public override void SpawnDrop(Vector3 position, float chanceScale, ItemsService itemsService)
        {
            foreach (var group in data.AsSpan())
            {
                int currentCount = 0;
                int min = Math.Min(group.itemsCount.x, group.items.Length);
                int max = group.itemsCount.y;

                if (min > 0)
                {
                    float totalChances = group.items.SumBy(i => i.chance);

                    while (currentCount < min)
                    {
                        float chance = totalChances * Random.value;
                        float accumulatedChance = 0;

                        foreach (var item in group.items.AsSpan())
                            if ((accumulatedChance += item.chance) >= chance)
                            {
                                itemsService.SpawnItem(position, item.item);
                                currentCount++;
                                break;
                            }
                    }
                }
                else
                {
                    var chance = Random.value;
                    foreach (var item in group.items.AsSpan())
                        if (item.chance * chanceScale > chance)
                        {
                            itemsService.SpawnItem(position, item.item);
                            currentCount++;

                            if (currentCount >= max)
                                break;
                        }
                }
            }
        }
    }
}