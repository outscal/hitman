using System.Collections.Generic;
using Common;
using PathSystem.NodesScript;
using UnityEngine;

namespace PathSystem
{
    public class PathController
    {
        PathView view;
        List<int> shortestPath;
        int shortestPathLength;
        List<StarTypes> Stars;
        [SerializeField] List<Node> graph = new List<Node>();
        public PathController(ScriptableGraph Graph){
            view = new PathView();
            for (int i = 0; i < Graph.Graph.Count; i++)
            {
                Node node = new Node();
                node.node = Graph.Graph[i].node;
                node.connections = Graph.Graph[i].GetConnections();
                graph.Add(node);
            }
            DrawGraph(Graph);
        }
        public void DrawGraph(ScriptableGraph Graph)
        {
            Stars = new List<StarTypes>(Graph.stars);
            shortestPathLength = view.DrawGraph(Graph);
        }
        public List<StarTypes> GetStarsForLevel(){return Stars;}
        public void DestroyPath()
        {
            view.DestroyPath();
            view = null;
        }
        public List<int> GetShortestPath(int _currentNode, int _destinationNode)
        {
            shortestPath = new List<int>();
            shortestPathLength = graph.Count;
            //Debug.Log("is" + shortestPathLength);
            printAllPaths(_currentNode, _destinationNode);
            //Debug.Log("Shortest Path Length is" + shortestPath.Count);
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

        public void ShowAlertedNodes(int nodeId){view.ShowAlertedNodes(nodeId);}
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
        public Directions GetEnemySpawnDirection(int _nodeID){return graph[_nodeID].node.spawnEnemies[0].dir;}
        public bool CheckForTargetNode(int _NodeID){return graph[_NodeID].node.property == NodeProperty.TARGETNODE;}
        public bool CanMoveToNode(int playerNode, int destinationNode){return (graph[playerNode].connections[0] == destinationNode || graph[playerNode].connections[1] == destinationNode || graph[playerNode].connections[2] == destinationNode || graph[playerNode].connections[3] == destinationNode);}
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
        public void ShowThrowableNodes(int nodeId){view.ShowThrowableNodes(nodeId);}
        public int GetNextNodeID(int _nodeId, Directions _dir){return graph[_nodeId].connections[(int)_dir];}
        public Vector3 GetNodeLocation(int _nodeID){return graph[_nodeID].node.nodePosition;}
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
            int playerNode = -1;
            for (int i = 0; i < graph.Count; i++)
            {
                if (graph[i].node.property == NodeProperty.SPAWNPLAYER)
                {
                    playerNode = graph[i].node.uniqueID;
                    break;
                }
            }
            Debug.Log(playerNode);
            return playerNode;
        }
        public List<int> GetEnemySpawnLocation(EnemyType type)
        {
            List<int> enemySpawnNode = new List<int>();
            for (int i = 0; i < graph.Count; i++)
            {
                if (graph[i].ContainsEnemyType(type))
                {
                    enemySpawnNode.Add(graph[i].node.uniqueID);
                }
            }
            return enemySpawnNode;
        }
        public Directions GetDirections(int sourceNode, int nextNode)
        {
            Debug.Log("source is" + sourceNode + " dest is" + nextNode);
            if (graph[sourceNode].connections[0] == nextNode){return Directions.UP;}
            else if (graph[sourceNode].connections[1] == nextNode){return Directions.DOWN;}
            else if (graph[sourceNode].connections[2] == nextNode){return Directions.LEFT;}
            else{return Directions.RIGHT;}
        }
    }
}