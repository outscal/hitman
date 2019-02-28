using UnityEngine;
using Common;
using PathSystem.NodesScript;

namespace InputSystem
{
    public class TouchInput : IInputComponent
    {
        private IInputService inputService;
        private Vector2 startPos, endPos;
        private Directions direction;

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

                GameObject gameObject = inputService.GetTapDetect().ReturnObject(touch.position);
                if (gameObject.GetComponent<NodeControllerView>() != null)
                {
                    inputService.PassNodeID(gameObject.GetComponent<NodeControllerView>().nodeID);
                }
                else
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        startPos = touch.position;
                        endPos = touch.position;
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        endPos = touch.position;

                        inputService.PassDirection(inputService.GetSwipeDirection()
                        .GetDirection(startPos, endPos));
                    }
                }
            }
        }

    }
}