using System.Reflection;
using Reflex;
using UnityEngine;

namespace Lib
{
    public abstract class MonoConstruct : MonoBehaviour
    {
        public static MethodInfo GetMethod() => typeof(MonoConstruct).GetMethod(nameof(Construct),
            BindingFlags.NonPublic |
            BindingFlags.Instance
        );

        protected abstract void Construct(Context context);
    }
}