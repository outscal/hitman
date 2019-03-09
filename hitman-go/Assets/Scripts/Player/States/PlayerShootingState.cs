using Common;
using InteractableSystem;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Player
{
    public class PlayerShootingState : IPlayerState
    {
        IPlayerView playerView;
        PlayerStates currentStateType;

        IPlayerService playerService;
        PlayerStateMachine stateMachine;

        public PlayerShootingState(IPlayerView _playerView, PlayerStateMachine playerStateMachine, IPlayerService _playerService)
        {
            playerView = _playerView;
            playerService = _playerService;
            stateMachine = playerStateMachine;
            currentStateType = PlayerStates.SHOOTING;

        }

        async public Task OnStateEnter(PlayerStates playerStates = PlayerStates.NONE, IInteractableController controller = null)
        {
            playerView.PlayAnimation(currentStateType);
            if (controller != null && playerStates != PlayerStates.NONE)
            {
                await new WaitForEndOfFrame();
            }
        }

        public void OnStateExit()
        {
            playerView.StopAnimation(currentStateType);
        }
        public PlayerStates GetCurrentStateType()
        {
            return currentStateType;
        }
    }
}