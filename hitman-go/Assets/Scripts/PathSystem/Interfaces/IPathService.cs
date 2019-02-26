using Common;
using System.Collections.Generic;
using UnityEngine;

namespace PathSystem
{
    public interface IPathService
    {
        int GetNextNodeID(int _nodeID, Directions _dir);
        Vector3 GetNodeLocation(int _nodeID);

        List<Vector3> GetSpawnLocation(EnemyType type);

        List<Vector3> GetShortestPath(int _currentNodeID, int _destinationNodeID);
    }
}