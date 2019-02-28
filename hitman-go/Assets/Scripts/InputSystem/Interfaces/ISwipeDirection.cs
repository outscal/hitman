using UnityEngine;
using Common;

namespace InputSystem
{
    public interface ISwipeDirection
    {
        Directions GetDirection(Vector2 startPos, Vector2 endPos);
    }
}