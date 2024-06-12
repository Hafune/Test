using UnityEngine;

namespace Core.Lib
{
    public abstract class ComponentTemplate<T> : BaseComponentTemplate where T : struct
    {
        [SerializeField] public T value;
        public override IBaseProvider Build() => new BaseProvider<T>(value);
    }
}