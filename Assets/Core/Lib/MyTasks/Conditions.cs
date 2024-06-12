using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Lib.MyTasks
{
    public class Conditions : IDisposable
    {
        private readonly Dictionary<Type, bool> values = new();

        public void Set<T>(bool value) => values[typeof(T)] = value;

        public bool All() => values.All(i => i.Value);
        public bool Any() => values.Any(i => i.Value);

        public void Dispose() => values.Clear();
    }
}