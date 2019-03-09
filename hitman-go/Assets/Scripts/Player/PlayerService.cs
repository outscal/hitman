using CameraSystem; //
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
        private ICamera camera;

        private bool isPlayerDead = false;
        private int playerNodeID;
        private int targetNode;

        public PlayerService(IPathService _pathService, IGameService _gameService, IInteractable _interactableService, PlayerScriptableObject _playerScriptableObject, SignalBus _signalBus, ICamera camera) //
        {
            this.signalBus = _signalBus;
            this.camera = camera;
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
            if (gameService.GetCurrentState() == GameStatesType.PLAYERSTATE && playerController.GetPlayerState() != PlayerStates.WAIT_FOR_INPUT )//&& playerController.GetPlayerState() != PlayerStates.AMBUSH)
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

            camera.SetNodeID(GetPlayerNodeID());

        }


        //dead trigger
        async private void PlayerDead()
        {
            isPlayerDead = true;
            playerNodeID = 0;
            await new WaitForSeconds(.75f);
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

            playerController = new PlayerController(this, gameService, currentPathService, interactableService, playerScriptableObject);
            signalBus.TryFire(new PlayerSpawnSignal());
            playerNodeID = playerController.GetID();
            playerController.ChangePlayerState(PlayerStates.IDLE, PlayerStates.NONE);
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

            if (currentPathService.CanMoveToNode(GetPlayerNodeID(), _nodeID))
            {


                playerController.ChangePlayerState(PlayerStates.INTERMEDIATE_MOVE, PlayerStates.NONE);

                playerController.PerformMovement(_nodeID);

            }

            await new WaitForEndOfFrame();
            camera.SetNodeID(GetPlayerNodeID());


        }

        //get the node after tap
        public int GetTargetNode()
        {
            return targetNode;
        }

        //return player node id
        public int GetPlayerNodeID()
        {
            if (playerController == null)
                return 0;

            return playerController.GetID();
        }
        //is player dead?

        public bool PlayerDeathStatus()
        {
            return isPlayerDead;
        }


        public bool CheckForKillablePlayer(EnemyType enemyType)
        {
            bool killable = true;

            if (playerController.GetPlayerState() == PlayerStates.AMBUSH)
            {
                return false;
            }
            else
            {
                if (playerController.GetDisguiseType() != EnemyType.None)
                {
                    
                    if (enemyType == playerController.GetDisguiseType())
                    { return false; }
                }
            }
          
            return killable;


        }

        public bool CheckForRange(int _nodeID)
        {
            return currentPathService.ThrowRange(GetPlayerNodeID(), _nodeID);
        }

        public void ChangeToEnemyState()
        {
            gameService.ChangeToEnemyState();
        }

        public void FireLevelFinishedSignal()
        {

            signalBus.TryFire(new LevelFinishedSignal());
        }

        public void SetTargetTap(int tapNode)
        {
            targetNode = tapNode;
        }

        public SignalBus GetSignalBus()
        {
            return signalBus;
        }
    }
}