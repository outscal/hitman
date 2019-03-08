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
        ScriptableGraph graph;
        int[,] position_values;
        int hzero, vzero;
        public void OnEnable()
        {
            graph = (ScriptableGraph)target;
            position_values = new int[graph.maxhight, graph.maxwidth];
            int multiplier = 5;
            hzero = ((int)(Mathf.Ceil(((float)graph.maxwidth / 2)))) - 1; vzero = (int)((Mathf.Ceil((float)(graph.maxhight / 2)))) - 1;
            for (int i = 0; i < graph.maxhight; i++)
            {
                for (int j = 0; j < graph.maxwidth; j++)
                {
                    position_values[i, j] = -1;
                }
            }
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            for (int i = 0; i < graph.maxhight; i++)
            {
                GUILayout.BeginHorizontal();
                for (int j = 0; j < graph.maxwidth; j++)
                {
                    position_values[i, j] = EditorGUILayout.IntField("", position_values[i, j], GUILayout.MaxWidth(50f));
                }
                GUILayout.EndHorizontal();
            }
            if (GUILayout.Button("Set Node Positions"))
            {
                for (int i = 0; i < graph.maxhight; i++)
                {
                    for (int j = 0; j < graph.maxwidth; j++)
                    {
                        if(position_values[i, j] != -1)
                        {
                            graph.Graph[position_values[i, j]].node.nodePosition = new Vector3(-((j-hzero)*5), 0, (i-vzero)*5);
                        }
                    }
                }
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
                for (int i = 0; i < graph.stars.Length; i++)
                {
                    if (graph.stars[i].name == "")
                    {
                        Debug.LogError("Star Name Not Set");
                    }
                }
            }
            if (GUILayout.Button("Create Grid"))
            {
                OnEnable();

            }
        }

    }
}