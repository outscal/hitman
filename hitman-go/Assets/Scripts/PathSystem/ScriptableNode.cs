using UnityEngine;
using Common;
namespace PathSystem
{

    [CreateAssetMenu(fileName = "ScriptableNode", menuName = "Custom Objects/Graph/ScriptableNode", order = 0)]
    public class ScriptableNode : ScriptableObject
    {
        public NodeData node;
        public ScriptableNode[] Connections=new ScriptableNode[4];
    }



}