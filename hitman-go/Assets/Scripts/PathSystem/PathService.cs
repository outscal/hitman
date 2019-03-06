using Common;
using System.Collections.Generic;
using UnityEngine;
using CameraSystem;

namespace PathSystem
{
    public class PathService : IPathService
    {
        PathController controller;
        public void DrawGraph(ScriptableGraph Graph)
        {
            controller = new PathController(Graph,this);
            controller.DrawGraph(Graph);
        }
        public void KeyCollected(KeyTypes key){
            controller.KeyCollected(key);
        }
        public KeyTypes GetKeyType(int node){
            return controller.GetKeyType(node);
        }
        public void DestroyPath()
        {
            if (controller != null)
            {
                controller.DestroyPath();
                controller = null;
            }
        }
        public List<StarData> GetStarsForLevel()
        {
            return controller.GetStarsForLevel();
        }
        public void ShowThrowableNodes(int nodeId)
        {
            controller.ShowThrowableNodes(nodeId);
        }
        public List<int> GetShortestPath(int _currentNode, int _destinationNode)
        {
            return controller.GetShortestPath(_currentNode, _destinationNode);
        }
        public int GetNextNodeID(int _nodeId, Directions _dir)
        {
            return controller.GetNextNodeID(_nodeId, _dir);
        }
        public Vector3 GetNodeLocation(int _nodeID)
        {
            return controller.GetNodeLocation(_nodeID);
        }
        public List<int> GetPickupSpawnLocation(InteractablePickup type)
        {
            return controller.GetPickupSpawnLocation(type);
        }
        public int GetPlayerNodeID()
        {
            return controller.GetPlayerNodeID();
        }
        public List<EnemySpawnData> GetEnemySpawnLocation(EnemyType type)
        {
            return controller.GetEnemySpawnLocation(type);
        }
        public List<int> GetAlertedNodes(int _targetNodeID)
        {
            return controller.GetAlertedNodes(_targetNodeID);
        }
        public Directions GetEnemySpawnDirection(int _nodeID)
        {
            return controller.GetEnemySpawnDirection(_nodeID);
        }
        public bool CheckForTargetNode(int _NodeID)
        {
            return controller.CheckForTargetNode(_NodeID);
        }
        public bool CanMoveToNode(int playerNode, int destinationNode)
        {
            return controller.CanMoveToNode(playerNode, destinationNode);
        }
        public bool ThrowRange(int playerNode, int destinationNode)
        {
            return controller.ThrowRange(playerNode, destinationNode);
        }
        public Directions GetDirections(int sourceNode, int nextNode)
        {
            return controller.GetDirections(sourceNode, nextNode);

        }
        public bool CheckIfSnipeable(int nodeId)
        {
            return controller.CheckIfSnipeable(nodeId);
        }
            public List<CameraScriptableObj> GetCameraDataList()
        {
            return controller.GetCameraScriptableObject();
        }
    }
}