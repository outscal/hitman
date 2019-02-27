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

        private IInputComponent playerInput;

        public InputService (/*IPlayerService playerService*/)
        {
            Debug.Log("<color=red>[InputService] Created:</color>");
            //this.playerService = playerService;

            #if UNITY_ANDROID || UNITY_IOS
                        playerInput = new TouchInput();
            #elif UNITY_EDITOR || UNITY_STANDALONE
                        inputComponent = new KeyboardInput();
            #endif
            playerInput.OnInitialized(this);
        }

        public void PassDirection(Directions direction)
        {
            //playerService.SetDirection(direction);
        }

        public void Tick()
        {
            playerInput.OnTick();
        }

        public void DetectTap()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycast;

            if(Physics.Raycast(ray,out raycast))
            {
                 
            }

        }
    }
}