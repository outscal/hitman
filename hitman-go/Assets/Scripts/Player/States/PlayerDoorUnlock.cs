using Common;
using InteractableSystem;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Player
{
    public class PlayerDoorUnlock : IPlayerState
    {
        IPlayerView playerView;
        PlayerStates currentStateType;

        IPlayerService playerService;
        PlayerStateMachine stateMachine;

        public PlayerDoorUnlock(IPlayerView _playerView, PlayerStateMachine playerStateMachine, IPlayerService _playerService)
        {
            playerView = _playerView;
            playerService = _playerService;
            stateMachine = playerStateMachine;
            currentStateType = PlayerStates.UNLOCK_DOOR;

        }

        public PlayerStates GetCurrentStateType()
        {
            return currentStateType;
        }

        async public void OnStateEnter(PlayerStates playerStates = PlayerStates.NONE, IInteractableController controller = null)
        {
            playerView.PlayAnimation(currentStateType);
            if (controller != null && playerStates != PlayerStates.NONE)
            {
                await new WaitForEndOfFrame();
            }
        }

        public void OnStateExit()
        {

        }
    }
}