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
            ScriptableGraph graph = (ScriptableGraph)target;

            base.OnInspectorGUI();
            if(GUILayout.Button("Set NodeId"))
            {
                if (graph.setAndValidate)
                {
                    for (int i = 0; i < graph.Graph.Count; i++)
                    {
                        graph.Graph[i].node.uniqueID = i;
                        graph.Graph[i].up = -1;
                        graph.Graph[i].down = -1;
                        graph.Graph[i].left = -1;
                        graph.Graph[i].right = -1;
                        graph.setAndValidate = false;
                    }
                }

            }
        }
    }
}
