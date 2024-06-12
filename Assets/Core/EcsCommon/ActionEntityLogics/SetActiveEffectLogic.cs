using Core.Systems;
using UnityEngine;

public class SetActiveEffectLogic : AbstractEntityLogic
{
    [SerializeField] private GameObject _effectRoot;
    [SerializeField] private bool _isActive;

    public override void Run(int entity) => _effectRoot.SetActive(_isActive);
}