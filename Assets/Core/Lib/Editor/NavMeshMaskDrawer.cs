using UnityEngine;
using UnityEngine.AI;
using Lib;
using UnityEditor;


[CustomPropertyDrawer(typeof(NavMeshMaskAttribute))]
public class NavMeshMaskDrawer : PropertyDrawer {
 
    public override void OnGUI(Rect position, SerializedProperty serializedProperty, GUIContent label) {
 
        var count = NavMesh.GetSettingsCount();
        var agentTypeNames = new string[count];
        for (var i = 0; i < count; i++)
        {
            var id = NavMesh.GetSettingsByIndex(i).agentTypeID;
            var name = NavMesh.GetSettingsNameFromID(id);
            agentTypeNames[i] = name;
        }
        
        EditorGUI.BeginChangeCheck();
 
        int mask = serializedProperty.intValue;
 
        mask = EditorGUI.Popup(EditorGUI.PrefixLabel(position, label), mask, agentTypeNames);
        
        if (EditorGUI.EndChangeCheck()) {
            serializedProperty.intValue = mask;
        }
    }
}