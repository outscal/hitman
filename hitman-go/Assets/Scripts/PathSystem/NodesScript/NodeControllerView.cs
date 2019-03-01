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
        GameObject highlightnode;
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
            high.transform.localScale = Vector3.zero;
            iTween.ScaleTo(high, iTween.Hash
            (
                "x", 12,
                "z", 12,
                "time", 2f,
                "easetype", iTween.EaseType.easeOutBounce
            ));
            await Task.Delay(TimeSpan.FromSeconds(3));
            Destroy(high);
        }
        public void HighlightNode()
        {
            highlightnode = Instantiate(highlighter, transform.position, Quaternion.identity, transform);
        }
        public void UnHighlightNode()
        {
            Destroy(highlightnode);
        }
    }
}