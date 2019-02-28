using Common;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerWaitingForInputState : IPlayerState
    {
        IPlayerView playerView;
        PlayerStates currentStateType;

        public PlayerWaitingForInputState(IPlayerView _playerView)
        {
            playerView = _playerView;
            currentStateType = PlayerStates.WAIT_FOR_INPUT;

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