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
        List<int> shortestPath;
        [SerializeField] ScriptableGraph Graph;
        [SerializeField] List<Node> graph = new List<Node>();
        private void Start()
        {
            DrawGraph();
        }
        public void DrawGraph()
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
                    GameObject.Instantiate(line, new Vector3(node.node.nodePosition.x, node.node.nodePosition.y, node.node.nodePosition.z - 2.5f), Quaternion.Euler(new Vector3(0, 90, 0)));
                }
                if (node.connections[2] != -1)
                {
                    GameObject.Instantiate(line, new Vector3(node.node.nodePosition.x + 2.5f, node.node.nodePosition.y, node.node.nodePosition.z), new Quaternion(0, 0, 0, 0));
                }
            }
            GetShortestPath(0, 3);
        }
        public Node GetNode(int _nodeId, Directions _dir)
        {
            return graph[graph[_nodeId].connections[(int)_dir]];
        }
        private void printAllPaths(int s, int d)
        {
            bool[] isVisited = new bool[graph.Count];
            List<int> pathList = new List<int>();
            pathList.Add(s);
            printAllPathsUtil(s, d, isVisited, pathList);
        }
        private void printAllPathsUtil(int u, int d, bool[] isVisited, List<int> localPathList)
        {
            int shortPathLength=graph.Count;
            isVisited[u] = true;
            if (u.Equals(d))
            {
                if(localPathList.Count<shortPathLength){
                    shortestPath=localPathList;
                }
                isVisited[u] = false;
                return;
            }
            foreach (int i in graph[u].connections)
            {
                if (i != -1)
                {
                    if (!isVisited[i])
                    {
                        localPathList.Add(i);
                        printAllPathsUtil(i, d, isVisited, localPathList);
                        localPathList.Remove(i);
                    }
                }
            }
            isVisited[u] = false;
        }
        public void GetShortestPath(int _currentNode, int _destinationNode)
        {
            printAllPaths(_currentNode, _destinationNode);
            Debug.Log("Shortest Path Length is"+shortestPath.Count);
        }
    }
}