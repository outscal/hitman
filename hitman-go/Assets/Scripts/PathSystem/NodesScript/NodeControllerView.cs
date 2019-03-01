using PathSystem.NodesScript.Interface;
using UnityEngine;
using System.Threading.Tasks;
using System;
namespace PathSystem.NodesScript
{
    public class NodeControllerView : MonoBehaviour, INodeControllerView
    {
        public GameObject highlighter;
        public int nodeID;
        public int GetNodeId()
        {
            return nodeID;
        }
        public void SetNodeID(int ID)
        {
            nodeID = ID;
        }
        public async void ShowAlertedNodes()
        {
            GameObject high = Instantiate(highlighter, transform.position, Quaternion.identity, transform);
            await Task.Delay(TimeSpan.FromSeconds(2));
            Destroy(high);
        }
    }
}