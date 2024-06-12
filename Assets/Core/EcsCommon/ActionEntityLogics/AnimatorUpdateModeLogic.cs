using Core.Systems;
using UnityEngine;

public class AnimatorUpdateModeLogic : AbstractEntityLogic
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimatorUpdateMode _mode;

    private void OnValidate() => _animator = _animator ? _animator : GetComponentInParent<Animator>();

    public override void Run(int entity) => _animator.updateMode = _mode;
}