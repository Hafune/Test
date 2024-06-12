using System.Linq;

namespace Core.Lib
{
    public class EffectNode : AbstractEffect
    {
        private AbstractEffect[] _effects;

        private void Awake()
        {
            _effects = transform
                .GetComponentsInChildren<AbstractEffect>(true)
                .Where(i => i != this)
                .ToArray();
        }

        public override void Execute()
        {
            for (int i = 0, iMax = _effects.Length; i < iMax; i++)
                _effects[i].Execute();
        }
    }
}