using System.Collections.Generic;
using Common;
using PathSystem.NodesScript;
using UnityEngine;
namespace PathSystem
{
    [CreateAssetMenu(fileName = "ScriptableGraph", menuName = "Custom Objects/Graph/ScriptableGraph", order = 0)]
    public class ScriptableGraph : ScriptableObject
    {
        public GameObject line;
        public NodeControllerView nodeprefab,targetNode;       
        public List<Node> graph =new List<Node>();

    }
}