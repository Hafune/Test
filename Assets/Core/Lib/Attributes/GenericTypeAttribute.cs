using System;
using UnityEngine;


namespace Lib
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class GenericTypeAttribute : PropertyAttribute
    {
        public readonly Type type;

        public GenericTypeAttribute(Type type) => this.type = type;
    }
}