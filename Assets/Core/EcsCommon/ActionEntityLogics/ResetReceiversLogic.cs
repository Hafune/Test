using Core.Systems;
using UnityEngine;

public class ResetReceiversLogic : AbstractEntityLogic
{
    [SerializeField] private AbstractArea _area;

    public override void Run(int entity) => _area.ResetReceivers();
}