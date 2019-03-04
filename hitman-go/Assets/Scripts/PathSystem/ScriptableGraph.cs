using Common;
using PathSystem.NodesScript;
using System.Collections.Generic;
using UnityEngine;
namespace PathSystem
{
    [CreateAssetMenu(fileName = "ScriptableGraph", menuName = "Custom Objects/Graph/ScriptableGraph", order = 0)]
    public class ScriptableGraph : ScriptableObject
    {
        public GameObject line;

        public StarTypes[] stars=new StarTypes[0];
        public int maxPlayerMoves=10,noOfEnemies=1;
        public NodeControllerView nodeprefab, targetNode;
        public List<ScriptableNode> Graph = new List<ScriptableNode>();
        public List<Node> GetGraph()
        {
            List<Node> graph = new List<Node>();
            for (int i = 0; i < Graph.Count; i++)
            {
                Node node = new Node();
                node.node = Graph[i].node;
                node.connections = Graph[i].GetConnections();
                graph.Add(node);
            }
            return graph;
        }

    }
}