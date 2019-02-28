using Common;
using Enemy;
using PathSystem;
using GameState;
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
        private bool isPlayerDead = false;
        private int playerNodeID;

        public PlayerService(IPathService _pathService, IGameService _gameService, PlayerScriptableObject _playerScriptableObject, SignalBus signalBus)
        {
            _signalBus = signalBus;
            gameService = _gameService;

            currentPathService = _pathService;
            playerScriptableObject = _playerScriptableObject;

            _signalBus.Subscribe<PlayerDeathSignal>(PlayerDead);
            _signalBus.Subscribe<GameOverSignal>(GameOver);
            _signalBus.Subscribe<GameStartSignal>(OnGameStart);

        }

        public void OnGameStart()
        {
            Debug.Log("PlayerSpawned");
            SpawnPlayer();
        }

        public void SetSwipeDirection(Directions _direction)
        {
            if (gameService.GetCurrentState() != GameStatesType.PLAYERSTATE)
            {
                return;
            }
            if(gameService.GetCurrentState()==GameStatesType.GAMEOVERSTATE)
            {
                return;                    
            }
            int nextNodeID = currentPathService.GetNextNodeID(playerNodeID, _direction);
            if (nextNodeID == -1)
            {
                return;
            }
            Vector3 nextLocation = currentPathService.GetNodeLocation(nextNodeID);        
            playerController.MoveToLocation(nextLocation);
            playerNodeID = nextNodeID;
            if (CheckForFinishCondition())
            {
                Debug.Log("Game finished");
                _signalBus.TryFire(new StateChangeSignal() { newGameState = GameStatesType.LEVELFINISHEDSTATE });
            }
            _signalBus.TryFire(new StateChangeSignal() { newGameState = GameStatesType.ENEMYSTATE });

        }
        private void PlayerDead()
        {
            playerController.DisablePlayer();
            playerController = null;
            playerNodeID = -1;
            isPlayerDead = true;
            _signalBus.TryFire(new GameOverSignal());
        }
        private void GameOver()
        {
            _signalBus.Unsubscribe<PlayerDeathSignal>(PlayerDead);
            _signalBus.TryFire(new StateChangeSignal() { newGameState = GameStatesType.GAMEOVERSTATE });
        }
        private bool CheckForFinishCondition()
        {
            return currentPathService.CheckForTargetNode(playerNodeID);
        }
        public void SpawnPlayer()
        {

            playerNodeID = currentPathService.GetPlayerNodeID();
            spawnLocation = currentPathService.GetNodeLocation(playerNodeID);
            playerController = new PlayerController(this, spawnLocation
                                                  , playerScriptableObject);
            _signalBus.TryFire(new PlayerSpawnSignal());

        }

        public void IncreaseScore()
        {
            _signalBus.TryFire(new PlayerKillSignal());
        }

        public void SetTargetNode(int _nodeID)
        {

        }

        public int GetPlayerNodeID()
        {
            return playerNodeID;
        }
        public bool PlayerDeathStatus()
        {
            return isPlayerDead;
        }
    }
}