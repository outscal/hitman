using Common;
using GameState;
using InteractableSystem;
using System;
using System.Collections;
using PathSystem;
using System.Threading.Tasks;
using UnityEngine;

namespace Player
{
    public class PlayerController : IPlayerController
    {
        private IPlayerService playerService;
        private IPlayerView currentPlayerView;
        private IGameService gameService;
        private IInteractable interactableService;
        private IPathService pathService;
        private PlayerStateMachine playerStateMachine;
        private PlayerScriptableObject scriptableObject;
        private int playerNodeID;
        GameObject playerInstance;
        private Vector3 spawnLocation;

        public PlayerController(IPlayerService _playerService, IGameService _gameService, IPathService _pathService, IInteractable _interactableService, PlayerScriptableObject _playerScriptableObject)
        {
            playerService = _playerService;            
            pathService = _pathService;
            gameService = _gameService;
            interactableService = _interactableService;

            scriptableObject = _playerScriptableObject;

            playerNodeID = pathService.GetPlayerNodeID();
            spawnLocation = pathService.GetNodeLocation(playerNodeID);

            SpawnPlayerView();
        }

        public Vector3 GetCurrentLocation()
        {
            return spawnLocation;
        }

        async public Task MoveToLocation(Vector3 _location)
        {
            await currentPlayerView.MoveToLocation(_location);
        }

        private void SpawnPlayerView()
        {
            // currentPlayerView=scriptableObject.playerView;
            playerInstance = GameObject.Instantiate(scriptableObject.playerView.gameObject);
            currentPlayerView = playerInstance.GetComponent<PlayerView>();
            playerStateMachine = new PlayerStateMachine(currentPlayerView, playerService);
            playerNodeID = 0;
            playerInstance.transform.localPosition = spawnLocation;

        }

        public void DisablePlayer()
        {
            currentPlayerView.DisablePlayer();
        }

        public void Reset()
        {
            currentPlayerView.Reset();
        }

        private PlayerStateMachine GetCurrentStateMachine()
        {
            return playerStateMachine;
        }

        async public Task ChangePlayerState(PlayerStates _state, PlayerStates stateToChange, IInteractableController interactableController = null)
        {
            await playerStateMachine.ChangePlayerState(_state, stateToChange, interactableController);

        }
        public PlayerStates GetPlayerState()
        {
            return playerStateMachine.GetPlayerState();
        }

        async public Task PerformMovement(int nextNodeID)
        {
            playerNodeID = nextNodeID;
            Vector3 nextLocation = pathService.GetNodeLocation(nextNodeID);
            await MoveToLocation(nextLocation);

            if (interactableService.CheckForInteractable(nextNodeID))
            {
                IInteractableController interactableController = interactableService.ReturnInteractableController(nextNodeID);
                PerformInteractableAction(interactableController);
            }
            if (IsGameFinished())
            {
                
                playerService.FireLevelFinishedSignal();                

            }
            else if (GetPlayerState() != PlayerStates.WAIT_FOR_INPUT)
            {
                gameService.ChangeToEnemyState();
            }

        }
        async public void PerformInteractableAction(IInteractableController _interactableController)
        {
            int nodeID = playerService.GetTargetNode();

            switch (_interactableController.GetInteractablePickup())
            {
                case InteractablePickup.AMBUSH_PLANT:
                    await  ChangePlayerState(PlayerStates.AMBUSH, PlayerStates.NONE);
                    _interactableController.TakeAction(playerNodeID);              
                    break;
                case InteractablePickup.BONE:
                    pathService.ShowThrowableNodes(playerNodeID);
                    playerService.SetTargetTap(-1);
                    await  ChangePlayerState(PlayerStates.WAIT_FOR_INPUT, PlayerStates.THROWING, _interactableController);

                    break;
                case InteractablePickup.BREIFCASE:
                    await  ChangePlayerState(PlayerStates.IDLE, PlayerStates.NONE);
                    _interactableController.TakeAction(playerNodeID);
                   
                    break;
                case InteractablePickup.COLOR_KEY:
                    await  ChangePlayerState(PlayerStates.IDLE, PlayerStates.NONE);
                    _interactableController.TakeAction(playerNodeID);
                   
                    break;
                case InteractablePickup.DUAL_GUN:
                    await  ChangePlayerState(PlayerStates.IDLE, PlayerStates.NONE);
                    _interactableController.TakeAction(playerNodeID);
                   
                    break;
                case InteractablePickup.GUARD_DISGUISE:
                    await  ChangePlayerState(PlayerStates.DISGUISE, PlayerStates.NONE);
                    _interactableController.TakeAction(playerNodeID);
                  
                    break;
                case InteractablePickup.SNIPER_GUN:
                    playerService.SetTargetTap(-1);
                    await  ChangePlayerState(PlayerStates.WAIT_FOR_INPUT, PlayerStates.SHOOTING, _interactableController);
                    break;
                case InteractablePickup.STONE:                    
                    pathService.ShowThrowableNodes(playerNodeID);
                    playerService.SetTargetTap(-1);
                    await  ChangePlayerState(PlayerStates.WAIT_FOR_INPUT, PlayerStates.THROWING, _interactableController);
                    break;
                case InteractablePickup.TRAP_DOOR:
                    playerService.SetTargetTap(-1);
                    await  ChangePlayerState(PlayerStates.WAIT_FOR_INPUT, PlayerStates.UNLOCK_DOOR, _interactableController);
                    break;
            }
            await new WaitForEndOfFrame();

        }

        public int GetID()
        {
            return playerNodeID;
        }

        private bool IsGameFinished()
        {
            return pathService.CheckForTargetNode(playerNodeID);
        }

        async  public void PerformAction(Directions _direction )
        {
           
            int nextNodeID = pathService.GetNextNodeID(GetID(), _direction);
            if (nextNodeID == -1)
            {
                ChangePlayerState(PlayerStates.IDLE, PlayerStates.NONE);
                return;
            }
            await PerformMovement(nextNodeID);

           

        }
    }
}