using Core.Systems;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class PlayerDeath_Logic : AbstractActionEntityLogic
    {
        private static int _animationHash = Animator.StringToHash("Dead");

        [SerializeField] private Animator _animator;
        
        private void OnValidate() => _animator = _animator ? _animator : GetComponentInParent<Animator>();

        public override void StartLogic(int entity) => _animator.Play(_animationHash, 0, 0);
    }
}