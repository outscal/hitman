using UnityEngine;
using Common;
using PathSystem.NodesScript;
using Player;

namespace InputSystem
{
    public class TouchInput : IInputComponent
    {
        private IInputService inputService;
        private Vector2 startPos, endPos;
        private Directions direction;
        private int nodeLayer = 1 << 9;
        GameObject gameObject;

        public TouchInput()
        {
            Debug.Log("<color=green>[TouchInput] Created:</color>");
        }

        public void OnInitialized(IInputService inputService)
        {
            this.inputService = inputService;
        }

        public void OnTick()
        {
            if (Input.touchCount >= 1)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    gameObject = inputService.GetTapDetect().ReturnObject(touch.position, nodeLayer);
                    if (gameObject != null)
                    {
                        if (gameObject.GetComponent<NodeControllerView>() != null)
                        {
                            inputService.PassNodeID(gameObject.GetComponent<NodeControllerView>().nodeID);
                        }
                        else
                        {
                            startPos = touch.position;
                            endPos = touch.position;
                        }
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    if (gameObject != null)
                    {
                        if (gameObject.GetComponent<PlayerView>() != null)
                        {
                            endPos = touch.position;

                            inputService.PassDirection(inputService.GetSwipeDirection()
                            .GetDirection(startPos, endPos));
                            gameObject = null;
                        }
                    }
                }

            }
        }

        public void StartPosition(Vector3 pos)
        {
            //startPos = pos;
        }
    }
}