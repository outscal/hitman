using UnityEngine;
using Common;
using System.Collections;

namespace Player
{
    public class PlayerIdleState : IPlayerState
    {
        IPlayerView playerView;
        PlayerStates currentStateType;

        public PlayerIdleState(IPlayerView _playerView)
        {
            currentStateType = PlayerStates.IDLE;
            playerView = _playerView;

        }

        public void OnStateEnter()
        {
            playerView.PlayAnimation(PlayerStates.AMBUSH);
        }

        public void OnStateExit()
        {

        }
        public PlayerStates GetCurrentStateType()
        {
            return currentStateType;
        }
      
    }
}