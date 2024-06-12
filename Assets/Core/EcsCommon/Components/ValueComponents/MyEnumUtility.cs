using System;
using Core.Lib;

namespace Core.EcsCommon.ValueComponents
{
    public static class MyEnumUtility<T> where T : Enum
    {
        private static Glossary<string> _cache = new();

        public static string Name(int e)
        {
            if (_cache.TryGetValue(e, out var n))
                return n;

            var name = Enum.GetName(typeof(T), e);
            _cache.Add(e, name);
            return name;
        }
    }
}