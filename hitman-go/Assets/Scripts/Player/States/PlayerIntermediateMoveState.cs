using Common;
using InteractableSystem;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Player
{
    public class PlayerIntermediateMoveState : IPlayerState
    {
        IPlayerView playerView;
        PlayerStates currentStateType;

        IPlayerService playerService;
        PlayerStateMachine stateMachine;

        public PlayerIntermediateMoveState(IPlayerView _playerView, PlayerStateMachine playerStateMachine, IPlayerService _playerService)
        {
            playerView = _playerView;
            playerService = _playerService;
            stateMachine = playerStateMachine;
            currentStateType = PlayerStates.INTERMEDIATE_MOVE;

        }

        async public Task OnStateEnter(PlayerStates playerStates = PlayerStates.NONE, IInteractableController controller = null)
        {           
            if (controller != null && playerStates != PlayerStates.NONE)
            {
                await new WaitForEndOfFrame();
            }
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