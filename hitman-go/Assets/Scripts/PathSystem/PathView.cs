using System.Collections.Generic;
using Common;
using PathSystem.NodesScript;
using UnityEngine;

namespace PathSystem
{
    public class PathView
    {
        //List<int> shortestPath;
        GameObject line;
        List<GameObject> physicalHighlightedNodes = new List<GameObject>();
        List<GameObject> physicalPath = new List<GameObject>();
        List<GameObject> physicalNode = new List<GameObject>();
        NodeControllerView nodeprefab, targetNode;
        // int shortestPathLength;
        //List<StarTypes> Stars;
        [SerializeField] List<Node> graph = new List<Node>();
        public int DrawGraph(ScriptableGraph Graph)
        {
            nodeprefab = Graph.nodeprefab;
            targetNode = Graph.targetNode;
            line = Graph.line;
            for (int i = 0; i < Graph.Graph.Count; i++)
            {
                Node node = new Node();
                node.node = Graph.Graph[i].node;
                node.connections = Graph.Graph[i].GetConnections();
                graph.Add(node);

                if (graph[i].node.property == NodeProperty.TARGETNODE)
                {
                    targetNode.SetNodeID(i);
                    physicalNode.Add(GameObject.Instantiate(targetNode.gameObject, new Vector3(node.node.nodePosition.x, node.node.nodePosition.y - 0.195f, node.node.nodePosition.z), Quaternion.identity));
                }
                else
                {
                    nodeprefab.SetNodeID(i);
                    physicalNode.Add(GameObject.Instantiate(nodeprefab.gameObject, new Vector3(node.node.nodePosition.x, node.node.nodePosition.y - 0.195f, node.node.nodePosition.z), Quaternion.identity));
                }
                if (node.connections[0] != -1)
                {
                    physicalPath.Add(GameObject.Instantiate(line, new Vector3(node.node.nodePosition.x, node.node.nodePosition.y - 0.195f, node.node.nodePosition.z - 2.5f), Quaternion.Euler(new Vector3(0, 90, 0))));
                }
                if (node.connections[2] != -1)
                {
                    physicalPath.Add(GameObject.Instantiate(line, new Vector3(node.node.nodePosition.x + 2.5f, node.node.nodePosition.y - 0.195f, node.node.nodePosition.z), new Quaternion(0, 0, 0, 0)));
                }
            }
            return graph.Count;

        }
         public void DestroyPath()
        {
            graph = new List<Node>();
            for (int i = 0; i < physicalPath.Count; i++)
            {
                GameObject.DestroyImmediate(physicalPath[i]);
            }
            for (int i = 0; i < physicalNode.Count; i++)
            {
                GameObject.DestroyImmediate(physicalNode[i]);
            }
            physicalPath = new List<GameObject>();
            physicalNode = new List<GameObject>();
        }
        public void ShowAlertedNodes(int nodeId)
        {
            for (int i = 0; i < physicalHighlightedNodes.Count; i++)
            {
                physicalHighlightedNodes[i].GetComponent<NodeControllerView>().UnHighlightNode();
            }
            physicalHighlightedNodes = new List<GameObject>();
            physicalNode[nodeId].GetComponent<NodeControllerView>().ShowAlertedNodes();
        }
         public void ShowThrowableNodes(int nodeId)
        {
            Vector3 playerpos = graph[nodeId].node.nodePosition;
            for (int i = 0; i < graph.Count; i++)
            {
                Vector3 testpos = graph[i].node.nodePosition;
                if ((playerpos.x + 5 == testpos.x || playerpos.x - 5 == testpos.x) && playerpos.z == testpos.z)
                {
                    physicalHighlightedNodes.Add(physicalNode[i]);
                    physicalNode[i].GetComponent<NodeControllerView>().HighlightNode();
                }
                if ((playerpos.z + 5 == testpos.z || playerpos.z - 5 == testpos.z) && playerpos.x == testpos.x)
                {
                    physicalHighlightedNodes.Add(physicalNode[i]);
                    physicalNode[i].GetComponent<NodeControllerView>().HighlightNode();
                }
            }
        }
    }
}