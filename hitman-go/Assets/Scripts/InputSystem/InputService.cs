using UnityEngine;
using System;
using Common;
using Player;
using Zenject;
using PathSystem.NodesScript;
using CameraSystem;

namespace InputSystem
{
    public class InputService : IInputService, ITickable
    {
        private IPlayerService playerService;

        private IInputComponent playerInput, cameraInput;
        private ISwipeDirection swipeDirection;
        private ITapDetect tapDetect;
        private GameObject tapObject;
        private int nodeLayer = 1 << 9;
        private ICameraManager cameraManager;

        public InputService (IPlayerService playerService, ICameraManager cameraManager)
        {
            this.cameraManager = cameraManager;
            Debug.Log("<color=red>[InputService] Created:</color>");
            this.playerService = playerService;

            swipeDirection = new SwipeDirection();
            tapDetect = new TapDetect();
            //cameraInput = new CameraInput();
           //cameraInput.OnInitialized(this);
            //playerInput = new KeyboardInput();
            #if UNITY_ANDROID || UNITY_IOS
                        playerInput = new TouchInput();
            #elif UNITY_EDITOR || UNITY_STANDALONE
                        inputComponent = new KeyboardInput();
            #endif
            playerInput.OnInitialized(this);
        }

        public void PassDirection(Directions direction)
        {
            Debug.Log("[InputService] Setting Direction:" + direction);
            playerService.SetSwipeDirection(direction);
        }

        public void PassNodeID(int nodeID)
        {
            Debug.Log("[InputService] Setting NodeID:" + nodeID);
            playerService.SetTargetNode(nodeID);
        }

        public void Tick()
        {
            playerInput.OnTick();
            //cameraInput.OnTick();
        }


        public ISwipeDirection GetSwipeDirection()
        {
            return swipeDirection;
        }

        public ITapDetect GetTapDetect()
        {
            return tapDetect; 
        }

        public ICameraManager GetCameraManager()
        {
            return cameraManager; 
        }
    }
}