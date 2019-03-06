using UnityEngine;
using System;
using System.Collections.Generic;

namespace Common
{
    [Serializable]
    public struct NodeData
    {
        public bool snipeable;
        public KeyTypes keyType;
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
    public struct EnemySpawnData
    {
        public Directions dir;
        public int node;
    }

    [Serializable]
    public class StarData
    {
        public StarTypes type;
        public String name;
    }
    
}