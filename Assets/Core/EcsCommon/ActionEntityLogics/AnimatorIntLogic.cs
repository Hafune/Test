using Core.Systems;
using UnityEngine;

public class AnimatorIntLogic : AbstractEntityLogic
{
    [SerializeField, ReadOnly] private int _propertyId;
    [SerializeField] private string _property;
    [SerializeField] private int _value;
    [SerializeField] private Animator _animator;

    private void OnValidate()
    {
        _propertyId = Animator.StringToHash(_property);
        _animator = _animator ? _animator : GetComponentInParent<Animator>();
    }

    public override void Run(int entity) => _animator.SetInteger(_propertyId, _value);
}