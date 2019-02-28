using UnityEngine;
using Common;
using System.Collections;

namespace Player
{
    public class PlayerThrowingState : IPlayerState
    {
        IPlayerView playerView;
        PlayerStates currentStateType;
        public PlayerThrowingState(IPlayerView _playerView)
        {
            currentStateType = PlayerStates.THROWING;
            playerView = _playerView;
            
        }

        public PlayerStates GetCurrentStateType()
        {
            return currentStateType;
            
        }

        public void OnStateEnter()
        {
            playerView.PlayAnimation(currentStateType);
        }

        public void OnStateExit()
        {
           
        }
    }
}