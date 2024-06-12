using UnityEngine;

namespace Core.Systems
{
    public class ResetLogic : AbstractEntityLogic
    {
        [SerializeField] private AbstractEntityResettableLogic _target;

        public override void Run(int entity) => _target.ResetLogic(entity);
    }
}