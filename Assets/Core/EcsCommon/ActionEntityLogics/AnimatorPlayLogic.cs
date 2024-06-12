using Core.Systems;
using UnityEngine;

public class AnimatorPlayLogic : AbstractEntityLogic
{
    [SerializeField, ReadOnly] private int _animationHash;
    [SerializeField] private string _animation = "";
    [SerializeField] private int _layerAnimation = 0;
    [SerializeField] private Animator _animator;

    private void OnValidate()
    {
        _animationHash = Animator.StringToHash(_animation);
        _animator = _animator ? _animator : GetComponentInParent<Animator>();
    }

    public override void Run(int entity) => _animator.Play(_animationHash, _layerAnimation, 0);
}