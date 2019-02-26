using UnityEngine;
using System.Collections;
using Common;
using System.Collections.Generic;

namespace PathSystem
{
    public class PathService : IPathService
    {
        public int GetNextNodeID(int _nodeID, Directions _dir)
        {
            throw new System.NotImplementedException();
        }

      

        public Vector3 GetNodeLocation(int _nodeID)
        {
            throw new System.NotImplementedException();
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