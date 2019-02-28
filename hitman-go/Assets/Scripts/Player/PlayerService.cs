using Common;
using Enemy;
using GameState.Interface;
using GameState.Signals;
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

        public PlayerService(IPathService _pathService, IGameService _gameService, IInteractable _interactableService, PlayerScriptableObject _playerScriptableObject, SignalBus signalBus)
        {
            _signalBus = signalBus;
            gameService = _gameService;
            interactableService = _interactableService;
            currentPathService = _pathService;
            playerScriptableObject = _playerScriptableObject;

            _signalBus.Subscribe<PlayerDeathSignal>(PlayerDead);
            _signalBus.Subscribe<GameOverSignal>(GameOver);
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
            if (CheckForInteractables(nextNodeID))
            {
                //InteractablePickup interactable = interactableService.getPickup();
                //PerformInteractableAction();
            }
            Vector3 nextLocation = currentPathService.GetNodeLocation(nextNodeID);
            playerController.MoveToLocation(nextLocation);
            playerNodeID = nextNodeID;
            if (CheckForFinishCondition())
            {
                Debug.Log("Game finished");
            }
            _signalBus.TryFire(new StateChangeSignal());

        }

        //interactable perform
        private void PerformInteractableAction(InteractablePickup _interactablePickup)
        {
            switch (_interactablePickup)
            {
                case InteractablePickup.AMBUSH_PLANT:
                    
                    playerStateMachine.ChangePlayerState(PlayerStates.AMBUSH);
                    //interactable.takeaction();

                    break;
                case InteractablePickup.BONE:
                    break;
                case InteractablePickup.BREIFCASE:
                    break;
                case InteractablePickup.COLOR_KEY:
                    break;
                case InteractablePickup.DUAL_GUN:
                    break;
                case InteractablePickup.GUARD_DISGUISE:
                    break;
                case InteractablePickup.SNIPER_GUN:
                    break;
                case InteractablePickup.STONE:
                    break;
                case InteractablePickup.TRAP_DOOR:
                    break;
            }
        }

        //dead trigger
        private void PlayerDead()
        {
            playerController.DisablePlayer();
            playerController = null;
            playerNodeID = -1;
            isPlayerDead = true;
            _signalBus.TryFire(new GameOverSignal());
        }
        //gameOver trigger
        private void GameOver()
        {
            ResetEverything();
            Debug.Log("GameOver");
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

        //spawn Players
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
            //IInterface
            return false;

        }

    }
}