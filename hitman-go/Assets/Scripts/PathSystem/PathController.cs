using System.Collections.Generic;
using Common;
using PathSystem.NodesScript;
using UnityEngine;
using System.Linq;
using CameraSystem;
namespace PathSystem
{
    public class PathController
    {
        PathView view;
        List<int> shortestPath;
        List<GateData> gates;
        int shortestPathLength;
        List<StarData> Stars;
        List<List<int>> circularPath = new List<List<int>>();
        List<CameraScriptableObj> cameraList;
        [SerializeField] List<Node> graph = new List<Node>();
        public PathController(ScriptableGraph Graph, IPathService pathService)
        {
            view = new PathView(pathService);
            cameraList = Graph.cameraScriptableList;
            for (int i = 0; i < Graph.circularPath.Count; i++)
            {
                circularPath.Add(Graph.circularPath[i].path);
            }
            for (int i = 0; i < Graph.Graph.Count; i++)
            {
                Node node = new Node();
                node.node = Graph.Graph[i].node;
                node.teleport = Graph.Graph[i].teleport;
                node.connections = Graph.Graph[i].GetConnections();
                graph.Add(node);
            }
        }
        public List<int> GetOriginalPath(int ID)
        {
            return circularPath[ID];
        }
        public void DrawGraph(ScriptableGraph Graph)
        {
            gates = Graph.GatesEdge;
            Stars = new List<StarData>(Graph.stars);
            shortestPathLength = view.DrawGraph(Graph);
        }
        public List<StarData> GetStarsForLevel() { return Stars; }
        public void DestroyPath()
        {
            view.DestroyPath();
            view = null;
        }
        public void KeyCollected(KeyTypes key)
        {
            for (int i = 0; i < gates.Count; i++)
            {
                if (gates[i].key == key)
                {

                    int dir = -1, node = -1, setdir1 = -1, setdir2 = -1;
                    if (GetNodeLocation(gates[i].node1).x < GetNodeLocation(gates[i].node2).x)
                    {
                        dir = 2;
                        setdir1 = 2;
                        setdir2 = 3;
                        node = gates[i].node1;
                    }
                    if (GetNodeLocation(gates[i].node1).z > GetNodeLocation(gates[i].node2).z)
                    {
                        dir = 0;
                        setdir2 = 1;
                        setdir1 = 0;
                        node = gates[i].node1;
                    }
                    if (GetNodeLocation(gates[i].node2).z > GetNodeLocation(gates[i].node1).z)
                    {
                        dir = 0;
                        setdir2 = 0;
                        setdir1 = 1;
                        node = gates[i].node2;
                    }
                    if (GetNodeLocation(gates[i].node2).x < GetNodeLocation(gates[i].node1).x)
                    {
                        dir = 2;
                        setdir2 = 2;
                        setdir1 = 3;
                        node = node = gates[i].node2;
                    }
                    
                    graph[gates[i].node1].connections[setdir1] = gates[i].node2;
                    graph[gates[i].node2].connections[setdir2] = gates[i].node1;
                    view.DrawPath(dir, GetNodeLocation(node));
                   

                }
            }
            view.KeyCollected(key);
        }
        public List<int> GetShortestPath(int _currentNode, int _destinationNode)
        {
            shortestPath = new List<int>();
            shortestPathLength = graph.Count;
            
            printAllPaths(_currentNode, _destinationNode);
            
            return shortestPath;
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
            isVisited[u] = true;
            if (u.Equals(d))
            {
                if (localPathList.Count < shortestPathLength)
                {
                    int[] shortest = new int[localPathList.Count];
                    localPathList.CopyTo(shortest);
                    shortestPath = new List<int>(shortest);
                    shortestPathLength = localPathList.Count;
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
        public bool CheckIfSnipeable(int nodeId)
        {
            return graph[nodeId].node.snipeable;
        }

        public void ShowAlertedNodes(int nodeId) { view.ShowAlertedNodes(nodeId); }
        public List<int> GetAlertedNodes(int _targetNodeID)
        {
            ShowAlertedNodes(_targetNodeID);

            Vector3 tnode = graph[_targetNodeID].node.nodePosition;
            List<int> alerted = new List<int>();
            for (int i = 0; i < graph.Count; i++)
            {
                Vector3 node = graph[i].node.nodePosition;
                if ((tnode.x + 6 > node.x && node.x > tnode.x - 6) && (tnode.z + 6 > node.z && node.z > tnode.z - 6))
                {
                    alerted.Add(i);
                }
            }
            return alerted;
        }
        public Directions GetEnemySpawnDirection(int _nodeID) { return graph[_nodeID].node.spawnEnemies[0].dir; }
        public bool CheckForTargetNode(int _NodeID) { return graph[_NodeID].node.property == NodeProperty.TARGETNODE; }
        bool CheckTeleportable(int playerNode, int destinationNode) { return graph[playerNode].teleport.Contains(destinationNode); }
        public bool CanMoveToNode(int playerNode, int destinationNode)
        {
            if ((graph[playerNode].connections[0] == destinationNode || graph[playerNode].connections[1] == destinationNode || graph[playerNode].connections[2] == destinationNode || graph[playerNode].connections[3] == destinationNode))
            {
                view.Unhighlightnodes();
                if (graph[destinationNode].node.property == NodeProperty.TELEPORT)
                {
                    view.ShowTeleportableNodes(graph[destinationNode].teleport);
                }
                return true;
            }
            else
            {
                return CheckTeleportable(playerNode, destinationNode);
            }
        }

        public bool CanEnemyMoveToNode(int enemyNode, int destinationNode)
        {
            return ((graph[enemyNode].connections[0] == destinationNode || graph[enemyNode].connections[1] == destinationNode || graph[enemyNode].connections[2] == destinationNode || graph[enemyNode].connections[3] == destinationNode));
        }

        public bool ThrowRange(int playerNode, int destinationNode)
        {
            Vector3 playerpos = graph[playerNode].node.nodePosition;
            Vector3 testpos = graph[destinationNode].node.nodePosition;
            if ((playerpos.x + 5 == testpos.x || playerpos.x - 5 == testpos.x) && playerpos.z == testpos.z)
            {
                return true;
            }
            if ((playerpos.z + 5 == testpos.z || playerpos.z - 5 == testpos.z) && playerpos.x == testpos.x)
            {
                return true;
            }
            return false;
        }
        public void ShowThrowableNodes(int nodeId) { view.ShowThrowableNodes(nodeId); }
        public int GetNextNodeID(int _nodeId, Directions _dir)
        {
            int nextnode = graph[_nodeId].connections[(int)_dir];
            // CheckTeleportable(_nodeId, nextnode);
            view.Unhighlightnodes();
            if (nextnode != -1 && graph[nextnode].node.property == NodeProperty.TELEPORT)
            {
                //view.ShowTeleportableNodes(graph[nextnode].teleport);
            }

            return nextnode;
        }
        public KeyTypes GetKeyType(int node)
        {
            return graph[node].node.keyType;
        }
        public Vector3 GetNodeLocation(int _nodeID) { return graph[_nodeID].node.nodePosition; }
        public List<int> GetPickupSpawnLocation(InteractablePickup type)
        {
            List<int> pickableNodeList = new List<int>();
            for (int i = 0; i < graph.Count; i++)
            {
                if (graph[i].node.spawnPickups == type)
                {
                    pickableNodeList.Add(graph[i].node.uniqueID);
                }
            }
            return pickableNodeList;
        }
        public int GetPlayerNodeID()
        {
            return 0;
        }
        public List<CameraScriptableObj> GetCameraScriptableObject()
        {
            return cameraList;
        }
        public List<EnemySpawnData> GetEnemySpawnLocation(EnemyType type)
        {
            List<EnemySpawnData> enemySpawnNode = new List<EnemySpawnData>();
            for (int i = 0; i < graph.Count; i++)
            {
                List<EnemySpawnData> tempEnemyData = new List<EnemySpawnData>();
                tempEnemyData = graph[i].GetEnemyType(type);
                if (tempEnemyData.Count > 0)
                {
                    for (int j = 0; j < tempEnemyData.Count; j++)
                    {
                        enemySpawnNode.Add(tempEnemyData[j]);
                    }
                }
            }
            return enemySpawnNode;
        }
        public Directions GetDirections(int sourceNode, int nextNode)
        {
            
            if (graph[sourceNode].connections[0] == nextNode) { return Directions.UP; }
            else if (graph[sourceNode].connections[1] == nextNode) { return Directions.DOWN; }
            else if (graph[sourceNode].connections[2] == nextNode) { return Directions.LEFT; }
            else { return Directions.RIGHT; }
        }
        public EnemyType GetDisguise(int NodeId)
        {
            return graph[NodeId].node.disguise;
        }
    }
}