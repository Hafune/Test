using System;
using BansheeGz.BGDatabase;
using Lib;
using Reflex;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Services
{
    public class ItemsService : MonoConstruct
    {
        private Context _context;
        private static UnityComponentMappedPool _pool;

        protected override void Construct(Context context) => _context = context;

        public void Awake()
        {
            _pool = _context.Resolve<PoolService>().DontDisposablePool.BuildMappedPull();
        }

        public void SpawnItem(Vector3 position, ItemDatabaseEnum id)
        {
            // var data = ItemDatabase.GetEntity((int)id);
            //
            // switch (data.source)
            // {
            //     case ItemSourceEnum.WeaponService:
            //         RandomSpawnVelocity(_weaponService.SpawnMapItem(Weapons.GetEntity(new BGId(data.key)), position));
            //         break;
            //     case ItemSourceEnum.EnhancementService:
            //         RandomSpawnVelocity(_enhancementService
            //             .SpawnMapItem(Enhancements.GetEntity(new BGId(data.key)), position)
            //             .gameObject);
            //         break;
            //     case ItemSourceEnum.Prefab:
            //         RandomSpawnVelocity(_pool
            //             .GetComponentByPrefab(data.prefab.transform, position, Quaternion.identity)
            //             .gameObject);
            //         break;
            //     default:
            //         throw new ArgumentOutOfRangeException();
            // }
        }

        private void RandomSpawnVelocity(GameObject go)
        {
            go.GetComponent<Rigidbody2D>()
                .velocity = new((Random.value - .5f) * 2, 12);
        }
    }
}