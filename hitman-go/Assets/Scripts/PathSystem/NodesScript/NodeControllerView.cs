using PathSystem.NodesScript.Interface;
using UnityEngine;
namespace PathSystem.NodesScript
{
    public class NodeControllerView : MonoBehaviour, INodeControllerView
    {
        public int nodeID;
        public int GetNodeId()
        {
            return nodeID;
        }
        public void SetNodeID(int ID){
            nodeID=ID;
        }
    }
}