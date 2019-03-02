using Common;
using InteractableSystem;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerWaitingForInputState : IPlayerState
    {
        IPlayerView playerView;
        PlayerStates currentStateType;
        IPlayerService playerService;
        PlayerStateMachine stateMachine;

        public PlayerWaitingForInputState(IPlayerView _playerView,PlayerStateMachine playerStateMachine,IPlayerService _playerService)
        {
            playerView = _playerView;
            playerService = _playerService;
            stateMachine = playerStateMachine;
            currentStateType = PlayerStates.WAIT_FOR_INPUT;

        }

       async public Task OnStateEnter(PlayerStates playerStates = PlayerStates.NONE,IInteractableController controller = null )
        {           
            playerView.PlayAnimation(currentStateType);
            if(controller!=null && playerStates!= PlayerStates.NONE)
            {
                 await  NewWaitForTask(controller, playerStates);
            }
        }
        
        async private Task NewWaitForTask(IInteractableController _interactableController, PlayerStates playerState)
        {
            int nodeID;
            while (stateMachine.GetPlayerState() == this.currentStateType)
            {
                nodeID = playerService.GetTargetNode();

                if (nodeID != -1)
                {
                    bool inRange = playerService.CheckForRange(nodeID);                    

                    if (inRange)
                    {

                        Debug.Log("take action called");
                        stateMachine.ChangePlayerState(playerState,PlayerStates.NONE);
                        _interactableController.TakeAction(nodeID);
                        stateMachine.ChangePlayerState(PlayerStates.END_TURN,PlayerStates.NONE);
                        playerService.ChangeToEnemyState();
                    
                        break;


                    }
                }
                else
                {
                    await new WaitForEndOfFrame();
                }
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