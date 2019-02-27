using Common;
using System.Collections.Generic;
using UnityEngine;

namespace PathSystem
{
    public interface IPathService
    {
        int GetNextNodeID(int _nodeID, Directions _dir);

        Vector3 GetNodeLocation(int _nodeID);

        List<int> GetPickupSpawnLocation(InteractablePickup type);

        int GetPlayerNodeID();

        List<int> GetEnemySpawnLocation(EnemyType type);


        List<int> GetShortestPath(int _currentNodeID, int _destinationNodeID);

        List<int> GetAlertedNodes(int _targetNodeID);
        Directions GetEnemySpawnDirection(int _nodeID);
 bool CheckForTargetNode(int _NodeID);    }
}