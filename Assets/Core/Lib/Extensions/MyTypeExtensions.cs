using System;
using System.Reflection;

namespace Lib
{
    public static class MyTypeExtensions
    {
        public static FieldInfo GetFieldPrivate(this Type type, in string name) =>
            type.GetField(name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
    }
}