using UnityEngine;

namespace Scripts
{
    public struct Edge
    {
        public Node NodeId1,NodeId2;
    }
    public struct Node{
        public int uniqueId; 
        public Vector3 nodePosition;
    }

}