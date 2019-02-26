using UnityEngine;
using System;
using System.Collections.Generic;

namespace Common
{
    [Serializable]
    public struct Edge
    {
        public Node NodeID1, NodeID2;
    }

    [Serializable]
    public struct NodeData
    {
        public int uniqueID;
        public bool spawnPlayer;
        public List<InteractablePickup> spawnPickups;
        public List<EnemyType> spawnEnemies;
        public Vector3 nodePosition;
    }
    [Serializable]
    public class Node
    {
        public NodeData node;
        public int[] connections = new int[4];

    }
}