using UnityEngine;
using Common;
using System.Collections;

namespace Player
{
    public class PlayerAmbushState : IPlayerState
    {
        IPlayerView playerView;
        PlayerStates currentStateType;
        public PlayerAmbushState(IPlayerView _playerView)
        {
            currentStateType = PlayerStates.AMBUSH;
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