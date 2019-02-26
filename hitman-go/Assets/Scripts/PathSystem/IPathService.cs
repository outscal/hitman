using System.Collections.Generic;
using Common;
using UnityEngine;

namespace PathSystem
{
    public interface IPathService
    {
        Node GetNode(int _nodeId, Directions _dir);


        void GetShortestPath(int currentNode, int destinationNode);
        void DrawGraph(); 
    }
}