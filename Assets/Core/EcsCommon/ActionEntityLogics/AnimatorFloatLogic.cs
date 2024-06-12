using Core.Systems;
using UnityEngine;

public class AnimatorFloatLogic : AbstractEntityLogic
{
    [SerializeField] private string _property;
    [SerializeField] private float _value;
    [SerializeField] private Animator _animator;
    [SerializeField, HideInInspector] private int _propertyId;

    private void OnValidate()
    {
        _propertyId = Animator.StringToHash(_property);
        _animator = _animator ? _animator : GetComponentInParent<Animator>();
    }

    public override void Run(int entity) => _animator.SetFloat(_propertyId, _value);
}