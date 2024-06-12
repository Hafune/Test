using System.Linq;
using Core.Systems;
using Lib;
using UnityEngine;

public class AnimatorPlayRandomLogic : AbstractEntityLogic
{
    [SerializeField] private string[] _animations;
    [SerializeField] private int _layerAnimation = 0;
    [SerializeField] private Animator _animator;

    [field: SerializeField, HideInInspector]
    private int[] animationHash { get; set; }

    private void OnValidate()
    {
        animationHash = _animations.Select(Animator.StringToHash).ToArray();
        _animator = _animator ? _animator : GetComponentInParent<Animator>();
    }
    
    public override void Run(int entity) => _animator.Play(animationHash.Random(), _layerAnimation, 0);
}