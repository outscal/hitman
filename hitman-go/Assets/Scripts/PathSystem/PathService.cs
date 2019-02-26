using UnityEngine;
using System.Collections;
using Common;
using System.Collections.Generic;

namespace PathSystem
{
    public class PathService : IPathService
    {
       

        public int GetNode(int _nodeID, Directions _dir)
        {
            return 1;
        }
      

        public List<Vector3> GetShortestPath(int _currentNodeID, int _destinationNodeID)
        {
            List < Vector3 > temp= new List<Vector3>();
            return temp;
        }

        public List<Vector3> GetSpawnLocation(EnemyType type)
        {
            throw new System.NotImplementedException();
        }
    }
}