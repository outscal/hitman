using UnityEngine;
using System.Collections;
using Common;

namespace Player
{
    public class PlayerDoorUnlock : IPlayerState
    {
        IPlayerView playerView;
        PlayerStates currentStateType;

        public PlayerDoorUnlock(IPlayerView _playerView)
        {
            playerView = _playerView;
            currentStateType = PlayerStates.UNLOCK_DOOR;

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