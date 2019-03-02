using Common;
using PathSystem.NodesScript;
using Player;
using System;
using UnityEngine;
using Zenject;

namespace InputSystem
{
    public class InputService : IInputService, ITickable
    {
        private IPlayerService playerService;

        private IInputComponent playerInput;
        private ISwipeDirection swipeDirection;
        private ITapDetect tapDetect;
        private GameObject tapObject;
        private int nodeLayer = 1 << 9;

        public InputService(IPlayerService playerService)
        {
            Debug.Log("<color=red>[InputService] Created:</color>");
            this.playerService = playerService;

            swipeDirection = new SwipeDirection();
            tapDetect = new TapDetect();
            //playerInput = new KeyboardInput();

#if UNITY_ANDROID || UNITY_IOS
            playerInput = new TouchInput();
#elif UNITY_EDITOR || UNITY_STANDALONE
            playerInput = new KeyboardInput();
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
        }


        public ISwipeDirection GetSwipeDirection()
        {
            return swipeDirection;
        }

        public ITapDetect GetTapDetect()
        {
            return tapDetect;
        }
    }
}