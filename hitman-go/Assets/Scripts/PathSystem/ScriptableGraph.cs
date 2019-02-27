using System.Collections.Generic;
using Common;
using UnityEngine;
namespace PathSystem
{
    [CreateAssetMenu(fileName = "ScriptableGraph", menuName = "Custom Objects/Graph/ScriptableGraph", order = 0)]
    public class ScriptableGraph : ScriptableObject
    {
        public GameObject nodeprefab, line;       
        public List<Node> graph =new List<Node>();

    }
}