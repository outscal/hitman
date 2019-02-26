using UnityEngine;
using System;
using Common;
using Player;
using Zenject;

namespace InputSystem
{
    public class InputService : IInputService, ITickable
    {
        private IPlayerService playerService;

        private IInputComponent inputComponent;

        public InputService (/*IPlayerService playerService*/)
        {
            Debug.Log("<color=red>[InputService] Created:</color>");
            //this.playerService = playerService;

            //inputComponent = new KeyboardInput();
            inputComponent = new TouchInput();

            //#if UNITY_ANDROID || UNITY_IOS
            //            inputComponent = new TouchInput();
            //#elif UNITY_EDITOR || UNITY_STANDALONE
            //            inputComponent = new KeyboardInput();
            //#endif
            inputComponent.OnInitialized(this);
        }

        public void PassDirection(Directions direction)
        {
            //playerService.SetDirection(direction);
        }

        public void Tick()
        {
            inputComponent.OnTick();
        }
    }
}