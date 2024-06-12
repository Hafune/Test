using Lib;

namespace Core.Systems
{
    public class AnyConditionNode : AbstractActionEntityCondition
    {
        private AbstractActionEntityCondition[] _conditions;

        private void Awake()
        {
            _conditions = new AbstractActionEntityCondition[transform.childCount];
            int index = 0;
            transform.ForEachSelfChildren<AbstractActionEntityCondition>(c => _conditions[index++] = c, true);
        }

        public override bool Check(int entity)
        {
            for (int i = 0, iMax = _conditions.Length; i < iMax; i++)
                if (_conditions[i].Check(entity))
                    return true;

            return false;
        }
    }
}