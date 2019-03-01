using UnityEngine;
using Common;
using InteractableSystem;
using System.Threading.Tasks;
using System.Collections;

namespace Player
{
    public class PlayerThrowingState : IPlayerState
    {
        IPlayerView playerView;
        PlayerStates currentStateType;
        IPlayerService playerService;
        PlayerStateMachine stateMachine;

        public PlayerThrowingState(IPlayerView _playerView, PlayerStateMachine playerStateMachine, IPlayerService _playerService)
        {
            playerView = _playerView;
            playerService = _playerService;
            stateMachine = playerStateMachine;
            currentStateType = PlayerStates.THROWING;

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