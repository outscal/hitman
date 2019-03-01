using UnityEngine;
using InteractableSystem;
using Common;
using System.Threading.Tasks;
using System.Collections;

namespace Player
{
    
    public class PlayerStateMachine
    {
        private IPlayerState currentState;
        private IPlayerState previousState;
        private IPlayerView playerView;
        private IPlayerService playerService;

        public PlayerStateMachine(IPlayerView _playerView,IPlayerService _playerService)
        {
            playerView = _playerView;
            playerService = _playerService;
            ChangePlayerState(PlayerStates.IDLE,PlayerStates.NONE);
        }

        async public Task ChangePlayerState(PlayerStates _state,PlayerStates stateToChange,IInteractableController interactableController= null)
        {
            previousState = currentState;
            if (previousState != null)
            {                
                previousState.OnStateExit();
            }
            switch(_state)
            {
                case PlayerStates.AMBUSH:
                    currentState = new PlayerAmbushState(playerView,this,playerService);
                  
                    break;
                case PlayerStates.DISGUISE:
                    currentState = new PlayerDisguiseState(playerView,this,playerService);
              
                    break;
                case PlayerStates.IDLE:
                    currentState = new PlayerIdleState(playerView,this,playerService);
                 
                    break;
                    case PlayerStates.SHOOTING:
                    currentState = new PlayerShootingState(playerView,this,playerService);               
                    break;
                case PlayerStates.UNLOCK_DOOR:
                    currentState = new PlayerDoorUnlock(playerView,this,playerService);
                    currentState.OnStateEnter();
                    break;

                case PlayerStates.WAIT_FOR_INPUT:
                    currentState = new PlayerWaitingForInputState(playerView,this,playerService);         
                    break;
                case PlayerStates.THROWING:
                    currentState = new PlayerThrowingState(playerView,this,playerService);                  
                    break;

                default:
                    currentState = new PlayerIdleState(playerView,this,playerService);                
                    break;
            }
            if (interactableController != null && stateToChange!=PlayerStates.NONE)
               await currentState.OnStateEnter(stateToChange,interactableController);
            else
               await currentState.OnStateEnter();

        }
        public PlayerStates GetPlayerState()
        {
            return currentState.GetCurrentStateType();
        }
    }
}