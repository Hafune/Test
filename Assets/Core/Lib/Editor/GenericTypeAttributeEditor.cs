using UnityEditor;
using UnityEngine;

namespace Lib
{
    [CustomPropertyDrawer(typeof(GenericTypeAttribute))]
    class GenericTypeAttributeEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label, true);

            if (!property.objectReferenceValue)
                return;

            property.objectReferenceValue =
                ((GenericTypeAttribute)attribute).type.IsInstanceOfType(property.objectReferenceValue)
                    ? property.objectReferenceValue
                    : null;
        }
    }
}