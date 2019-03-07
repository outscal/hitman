using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PathSystem;
namespace EditorScripts
{
    [CustomEditor(typeof(ScriptableGraph)), CanEditMultipleObjects]
    // Start is called before the first frame update
    public class GraphValidator : Editor
    {
        public override void OnInspectorGUI()
        {
            ScriptableGraph graph = (ScriptableGraph)target;
            int[,] position_values = new int[graph.maxhight, graph.maxwidth];
            int multiplier = 5, hzero = graph.maxwidth / 2,vzero=graph.maxhight/2;
            base.OnInspectorGUI();
            for (int i = 0; i < graph.maxhight; i++)
            {
                GUILayout.BeginHorizontal();
                for (int j = 0; j < graph.maxwidth; j++)
                {
                    position_values[i, j] = -1;
                    position_values[i,j]=EditorGUILayout.IntField("", position_values[i, j], GUILayout.MaxWidth(50f));
                }
                GUILayout.EndHorizontal();
            }
            if (GUILayout.Button("Set NodeId"))
            {
                if (graph.set)
                {
                    for (int i = 0; i < graph.Graph.Count; i++)
                    {
                        graph.Graph[i].node.uniqueID = i;
                        graph.Graph[i].up = -1;
                        graph.Graph[i].down = -1;
                        graph.Graph[i].left = -1;
                        graph.Graph[i].right = -1;
                        graph.set = false;
                    }
                }
            }
            if (GUILayout.Button("Velidate"))
            {
                for(int i = 0; i < graph.stars.Length; i++)
                {
                    if (graph.stars[i].name == "")
                    {
                        Debug.LogError("Star Name Not Set");
                    }
                }
            }
            if (GUILayout.Button("Create Grid"))
            {
                
                
            }
        }
       
}
}