using System;
using System.Collections.Generic;
using System.Linq;
using Core.Lib;
using Lib;
using Reflex;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Services
{
    public class DropService : MonoConstruct
    {
        [SerializeField] private AbstractDropTemplate[] _enemyDropTemplates;
        [SerializeField] private SceneDrops[] _sceneDropTemplates;
        private Glossary<AbstractDropTemplate[]> _enemyDropsCache = new();
        private Dictionary<string, Glossary<AbstractDropTemplate[]>> _chestDropsCache = new();
        private Context _context;
        private AddressableService _addressableService;
        private ItemsService _itemsService;

        protected override void Construct(Context context) => _context = context;

        private void Awake()
        {
            _context.Resolve<PoolService>().DontDisposablePool.BuildMappedPull();
            _addressableService = _context.Resolve<AddressableService>();
            _itemsService = _context.Resolve<ItemsService>();
        }

        public void SpawnUnitDrop(int id, Vector3 position, DropGroup[] unitDropData)
        {
            if (!_enemyDropsCache.TryGetValue(id, out var dropTemplates))
                _enemyDropsCache.Add(id,
                    dropTemplates = _enemyDropTemplates
                        .Where(i => i.Contains(id))
                        .Select(i => i).ToArray());

            foreach (var template in dropTemplates)
                template.SpawnDrop(position, 1f, _itemsService);

            SpawnDrop(position, unitDropData);
        }

        public void SpawnSceneDrop(int id, Vector3 position, DropGroup[] unitDropData)
        {
            SpawnDrop(position, unitDropData);
            
            var sceneName = _addressableService.SceneName;

            if (string.IsNullOrEmpty(sceneName))
                return;

            if (!_chestDropsCache.TryGetValue(sceneName, out var sceneTemplates))
                _chestDropsCache.Add(sceneName,
                    sceneTemplates = new Glossary<AbstractDropTemplate[]>()
                );

            if (!sceneTemplates.TryGetValue(id, out var dropTemplates))
            {
                sceneTemplates.Add(id, dropTemplates = _sceneDropTemplates
                    .Where(i => i.scene == sceneName)
                    .SelectMany(i => i.chestTemplates)
                    .Where(i => i.Contains(id))
                    .ToArray());
            }

            foreach (var template in dropTemplates)
                template.SpawnDrop(position, 1f, _itemsService);
        }

        private void SpawnDrop(Vector3 position, Span<DropGroup> groups)
        {
            foreach (var group in groups)
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

                        foreach (var data in group.items.AsSpan())
                            if ((accumulatedChance += data.chance) >= chance)
                            {
                                _itemsService.SpawnItem(position, data.item);
                                currentCount++;
                                break;
                            }
                    }
                }
                else
                {
                    var chance = Random.value;
                    foreach (var data in group.items.AsSpan())
                        if (data.chance > chance)
                        {
                            _itemsService.SpawnItem(position, data.item);
                            currentCount++;

                            if (currentCount >= max)
                                break;
                        }
                }
            }
        }

        [Serializable]
        private struct SceneDrops
        {
            [field: SerializeField] public SceneField scene { get; private set; }
            [field: SerializeField] public AbstractDropTemplate[] chestTemplates { get; private set; }
        }
    }
}