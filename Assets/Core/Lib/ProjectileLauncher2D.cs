using System.Collections.Generic;
using Core.Services;
using Lib;
using Reflex;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class ProjectileLauncher2D : MonoConstruct
    {
        [SerializeField] private Rigidbody2D _prefab;
        [SerializeField] private ProjectileEmitters2D _emitters;
        [SerializeField] private float _force;

        private UnityComponentPool<Rigidbody2D> _projectilePool;
        private Context _context;
        private IEnumerable<Rigidbody2D> _enumerator;

        protected override void Construct(Context context) => _context = context;

        private void Awake() => _projectilePool = _context.Resolve<PoolService>().ScenePool.GetPullByPrefab(_prefab);

        public void Launch() => _emitters.ForEachEmitter(LaunchOne);

        private void LaunchOne(Transform t)
        {
            var body = _projectilePool.GetObject(t.position, t.rotation);
            var velocity = body.transform.right * _force;
            body.velocity = velocity;
        }
    }
}