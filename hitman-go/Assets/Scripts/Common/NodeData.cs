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
        public bool TargetNode;
        public List<InteractablePickup> spawnPickups;
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
    public class Node
    {
        public NodeData node;
        public int[] connections = new int[4];

        public bool ContainsEnemyType(EnemyType type){
            for(int i=0;i<node.spawnEnemies.Count;i++){
                if(node.spawnEnemies[i].enemy==type){
                    return true;
                }
            }
            return false;
        }
    }
}