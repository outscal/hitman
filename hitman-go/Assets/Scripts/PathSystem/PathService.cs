using UnityEngine;
using System.Collections;
using Common;
using System.Collections.Generic;

namespace PathSystem
{
    public class PathService : IPathService
    {
        public List<Vector3> GetEnemySpawnLocation(EnemyType type)
        {
            // throw new System.NotImplementedException();
            List<Vector3> temp = new List<Vector3>();
            return temp;
        }

        public int GetNextNodeID(int _nodeID, Directions _dir)
        {
            //throw new System.NotImplementedException();
            return 1;
        }
      

        public Vector3 GetNodeLocation(int _nodeID)
        {
            // throw new System.NotImplementedException();
            Vector3 temp = Vector3.zero;
            return temp;
        }

        public List<Vector3> GetPickupSpawnLocation(InteractablePickup type)
        {
            //   throw new System.NotImplementedException();
            List<Vector3> temp = new List<Vector3>();
                return temp;

        }

        public int GetPlayerNodeID()
        {
            // throw new System.NotImplementedException();
            return 1;

        }

        public List<int> GetShortestPath(int _currentNodeID, int _destinationNodeID)
        {
            List < int > temp= new List<int>();
            return temp;
        }

        
    }
}