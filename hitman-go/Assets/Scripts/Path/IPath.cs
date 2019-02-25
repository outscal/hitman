using System.Collections.Generic;
using Scripts;
using UnityEngine;

namespace hitman_go.Assets.Scripts.Path
{
    public interface IPath
    {
        Node GetNode(Node currentNode, Directions dir);
        List<Node> GetShortestPath(Node currentNode, Node destinstionNode);
    }
}