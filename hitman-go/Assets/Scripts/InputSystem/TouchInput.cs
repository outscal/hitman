using UnityEngine;
using Zenject;
using Common;

namespace InputSystem
{
    public class TouchInput : IInputComponent
    {
        private IInputService inputService;
        private Vector2 startPos, endPos;
        private Directions direction;
        private float minDragDistance = Screen.height * 15 / 100;

        public TouchInput()
        {

        }

        public void OnInitialized(IInputService inputService)
        {
            this.inputService = inputService;
        }

        public void OnTick()
        {
            if(Input.touchCount >= 1)
            {
                Touch touch = Input.GetTouch(0);

                if(touch.phase == TouchPhase.Began)
                {
                    startPos = touch.position;
                    endPos = touch.position; 
                }
                else if(touch.phase == TouchPhase.Ended)
                {
                    endPos = touch.position;
                    DecideSwipe();
                }
            }

        }

        void DecideSwipe()
        {
            Vector2 value = endPos - startPos;
            if (Mathf.Abs(value.x) >= minDragDistance || Mathf.Abs(value.y) >= minDragDistance)
            {
                //x axis swipe
                if (Mathf.Abs(value.x) > Mathf.Abs(value.y))
                {
                    if (value.x < 0)
                    {
                        Debug.Log("[InputComponent] SwipeLeft");
                        direction = Directions.LEFT;
                    }
                    else if (value.x > 0)
                    {
                        Debug.Log("[InputComponent] SwipeRight");
                        direction = Directions.RIGHT;
                    }
                }//y axis swipe
                else if (Mathf.Abs(value.x) < Mathf.Abs(value.y))
                {
                    if (value.y < 0)
                    {
                        Debug.Log("[InputComponent] SwipeDown");
                        direction = Directions.DOWN;
                    }
                    else if (value.y > 0)
                    {
                        Debug.Log("[InputComponent] SwipeUp");
                        direction = Directions.UP;
                    }
                }

                inputService.PassDirection(direction);
            }
        }
    }
}