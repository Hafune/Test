using Core.Systems;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class TimelineProjectileGatlingLaunchLogic : AbstractEntityResettableLogic
    {
        [SerializeField] private ProjectileLauncher2D[] _projectileLaunchers;
        private ushort _index;

        public override void ResetLogic(int entity) => _index = 0;

        public override void Run(int entity) => _projectileLaunchers[_index++ % _projectileLaunchers.Length].Launch();
    }
}