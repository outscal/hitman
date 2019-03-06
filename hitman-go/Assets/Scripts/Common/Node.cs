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
}