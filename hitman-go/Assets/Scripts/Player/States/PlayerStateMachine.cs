using UnityEngine;
using Common;
using System.Collections;

namespace Player
{
    
    public class PlayerStateMachine
    {
        private IPlayerState currentState;
        private IPlayerState previousState;
        private IPlayerView playerView;

        public PlayerStateMachine(IPlayerView _playerView)
        {
            playerView = _playerView;
            
            ChangePlayerState(PlayerStates.IDLE);
        }

        public void ChangePlayerState(PlayerStates _state)
        {
            previousState = currentState;
            if (previousState != null)
            {                
                previousState.OnStateExit();
            }
            switch(_state)
            {
                case PlayerStates.AMBUSH:
                    currentState = new PlayerAmbushState(playerView);
                    currentState.OnStateEnter();
                    break;
                case PlayerStates.DISGUISE:
                    currentState = new PlayerDisguiseState(playerView);
                    currentState.OnStateEnter();
                    break;
                case PlayerStates.IDLE:
                    currentState = new PlayerIdleState(playerView);
                    currentState.OnStateEnter();
                    break;
                    case PlayerStates.SHOOTING:
                    currentState = new PlayerShootingState(playerView);
                    currentState.OnStateEnter();
                    break;
                case PlayerStates.UNLOCK_DOOR:
                    currentState = new PlayerDoorUnlock(playerView);
                    currentState.OnStateEnter();
                    break;

                case PlayerStates.WAIT_FOR_INPUT:
                    currentState = new PlayerWaitingForInputState(playerView);
                    currentState.OnStateEnter();
                    break;

                default:
                    currentState = new PlayerIdleState(playerView);
                    currentState.OnStateEnter();
                    break;



            }

        }
        public PlayerStates GetPlayerState()
        {
            return currentState.GetCurrentStateType();
        }
    }
}