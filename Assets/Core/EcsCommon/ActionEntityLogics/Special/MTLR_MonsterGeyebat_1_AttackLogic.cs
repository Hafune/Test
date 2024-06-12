using System;
using Core.Systems;
using UnityEngine;

namespace Core.ExternalEntityLogics
{
    public class MTLR_MonsterGeyebat_1_AttackLogic : AbstractActionEntityLogic
    {
        private static int Attack_Hash = Animator.StringToHash("CS_MonsterGeyebat_1");

        [SerializeField] private Animator _animator;

        private void OnValidate() => _animator = _animator ? _animator : GetComponentInParent<Animator>();

        public override void StartLogic(int entity) => _animator.Play(Attack_Hash);
    }
}