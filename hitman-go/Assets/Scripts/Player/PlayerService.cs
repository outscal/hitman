using Common;
using Enemy;
using GameState;
using InteractableSystem;
using PathSystem;
using System;
using System.Collections;
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
        private PlayerStateMachine playerStateMachine;
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
            _signalBus.Subscribe<ResetSignal>(GameOver);
            _signalBus.Subscribe<GameStartSignal>(OnGameStart);

        }

        public void OnGameStart()
        {
            SpawnPlayer();
        }

        //swipe input
        public void SetSwipeDirection(Directions _direction)
        {
            if (gameService.GetCurrentState() != GameStatesType.PLAYERSTATE)
            {
                return;
            }
            if (gameService.GetCurrentState() == GameStatesType.GAMEOVERSTATE)
            {
                return;
            }

            int nextNodeID = currentPathService.GetNextNodeID(playerNodeID, _direction);

            if (nextNodeID == -1)
            {
                return;
            }
            PerformMovement(nextNodeID);


        }

        private void PerformMovement(int nextNodeID)
        {
            Vector3 nextLocation = currentPathService.GetNodeLocation(nextNodeID);
            playerController.MoveToLocation(nextLocation);
            playerNodeID = nextNodeID;
            if (CheckForInteractables(nextNodeID))
            {
                IInteractableController interactableController = interactableService.ReturnInteractableController(nextNodeID);
                PerformInteractableAction(interactableController);
            }
            if (CheckForFinishCondition())
            {
                Debug.Log("Game finished");
                _signalBus.TryFire(new StateChangeSignal() { newGameState = GameStatesType.LEVELFINISHEDSTATE });
            }
            _signalBus.TryFire(new StateChangeSignal() { newGameState = GameStatesType.ENEMYSTATE });

        }

        //interactable perform
        private void PerformInteractableAction(IInteractableController _interactableController)
        {
            int nodeID = GetTargetNode();
            switch (_interactableController.GetInteractablePickup())
            {
                case InteractablePickup.AMBUSH_PLANT:
                    playerStateMachine.ChangePlayerState(PlayerStates.AMBUSH);
                    _interactableController.TakeAction(playerNodeID);
                    break;
                case InteractablePickup.BONE:
                    playerStateMachine.ChangePlayerState(PlayerStates.WAIT_FOR_INPUT);
                    while (playerStateMachine.GetPlayerState() == PlayerStates.WAIT_FOR_INPUT)
                    {
                        nodeID = GetTargetNode();
                        if (nodeID != -1)
                        {
                            bool inRange = currentPathService.ThrowRange(playerNodeID, nodeID);
                            if (inRange)
                            {
                                playerStateMachine.ChangePlayerState(PlayerStates.THROWING);
                                _interactableController.TakeAction(nodeID);
                                break;
                            }
                        }
                    }
                    break;
                case InteractablePickup.BREIFCASE:
                    playerStateMachine.ChangePlayerState(PlayerStates.IDLE);
                    _interactableController.TakeAction(playerNodeID);
                    break;
                case InteractablePickup.COLOR_KEY:
                    playerStateMachine.ChangePlayerState(PlayerStates.UNLOCK_DOOR);
                    _interactableController.TakeAction(playerNodeID);
                    break;
                case InteractablePickup.DUAL_GUN:
                    playerStateMachine.ChangePlayerState(PlayerStates.SHOOTING);
                    _interactableController.TakeAction(playerNodeID);
                    break;
                case InteractablePickup.GUARD_DISGUISE:
                    playerStateMachine.ChangePlayerState(PlayerStates.DISGUISE);
                    _interactableController.TakeAction(playerNodeID);
                    break;
                case InteractablePickup.SNIPER_GUN:
                    playerStateMachine.ChangePlayerState(PlayerStates.WAIT_FOR_INPUT);
                    while (playerStateMachine.GetPlayerState() == PlayerStates.WAIT_FOR_INPUT)
                    {
                        nodeID = GetTargetNode();
                        if (nodeID != -1)
                        {
                            bool inRange = currentPathService.ThrowRange(playerNodeID, nodeID);
                            if (inRange)
                            {
                                playerStateMachine.ChangePlayerState(PlayerStates.SHOOTING);
                                _interactableController.TakeAction(nodeID);
                                break;
                            }
                        }
                    }
                    break;
                case InteractablePickup.STONE:
                    Debug.Log("Stone found");
                    playerStateMachine.ChangePlayerState(PlayerStates.WAIT_FOR_INPUT);
                    if (targetNode != -1)
                    { targetNode = -1; }
                    while (playerStateMachine.GetPlayerState() == PlayerStates.WAIT_FOR_INPUT)
                    {
                        nodeID = GetTargetNode();
                        Debug.Log("node iD in pickup" + nodeID);
                        if (nodeID != -1)
                        {
                            bool inRange = currentPathService.ThrowRange(playerNodeID, nodeID);

                            if (inRange)
                            {
                                Debug.Log("take action called");
                                playerStateMachine.ChangePlayerState(PlayerStates.IDLE);
                                _interactableController.TakeAction(nodeID);
                                //playerStateMachine.ChangePlayerState(PlayerStates.IDLE);
                                break;
                            }
                        }
                    }
                    break;
                case InteractablePickup.TRAP_DOOR:
                    playerStateMachine.ChangePlayerState(PlayerStates.WAIT_FOR_INPUT);
                    while (playerStateMachine.GetPlayerState() == PlayerStates.WAIT_FOR_INPUT)
                    {
                        nodeID = GetTargetNode();
                        if (nodeID != -1)
                        {
                            bool inRange = currentPathService.ThrowRange(playerNodeID, nodeID);
                            if (inRange)
                            {
                                playerStateMachine.ChangePlayerState(PlayerStates.IDLE);
                                _interactableController.TakeAction(nodeID);
                                break;
                            }
                        }
                    }
                    break;
            }
        }
        //dead trigger
        private void PlayerDead()
        {
            isPlayerDead = true;
            playerNodeID = -1;
            _signalBus.TryFire(new StateChangeSignal(){newGameState=GameStatesType.GAMEOVERSTATE});
        }
        //gameOver trigger
        private void GameOver()
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
            playerController.Reset();
            playerController = null;
        }

        //is game finished?
        private bool CheckForFinishCondition()
        {
            return currentPathService.CheckForTargetNode(playerNodeID);
        }

        public void SpawnPlayer()
        {

            playerNodeID = currentPathService.GetPlayerNodeID();
            spawnLocation = currentPathService.GetNodeLocation(playerNodeID);
            playerController = new PlayerController(this, spawnLocation, playerScriptableObject);
            playerStateMachine = playerController.GetCurrentStateMachine();
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

            if (playerStateMachine.GetPlayerState() == PlayerStates.SHOOTING || playerStateMachine.GetPlayerState() == PlayerStates.WAIT_FOR_INPUT || playerStateMachine.GetPlayerState() == PlayerStates.THROWING)
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
        private int GetTargetNode()
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

    }
}