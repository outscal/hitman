using Common;
using System.Collections.Generic;
using UnityEngine;
using CameraSystem;

namespace PathSystem
{
    public interface IPathService
    {
        List<StarTypes> GetStarsForLevel();
        Directions GetDirections(int sourceNode, int nextNode);
        void ShowThrowableNodes(int nodeId);
        bool ThrowRange(int playerNode,int destinationNode);
        bool CanMoveToNode(int playerNode,int destinationNode);
        int GetNextNodeID(int _nodeID, Directions _dir);
        Vector3 GetNodeLocation(int _nodeID);
        List<int> GetPickupSpawnLocation(InteractablePickup type);
        int GetPlayerNodeID();
        List<int> GetEnemySpawnLocation(EnemyType type);
        List<int> GetShortestPath(int _currentNodeID, int _destinationNodeID);
        List<int> GetAlertedNodes(int _targetNodeID);
        Directions GetEnemySpawnDirection(int _nodeID);
        bool CheckForTargetNode(int _NodeID);
        void DestroyPath();
        void DrawGraph(ScriptableGraph Graph);
        //CameraScriptableObj GetCameraData();
    }
}