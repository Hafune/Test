using Core.Components;
using Core.Generated;
using Core.Systems;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class DeathKnightTimeline_10_Logic : AbstractEntityLogic
    {
        [SerializeField] private Transform _root;
        private EcsFilter _playerFilter;
        private EcsPool<TransformComponent> _transformPool;

        private void OnValidate() => _root = transform.root;

        private void Awake()
        {
            _playerFilter = Context.Resolve<EcsWorld>().Filter<PlayerUniqueTag>().Inc<TransformComponent>().End();
            _transformPool = Context.Resolve<ComponentPools>().Transform;
        }

        public override void Run(int entity)
        {
            int playerEntity = _playerFilter.GetFirst();
            float x = _transformPool.Get(playerEntity).transform.position.x;
            _root.position = _root.position.Copy(x: x);
        }
    }
}