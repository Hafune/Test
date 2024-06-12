using UnityEngine;

namespace Core.Lib
{
    public abstract class BaseComponentTemplate : ScriptableObject
    {
        public abstract IBaseProvider Build();
    }
}