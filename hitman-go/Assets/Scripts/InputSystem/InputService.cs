using UnityEngine;
using System;
using Common;
using Player;

namespace InputSystem
{
    public class InputService : IInputService
    {
        private IPlayerService playerService;

        public InputService (IPlayerService playerService)
        {
            this.playerService = playerService;    
        }

        public void PassDirection(Directions direction)
        {
            playerService.SetDirection(direction);
        }
    }
}