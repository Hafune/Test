using UnityEngine;

namespace Lib
{
    public class EditorNameAttribute : PropertyAttribute
    {
        public readonly string Name;

        public EditorNameAttribute(string name) => Name = name;
    }
}