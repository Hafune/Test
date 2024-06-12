using Core.Lib;
using UnityEngine;

namespace Core.Systems
{
    public class SpawnEffectLogic : AbstractEntityLogic
    {
        [SerializeField] private SpawnEffect _spawnEffect;

        public override void Run(int entity) => _spawnEffect.Execute();
    }
}