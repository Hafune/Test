using Core.Systems;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class TimelineProjectileLaunchLogic : AbstractEntityLogic
    {
        [SerializeField] private ProjectileLauncher2D _projectileLauncher;

        public override void Run(int entity) => _projectileLauncher.Launch();
    }
}