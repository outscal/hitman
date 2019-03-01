using System;
using Common;
using UnityEngine;
using CameraSystem;

namespace InputSystem
{
    public interface IInputService
    {
        void PassDirection(Directions direction);
        void PassNodeID(int nodeID);
        ISwipeDirection GetSwipeDirection();
        ITapDetect GetTapDetect();
        ICameraManager GetCameraManager();
    }
}
