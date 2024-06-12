#region Script Synopsis

//For adding buttons to a controller's SpreadPattern in the inspector, calling its methods for adding linear/stepped automators.

#endregion

using UnityEngine;
using UnityEditor;

namespace Core.Lib
{
    [CanEditMultipleObjects, CustomEditor(typeof(BasePattern), true)]
    public class BasePatternEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            Color cachedBG = GUI.backgroundColor;
            DrawDefaultInspector();

            if (targets.Length > 1)
            {
                GUI.backgroundColor = new Color(.98f, .98f, 0);
                if (GUILayout.Button("Clear Cache"))
                    foreach (SpreadPattern t in targets)
                        t.ClearEmitterCache();

                GUI.backgroundColor = Color.cyan;
                if (GUILayout.Button("Clear Emitters"))
                    foreach (SpreadPattern t in targets)
                        t.ClearEmitters();

                GUI.backgroundColor = Color.cyan;
                if (GUILayout.Button("Clone First Emitter To All"))
                    foreach (SpreadPattern t in targets)
                        t.cloneFirstEmitter();

                GUI.backgroundColor = cachedBG;
            }
            else
            {
                var bsScript = (SpreadPattern)target;

                GUI.backgroundColor = new Color(.98f, .98f, 0);
                if (GUILayout.Button("Clear Cache"))
                    bsScript.ClearEmitterCache();

                GUI.backgroundColor = Color.cyan;
                if (GUILayout.Button("Clear Emitters"))
                    bsScript.ClearEmitters();

                GUI.backgroundColor = Color.cyan;
                if (GUILayout.Button("Clone First Emitter To All"))
                    bsScript.cloneFirstEmitter();

                GUI.backgroundColor = cachedBG;
            }
        }
    }
}