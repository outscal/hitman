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
        public EnemyType disguise;
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
        public bool hasShield;
    }
    public struct EnemySpawnData
    {
        public Directions dir;
        public int node;
        public bool hasShield;
    }

    [Serializable]
    public class StarData
    {
        public StarTypes type;
        public String name;
    }
    [Serializable]
    public class cirPath
    {
        public List<int> path;
    }

}