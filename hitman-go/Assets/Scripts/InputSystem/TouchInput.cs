using UnityEngine;
using Zenject;
using Common;

namespace InputSystem
{
    public class TouchInput : IInputComponent
    {
        private IInputService inputService;
        private Vector2 startClickPos, endCLickPos;
        private Directions direction;

        public TouchInput()
        {

        }

        public void OnInitialized(IInputService inputService)
        {
            this.inputService = inputService;
        }

        public void OnTick()
        {
            if (Input.GetMouseButtonDown(0))
                startClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonUp(0))
            {
                endCLickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                DecideSwipe();
            }

        }

        void DecideSwipe()
        {
            Vector2 value = endCLickPos - startClickPos;

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