using Lib;

namespace Core.Systems
{
    public class EntityLogicNode : AbstractEntityLogic
    {
        private AbstractEntityLogic[] _logics;

        private void Awake() => _logics = transform.GetSelfChildrenComponents<AbstractEntityLogic>(true);

        public override void Run(int entity)
        {
            for (int i = 0, iMax = _logics.Length; i < iMax; i++)
                _logics[i].Run(entity);
        }
    }
}