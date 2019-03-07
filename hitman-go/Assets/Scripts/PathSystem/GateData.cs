using UnityEngine;
using Common;
using System;

namespace PathSystem
{
    [Serializable]
    public class GateData
    {
        public KeyTypes key;
        public int node1, node2;
        public Vector3 position;
        public GameObject gatePrefab;
    }
}