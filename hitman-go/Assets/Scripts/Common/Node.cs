using System;
using System.Collections.Generic;

namespace Common
{
    [Serializable]
    public class Node
    {
        
        public NodeData node;
        public int[] connections = new int[4];
        public List<int> teleport = new List<int>();
        public List<EnemySpawnData> GetEnemyType(EnemyType type)
        {
            List<EnemySpawnData> enemy = new List<EnemySpawnData>();

            for (int i = 0; i < node.spawnEnemies.Count; i++)
            {
                
                if (node.spawnEnemies[i].enemy == type)
                {
                    EnemySpawnData data = new EnemySpawnData();
                    data.node = node.uniqueID;
                    data.dir = node.spawnEnemies[i].dir;
                    data.hasShield = node.spawnEnemies[i].hasShield;
                    enemy.Add(data);
                }
            }
            return enemy;
        }

    }
}