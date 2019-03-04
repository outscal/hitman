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

            playerController.ChangePlayerState(PlayerStates.INTERMEDIATE_MOVE, PlayerStates.NONE);

            playerController.PerformAction(_direction);
               
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
                       
            playerController = new PlayerController(this,gameService,currentPathService, interactableService, playerScriptableObject);
            signalBus.TryFire(new PlayerSpawnSignal());
            playerNodeID = playerController.GetID();
        }

        //increase score on enemyKill etc 
        public void IncreaseScore()
        {
            signalBus.TryFire(new PlayerKillSignal());
        }
        //Get Tap Input
        async public void SetTargetNode(int _nodeID)
        {
            
            if (playerController.GetPlayerState() == PlayerStates.SHOOTING || playerController.GetPlayerState() == PlayerStates.WAIT_FOR_INPUT || playerController.GetPlayerState() == PlayerStates.THROWING || playerController.GetPlayerState() == PlayerStates.INTERMEDIATE_MOVE)
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
                playerController.ChangePlayerState(PlayerStates.INTERMEDIATE_MOVE, PlayerStates.NONE);
               var _direction= currentPathService.GetDirections(playerNodeID,_nodeID);
                playerController.PerformAction(_direction);
            }
            await new WaitForEndOfFrame();
           

        }

        //get the node after tap
        public int GetTargetNode()
        {
            return targetNode;
        }

        //return player node id
        public int GetPlayerNodeID()
        {
            return playerController.GetID();
        }
        //is player dead?

        public bool PlayerDeathStatus()
        {
            return isPlayerDead;
        }
       

        public bool CheckForKillablePlayer()
        {
            return playerController.GetPlayerState() == PlayerStates.AMBUSH;
            
        }

        public bool CheckForRange(int _nodeID)
        {
            return currentPathService.ThrowRange(playerNodeID, _nodeID);
        }

        public void ChangeToEnemyState()
        {
            gameService.ChangeToEnemyState();
        }

        public void FireLevelFinishedSignal()
        {
            signalBus.TryFire(new LevelFinishedSignal());
        }

        public void SetTargetTap(int v)
        {
            targetNode = v;
        }
    }
}