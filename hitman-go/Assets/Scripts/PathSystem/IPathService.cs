using System.Collections.Generic;
using Common;
using UnityEngine;

namespace PathSystem
{
    public interface IPathService
    {
        int GetNextNodeID(int _nodeID, Directions _dir);
        Vector3 GetNodeLocation(int _nodeID);

        List<Node> GetShortestPath(Node _currentNode, Node _destinationNode);
    }
}