using UnityEngine;
using System;
namespace Common
{ 
    [Serializable]
    public struct Edge
    {
        public Node NodeID1, NodeID2;
    }

    [Serializable]
    public struct Node
    {
        public int uniqueID;
        public Vector3 nodePosition;
    }

}