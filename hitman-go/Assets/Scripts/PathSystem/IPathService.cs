using System.Collections.Generic;
using Common;
using UnityEngine;

namespace PathSystem
{
    public interface IPathService
    {
        Node GetNode(Node _currentNode, Directions _dir);

        List<Node> GetShortestPath(Node _currentNode, Node _destinationNode);
        void DrawGraph(); 
    }
}