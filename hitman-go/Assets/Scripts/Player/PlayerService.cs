using Common;
using Enemy;
using GameState;
using InteractableSystem;
using PathSystem;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerService : IPlayerService
    {
        readonly SignalBus signalBus;
        private IPlayerController playerController;
        private IPathService currentPathService;
        private PlayerDeathSignal playerDeathSignal;
        private PlayerScriptableObject playerScriptableObject;
        private Vector3 spawnLocation;
        private IGameService gameService;
        private IInteractable interactableService;

        private bool isPlayerDead = false;
        private int playerNodeID;
        private int targetNode;

        public PlayerService(IPathService _pathService, IGameService _gameService, IInteractable _interactableService, PlayerScriptableObject _playerScriptableObject, SignalBus _signalBus)
        {
            this.signalBus = _signalBus;
            gameService = _gameService;
            interactableService = _interactableService;
            currentPathService = _pathService;
            playerScriptableObject = _playerScriptableObject;
            this.signalBus.Subscribe<PlayerDeathSignal>(this.PlayerDead);
            this.signalBus.Subscribe<ResetSignal>(this.ResetLevel);
            this.signalBus.Subscribe<GameStartSignal>(this.OnGameStart);
            this.signalBus.Subscribe<StateChangeSignal>(this.OnStateChange);

        }

        public void OnGameStart()
        {
            SpawnPlayer();
        }
        public void OnStateChange()
        {
            if (gameService.GetCurrentState() == GameStatesType.PLAYERSTATE && playerController.GetPlayerState()!=PlayerStates.WAIT_FOR_INPUT)
            {
                playerController.ChangePlayerState(PlayerStates.IDLE, PlayerStates.NONE);
            }
        }

        //swipe input
        async public void SetSwipeDirection(Directions _direction)
        {


            if (gameService.GetCurrentState() != GameStatesType.PLAYERSTATE)
            {
                Debug.Log("player state nahi hai");
                return;
            }
            if (gameService.GetCurrentState() == GameStatesType.GAMEOVERSTATE)
            {
                return;
            }
            if (playerController.GetPlayerState() != PlayerStates.IDLE)
            {
                return;
            }

            playerController.ChangePlayerState(PlayerStates.END_TURN, PlayerStates.NONE);

            int nextNodeID = currentPathService.GetNextNodeID(playerNodeID, _direction);
            if (nextNodeID == -1)
            {
                playerController.ChangePlayerState(PlayerStates.IDLE, PlayerStates.NONE);
                return;
            }
            await PerformMovement(nextNodeID);         
            await new WaitForEndOfFrame();


        }

        async private Task PerformMovement(int nextNodeID)
        {
            if (gameService.GetCurrentState() != GameStatesType.PLAYERSTATE)
            {
                return;
            }

            playerNodeID = nextNodeID;
            Vector3 nextLocation = currentPathService.GetNodeLocation(nextNodeID);
            await playerController.MoveToLocation(nextLocation);


            if (CheckForInteractables(nextNodeID))
            {
                IInteractableController interactableController = interactableService.ReturnInteractableController(nextNodeID);
                PerformInteractableAction(interactableController);
            }
            if (IsGameFinished())
            {
                Debug.Log("Game finished");
                signalBus.TryFire(new LevelFinishedSignal());

            }
            else if (playerController.GetPlayerState() != PlayerStates.WAIT_FOR_INPUT)
            {
                gameService.ChangeToEnemyState();
            }

        }

        //interactable perform
        async private void PerformInteractableAction(IInteractableController _interactableController)
        {
            int nodeID = await GetTargetNode();

            switch (_interactableController.GetInteractablePickup())
            {
                case InteractablePickup.AMBUSH_PLANT:
                    await playerController.ChangePlayerState(PlayerStates.AMBUSH, PlayerStates.NONE);
                    _interactableController.TakeAction(playerNodeID);
                    gameService.ChangeToEnemyState();
                    break;
                case InteractablePickup.BONE:
                    if (targetNode != -1)
                    { targetNode = -1; }
                    await playerController.ChangePlayerState(PlayerStates.WAIT_FOR_INPUT, PlayerStates.THROWING, _interactableController);

                    break;
                case InteractablePickup.BREIFCASE:
                    await playerController.ChangePlayerState(PlayerStates.IDLE, PlayerStates.NONE);
                    _interactableController.TakeAction(playerNodeID);
                    gameService.ChangeToEnemyState();
                    break;
                case InteractablePickup.COLOR_KEY:
                    await playerController.ChangePlayerState(PlayerStates.IDLE, PlayerStates.NONE);
                    _interactableController.TakeAction(playerNodeID);
                    gameService.ChangeToEnemyState();
                    break;
                case InteractablePickup.DUAL_GUN:
                    await playerController.ChangePlayerState(PlayerStates.IDLE, PlayerStates.NONE);
                    _interactableController.TakeAction(playerNodeID);
                    gameService.ChangeToEnemyState();
                    break;
                case InteractablePickup.GUARD_DISGUISE:
                    await playerController.ChangePlayerState(PlayerStates.DISGUISE, PlayerStates.NONE);
                    _interactableController.TakeAction(playerNodeID);
                    gameService.ChangeToEnemyState();
                    break;
                case InteractablePickup.SNIPER_GUN:
                    if (targetNode != -1)
                    { targetNode = -1; }
                    await playerController.ChangePlayerState(PlayerStates.WAIT_FOR_INPUT, PlayerStates.SHOOTING, _interactableController);
                    break;
                case InteractablePickup.STONE:
                    Debug.Log("stone aya");
                    currentPathService.ShowThrowableNodes(playerNodeID);
                    if (targetNode != -1)
                    { targetNode = -1; }
                    await playerController.ChangePlayerState(PlayerStates.WAIT_FOR_INPUT, PlayerStates.THROWING, _interactableController);
                    break;
                case InteractablePickup.TRAP_DOOR:
                    if (targetNode != -1)
                    { targetNode = -1; }
                    await playerController.ChangePlayerState(PlayerStates.WAIT_FOR_INPUT, PlayerStates.UNLOCK_DOOR, _interactableController);
                    break;
            }
            await new WaitForEndOfFrame();

        }
        //dead trigger
        async private void PlayerDead()
        {
            isPlayerDead = true;
            playerNodeID = 0;
            await new WaitForSeconds(2f);
            gameService.ChangeToGameOverState();
        }
        //reset level trigger
        private void ResetLevel()
        {
            if (playerController == null)
            {
                return;
            }
            ResetEverything();
        }

        //reset calls
        private void ResetEverything()
        {
            isPlayerDead = false;
            playerController.Reset();
            playerController = null;
        }
        //is game finished?
        private bool IsGameFinished()
        {
            return currentPathService.CheckForTargetNode(playerNodeID);
        }

        public void SpawnPlayer()
        {
            playerNodeID = currentPathService.GetPlayerNodeID();
//            Debug.Log("player node spawn" + playerNodeID);
            spawnLocation = currentPathService.GetNodeLocation(playerNodeID);
            playerController = new PlayerController(this, spawnLocation, playerScriptableObject);
            signalBus.TryFire(new PlayerSpawnSignal());
        }

        //increase score on enemyKill etc 
        public void IncreaseScore()
        {
            signalBus.TryFire(new PlayerKillSignal());
        }
        //Get Tap Input
        async public void SetTargetNode(int _nodeID)
        {

            Debug.Log("inside tap detect");
            if (playerController.GetPlayerState() == PlayerStates.SHOOTING || playerController.GetPlayerState() == PlayerStates.WAIT_FOR_INPUT || playerController.GetPlayerState() == PlayerStates.THROWING || playerController.GetPlayerState() == PlayerStates.INTERMEDIATE_MOVE)
            {
                targetNode = _nodeID;
                //Debug.Log("target" + targetNode);
                return;
            }

            else if (gameService.GetCurrentState() != GameStatesType.PLAYERSTATE)
            {
                return;
            }
            else if (gameService.GetCurrentState() == GameStatesType.GAMEOVERSTATE)
            {
                return;
            }

            targetNode = _nodeID;

            if (currentPathService.CanMoveToNode(playerNodeID, _nodeID))
            {
                playerController.ChangePlayerState(PlayerStates.INTERMEDIATE_MOVE, PlayerStates.NONE);
                await PerformMovement(_nodeID);
            }
            await new WaitForEndOfFrame();
           

        }

        //get the node after tap
        async public Task<int> GetTargetNode()
        {
            return targetNode;
        }

        //return player node id
        public int GetPlayerNodeID()
        {
            return playerNodeID;
        }
        //is player dead?

        public bool PlayerDeathStatus()
        {
            return isPlayerDead;
        }

        //is interactable present
        private bool CheckForInteractables(int _nodeID)
        {
            return interactableService.CheckForInteractable(_nodeID);
        }

        public bool CheckForKillablePlayer()
        {
            if (playerController.GetPlayerState() == PlayerStates.AMBUSH)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckForRange(int _nodeID)
        {
            return currentPathService.ThrowRange(playerNodeID, _nodeID);
        }

        public void ChangeToEnemyState()
        {
            gameService.ChangeToEnemyState();
        }


    }
}