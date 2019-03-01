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
        readonly SignalBus _signalBus;
        private PlayerController playerController;
        private IPathService currentPathService;
        private PlayerDeathSignal playerDeathSignal;
        private PlayerScriptableObject playerScriptableObject;
        private Vector3 spawnLocation;
        private IGameService gameService;
        private IInteractable interactableService;
 
        private bool isPlayerDead = false;
        private int playerNodeID;
        private int targetNode;

        public PlayerService(IPathService _pathService, IGameService _gameService, IInteractable _interactableService, PlayerScriptableObject _playerScriptableObject, SignalBus signalBus)
        {
            _signalBus = signalBus;
            gameService = _gameService;
            interactableService = _interactableService;
            currentPathService = _pathService;
            playerScriptableObject = _playerScriptableObject;
            _signalBus.Subscribe<PlayerDeathSignal>(PlayerDead);
            _signalBus.Subscribe<ResetSignal>(ResetLevel);
            _signalBus.Subscribe<GameStartSignal>(OnGameStart);

        }

        public void OnGameStart()
        {
            SpawnPlayer();
        }

        //swipe input
        async public void SetSwipeDirection(Directions _direction)
        {
            if (gameService.GetCurrentState() != GameStatesType.PLAYERSTATE)
            {
                return;
            }
            if (gameService.GetCurrentState() == GameStatesType.GAMEOVERSTATE)
            {
                return;
            }
            if (playerController.GetPlayerState() != PlayerStates.IDLE)
            {
                await new WaitForEndOfFrame();
                return;
            }

            int nextNodeID = currentPathService.GetNextNodeID(playerNodeID, _direction);
            
            if (nextNodeID == -1)
            {
                return;
            }
            
            PerformMovement(nextNodeID);


        }

        async private void PerformMovement(int nextNodeID)
        {
            Vector3 nextLocation = currentPathService.GetNodeLocation(nextNodeID);
            await playerController.MoveToLocation(nextLocation);
            
            playerNodeID = nextNodeID;

            if (CheckForInteractables(nextNodeID))
            {
                IInteractableController interactableController = interactableService.ReturnInteractableController(nextNodeID);
                PerformInteractableAction(interactableController);
            }
            if (IsGameFinished())
            {
                Debug.Log("Game finished");
                _signalBus.TryFire(new LevelFinishedSignal());
                //trigger level finished signal

            }
            else if (playerController.GetPlayerState() != PlayerStates.WAIT_FOR_INPUT)
            {
                gameService.ChangeToEnemyState();
                //_signalBus.TryFire(new StateChangeSignal() { newGameState = GameStatesType.ENEMYSTATE });
            }
        }

        //interactable perform
       async private void PerformInteractableAction(IInteractableController _interactableController)
        {
            int nodeID = GetTargetNode();

            switch (_interactableController.GetInteractablePickup())
            {
                case InteractablePickup.AMBUSH_PLANT:
                  await  playerController.ChangePlayerState(PlayerStates.AMBUSH, PlayerStates.NONE);
                    _interactableController.TakeAction(playerNodeID);
                    break;
                case InteractablePickup.BONE:
                    if (targetNode != -1)
                    { targetNode = -1; }
                   await playerController.ChangePlayerState(PlayerStates.WAIT_FOR_INPUT, PlayerStates.THROWING, _interactableController);
                    break;
                case InteractablePickup.BREIFCASE:
                  await  playerController.ChangePlayerState(PlayerStates.IDLE, PlayerStates.NONE);
                    //push test
                    _interactableController.TakeAction(playerNodeID);
                    break;
                case InteractablePickup.COLOR_KEY:
                   await playerController.ChangePlayerState(PlayerStates.IDLE, PlayerStates.NONE);
                    _interactableController.TakeAction(playerNodeID);
                    break;
                case InteractablePickup.DUAL_GUN:
                   await playerController.ChangePlayerState(PlayerStates.IDLE, PlayerStates.NONE);
                    _interactableController.TakeAction(playerNodeID);
                    break;
                case InteractablePickup.GUARD_DISGUISE:
                 await   playerController.ChangePlayerState(PlayerStates.DISGUISE, PlayerStates.NONE);
                    _interactableController.TakeAction(playerNodeID);
                    break;
                case InteractablePickup.SNIPER_GUN:
                    if (targetNode != -1)
                    { targetNode = -1; }
                   await playerController.ChangePlayerState(PlayerStates.WAIT_FOR_INPUT, PlayerStates.SHOOTING, _interactableController);
                    break;
                case InteractablePickup.STONE:
                    currentPathService.ShowThrowableNodes(playerNodeID);
                    if (targetNode != -1)
                    { targetNode = -1; }
                   await playerController.ChangePlayerState(PlayerStates.WAIT_FOR_INPUT, PlayerStates.THROWING, _interactableController);
                    break;
                case InteractablePickup.TRAP_DOOR:
                    if (targetNode != -1)
                    { targetNode = -1; }
                   await playerController.ChangePlayerState(PlayerStates.WAIT_FOR_INPUT,PlayerStates.UNLOCK_DOOR,_interactableController);               
                    break;
            }
        }
        async private void NewWaitForTask(IInteractableController _interactableController, PlayerStates playerState)
        {
            int nodeID;
            while (playerStateMachine.GetPlayerState() == PlayerStates.WAIT_FOR_INPUT)
            {
                nodeID = GetTargetNode();

                if (nodeID != -1)
                {
                    bool inRange = currentPathService.ThrowRange(playerNodeID, nodeID);

                    if (inRange)
                    {
                        Debug.Log("take action called");
                        playerStateMachine.ChangePlayerState(playerState);
                        _interactableController.TakeAction(nodeID);
                        playerStateMachine.ChangePlayerState(PlayerStates.IDLE);
                        gameService.ChangeToEnemyState();
                        //_signalBus.TryFire(new StateChangeSignal() { newGameState = GameStatesType.ENEMYSTATE });
                        break;


                    }
                }
                else
                {
                    await new WaitForEndOfFrame();
                }
            }
        }
        //dead trigger
        async private void PlayerDead()
        {
            isPlayerDead = true;
            playerNodeID = -1;
            await new WaitForSeconds(2f);
            gameService.ChangeToGameOverState();
            //_signalBus.TryFire(new StateChangeSignal() { newGameState = GameStatesType.GAMEOVERSTATE });
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
            Debug.Log("player node spawn" + playerNodeID);
            spawnLocation = currentPathService.GetNodeLocation(playerNodeID);
            playerController = new PlayerController(this, spawnLocation, playerScriptableObject);            
            _signalBus.TryFire(new PlayerSpawnSignal());
        }

        //increase score on enemyKill etc 
        public void IncreaseScore()
        {
            _signalBus.TryFire(new PlayerKillSignal());
        }
        //Get Tap Input
        public void SetTargetNode(int _nodeID)
        {

            if (playerController.GetPlayerState() == PlayerStates.SHOOTING || playerController.GetPlayerState() == PlayerStates.WAIT_FOR_INPUT || playerController.GetPlayerState() == PlayerStates.THROWING)
            {
                targetNode = _nodeID;
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
                
                PerformMovement(_nodeID);
            }

        }

        //get the node after tap
        public int GetTargetNode()
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
            _signalBus.TryFire(new StateChangeSignal() { newGameState = GameStatesType.ENEMYSTATE });
        }


    }
}