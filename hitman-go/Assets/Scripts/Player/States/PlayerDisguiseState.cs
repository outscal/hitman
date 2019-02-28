using Common;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerDisguiseState : IPlayerState
    {
        IPlayerView playerView;
        PlayerStates currentStateType;
        public PlayerDisguiseState(IPlayerView _playerView)
        {
            currentStateType = PlayerStates.DISGUISE;
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