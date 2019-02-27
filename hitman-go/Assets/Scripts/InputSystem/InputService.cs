using UnityEngine;
using System;
using Common;
using Player;
using Zenject;

namespace InputSystem
{
    public class InputService : IInputService, ITickable
    {
        //private IPlayerService playerService;
        private IInputComponent inputComponent;
        private ISwipeDirection swipeDirection;

        public InputService (/*IPlayerService playerService*/)
        {
            Debug.Log("<color=red>[InputService] Created:</color>");
            //this.playerService = playerService;

            swipeDirection = new SwipeDirection();

            #if UNITY_ANDROID || UNITY_IOS
                        inputComponent = new TouchInput();
            #elif UNITY_EDITOR || UNITY_STANDALONE
                        inputComponent = new KeyboardInput();
            #endif
            inputComponent.OnInitialized(this);
        }

        public void PassDirection(Directions direction)
        {
            //playerService.SetSwipeDirection(direction);
        }

        public void PassNodeID(int nodeID)
        {
            //playerService.SetTargetNode(nodeID);
        }

        public void Tick()
        {
            inputComponent.OnTick();
        }

        public ISwipeDirection GetSwipeDirection()
        {
            return swipeDirection;
        }
    }
}