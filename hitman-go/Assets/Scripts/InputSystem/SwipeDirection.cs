using Common;
using UnityEngine;

namespace InputSystem
{
    public class SwipeDirection : ISwipeDirection
    {
        private float minDragDistance = Screen.height * 15 / 100;
        Directions direction;

        public Directions GetDirection(Vector2 startPos, Vector2 endPos)
        {
            Vector2 value = endPos - startPos;
            if (Mathf.Abs(value.x) >= minDragDistance || Mathf.Abs(value.y) >= minDragDistance)
            {
                //x axis swipe
                if (Mathf.Abs(value.x) > Mathf.Abs(value.y))
                {
                    if (value.x < 0)
                    {
                        Debug.Log("[SwipeDirection] SwipeLeft");
                        direction = Directions.LEFT;
                    }
                    else if (value.x > 0)
                    {
                        Debug.Log("[SwipeDirection] SwipeRight");
                        direction = Directions.RIGHT;
                    }
                }//y axis swipe
                else if (Mathf.Abs(value.x) < Mathf.Abs(value.y))
                {
                    if (value.y < 0)
                    {
                        Debug.Log("[SwipeDirection] SwipeDown");
                        direction = Directions.DOWN;
                    }
                    else if (value.y > 0)
                    {
                        Debug.Log("[SwipeDirection] SwipeUp");
                        direction = Directions.UP;
                    }
                }
            }
            return direction;
        }

    }
}