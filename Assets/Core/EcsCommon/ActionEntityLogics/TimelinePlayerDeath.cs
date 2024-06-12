using Core.Services;
using Core.Systems;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class TimelinePlayerDeath : AbstractEntityLogic
    {
        private PlayerStateService _playerState;

        private void Awake() => _playerState = Context.Resolve<PlayerStateService>();

        public override void Run(int entity) => _playerState.PlayerDied();
    }
}