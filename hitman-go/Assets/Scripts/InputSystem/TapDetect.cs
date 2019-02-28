using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    public class TapDetect : ITapDetect
    {
        public GameObject ReturnObject(Vector2 position)
        {
            GameObject gameObject = null;
            Ray ray = Camera.main.ScreenPointToRay(position);
            RaycastHit raycast;
            if(Physics.Raycast(ray,out raycast))
            {
                gameObject = raycast.collider.gameObject;
            }

            return gameObject;
        }
    }
}