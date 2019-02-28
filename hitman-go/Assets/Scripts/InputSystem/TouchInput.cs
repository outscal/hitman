using UnityEngine;
using Common;

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

        public void DetectTap()
        {
            throw new System.NotImplementedException();
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