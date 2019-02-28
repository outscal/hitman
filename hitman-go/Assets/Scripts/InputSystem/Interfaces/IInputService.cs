using System;
using Common;

namespace InputSystem
{
    public interface IInputService
    {
        void PassDirection(Directions direction);
        void PassNodeID(int nodeID);
        ISwipeDirection GetSwipeDirection();
    }
}
