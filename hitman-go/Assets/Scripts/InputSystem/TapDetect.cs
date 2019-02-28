using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathSystem.NodesScript;
namespace InputSystem
{
    public class TapDetect : ITapDetect
    {
        public GameObject ReturnObject(Vector2 position, LayerMask layerMask)
        {
            GameObject gameObject = null;
            Ray ray = Camera.main.ScreenPointToRay(position);
            RaycastHit raycast;
            if (Physics.Raycast(ray, out raycast, Mathf.Infinity, layerMask))
            {
                gameObject = raycast.collider.gameObject;
                Debug.Log("[TapDetect] GameObject:" + gameObject.GetComponent<NodeControllerView>().GetNodeId());
            }

            return gameObject;
        }
    }
}