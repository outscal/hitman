using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PathSystem;
namespace EditorScripts
{
    [CustomEditor(typeof(ScriptableGraph)),CanEditMultipleObjects]
    // Start is called before the first frame update
  public class GraphValidator : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(GUILayout.Button("Set NodeId"))
            {
                Debug.Log("Setting Id");
            }
        }
    }
}
