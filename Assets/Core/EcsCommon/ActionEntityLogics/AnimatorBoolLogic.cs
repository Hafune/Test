using Core.Systems;
using UnityEngine;

public class AnimatorBoolLogic : AbstractEntityLogic
{
    [SerializeField, ReadOnly] private int _propertyId;
    [SerializeField] private string _property;
    [SerializeField] private bool _value;
    [SerializeField] private Animator _animator;    

    private void OnValidate()
    {
        _propertyId = Animator.StringToHash(_property);
        _animator = _animator ? _animator : GetComponentInParent<Animator>();
    }

    public override void Run(int entity) => _animator.SetBool(_propertyId, _value);
}