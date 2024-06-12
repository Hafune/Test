using Core.Components;
using Core.Systems;
using Leopotam.EcsLite;
using Lib;
using UnityEngine;

public class SetPositionOnGroundUnderPlayer : AbstractEntityLogic
{
    [SerializeField] private Transform _target;
    private EcsFilter _playerFilter;
    private EcsPool<TransformComponent> _transformPool;

    private void Awake()
    {
        var world = Context.Resolve<EcsWorld>();
        _playerFilter = world.Filter<PlayerUniqueTag>().Inc<TransformComponent>().End();
        _transformPool = world.GetPool<TransformComponent>();
    }

    public override void Run(int entity)
    {
        var playerPosition = _transformPool.Get(_playerFilter.GetFirst()).transform.position;
        var ray = Physics2D.Raycast(playerPosition, Vector2.down, 20, 1);

        _target.position = ray.point;
    }
}