using System;
using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine;

namespace PathSystem
{
    class PathService : MonoBehaviour, IPathService
    {
        public GameObject nodeprefab, line;
        [SerializeField] ScriptableGraph Graph;
        [SerializeField] List<Node> graph = new List<Node>();
        private void Start()
        {
            for (int i = 0; i < Graph.graph.Count; i++)
            {
                Node node = new Node();
                node.node = Graph.graph[i].node;
                node.connections = Graph.graph[i].connections;
                graph.Add(node);
                GameObject.Instantiate(nodeprefab, node.node.nodePosition, Quaternion.identity);
                if (node.connections[0] != -1)
                {
                    GameObject.Instantiate(line, new Vector3(node.node.nodePosition.x , node.node.nodePosition.y, node.node.nodePosition.z- 2.5f), Quaternion.Euler( new Vector3(0,90,0)));
                }
                if (node.connections[2] != -1)
                {
                    GameObject.Instantiate(line, new Vector3(node.node.nodePosition.x + 2.5f, node.node.nodePosition.y, node.node.nodePosition.z), new Quaternion(0, 0, 0, 0));
                }
            }
        }
        public void DrawGraph()
        {

        }

        public Node GetNode(Node _currentNode, Directions _dir)
        {
            throw new System.NotImplementedException();
        }

        public List<Node> GetShortestPath(Node _currentNode, Node _destinationNode)
        {
            throw new System.NotImplementedException();
        }
    }
}