using Core.Systems;

namespace Core.ExternalEntityLogics
{
    public class Move_Logic : AbstractEntityLogic
    {
        private MoveFunction _moveFunction;

        private void Awake() => _moveFunction = new MoveFunction(Context);

        public override void Run(int entity) => _moveFunction.UpdateEntity(entity);
    }
}