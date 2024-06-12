using Core.Lib;

namespace Lib
{
    using UnityEngine;
    using UnityEditor;
    using UnityEditor.UI;

//Don't forget to put this file inside a 'Editor' folder
    [CanEditMultipleObjects, CustomEditor(typeof(NonDrawingRaycastTarget), false)]
    public class NonDrawingRaycastTargetEditor : GraphicEditor
    {
        public override void OnInspectorGUI()
        {
            base.serializedObject.Update();
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(base.m_Script, new GUILayoutOption[0]);
            EditorGUI.EndDisabledGroup();
            // skipping AppearanceControlsGUI
            base.RaycastControlsGUI();
            base.serializedObject.ApplyModifiedProperties();
        }
    }
}