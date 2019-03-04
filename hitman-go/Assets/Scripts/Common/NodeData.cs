using UnityEngine;
using System;
using System.Collections.Generic;

namespace Common
{
    public enum NodeProperty
    {
        NONE,
        SPAWNPLAYER,
        TARGETNODE

    }
    [Serializable]
    public struct Edge
    {
        public Node NodeID1, NodeID2;
    }

    [Serializable]
    public struct NodeData
    {
        public int uniqueID;
        public NodeProperty property;
        public InteractablePickup spawnPickups;
        public List<NodeEnemyData> spawnEnemies;
        public Vector3 nodePosition;
    }
    [Serializable]
    public class NodeEnemyData
    {
        public EnemyType enemy;
        public Directions dir;
    }

    [Serializable]
    public class LevelData
    {
        public StarTypes type;
        public int bound;
    }

    [Serializable]
    public class Node
    {
        public NodeData node;
        public int[] connections = new int[4];
        public List<int> teleport = new List<int>();
        public bool ContainsEnemyType(EnemyType type)
        {
            for (int i = 0; i < node.spawnEnemies.Count; i++)
            {
                if (node.spawnEnemies[i].enemy == type)
                {
                    return true;
                }
            }
            return false;
        }
    }
    [Serializable]
    public class ScriptableNode
    {
        public NodeData node;
        public int up = -1, down = -1, left = -1, right = -1;
        public List<int> teleport = new List<int>();
        public ScriptableNode()
        {
            up = -1; down = -1; left = -1; right = -1;
        }
        public int[] GetConnections()
        {
            return new int[4] { up, down, left, right };
        }
    }
}