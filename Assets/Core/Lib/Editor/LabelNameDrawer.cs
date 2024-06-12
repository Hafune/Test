using Lib;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(EditorNameAttribute))]
public class LabelNameDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property,
            new GUIContent(((EditorNameAttribute) attribute).Name + $" ({property.displayName})"));
    }
}