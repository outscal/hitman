using Common;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerShootingState : IPlayerState
    {
        IPlayerView playerView;
        PlayerStates currentStateType;

        public PlayerShootingState(IPlayerView _playerView)
        {
            playerView = _playerView;
            currentStateType = PlayerStates.SHOOTING;

        }

        public void OnStateEnter()
        {
            playerView.PlayAnimation(currentStateType);
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