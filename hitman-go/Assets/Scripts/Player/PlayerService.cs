using Common;
using Enemy;
using PathSystem;
using GameState.Signals;
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
        private IEnemyService currentEnemyService;
        private PlayerScriptableObject playerScriptableObject;
        private Vector3 spawnLocation;
        private int playerNodeID;

        public PlayerService(IPathService _pathService, IEnemyService _enemyService, PlayerScriptableObject _playerScriptableObject, SignalBus signalBus)
        {
            _signalBus = signalBus;
            
            currentPathService = _pathService;
            playerScriptableObject = _playerScriptableObject;
            currentEnemyService=_enemyService;
        }

        public void SetSwipeDirection(Directions _direction)
        {
            int nextNodeID = currentPathService.GetNextNodeID(playerNodeID, _direction);
            if (nextNodeID == -1)
            {
                return;
            }
            Vector3 nextLocation = currentPathService.GetNodeLocation(nextNodeID);

            playerController.MoveToLocation(nextLocation);
            playerNodeID = nextNodeID;
            if (CheckForEnemyPresence())
            {
                KillEnemy();
            }
            if (CheckForFinishCondition())
            {
                Debug.Log("Game finished");
            }
            _signalBus.TryFire(new StateChangeSignal());
          //  currentEnemyService.PerformMovement();

        }

        private bool CheckForFinishCondition()
        {
            return currentPathService.CheckForTargetNode(playerNodeID);
        }

        public void SpawnPlayer()
        {
            currentEnemyService.SetPlayerService(this);
            playerNodeID = currentPathService.GetPlayerNodeID();
            spawnLocation = currentPathService.GetNodeLocation(playerNodeID);
            playerController = new PlayerController(this, spawnLocation, playerScriptableObject);
            _signalBus.TryFire(new PlayerSpawnSignal());

        }

        public void IncreaseScore()
        {
            Debug.Log("increase score called");
            _signalBus.TryFire(new PlayerKillSignal());
        }

        public void SetTargetNode(int _nodeID)
        {

        }

        public int GetPlayerNodeID()
        {
            return playerNodeID;
        }

        public void KillEnemy()
        {
            // throw new System.NotImplementedException();
            
            _signalBus.TryFire(new EnemyDeathSignal() { nodeID = playerNodeID });
            
        }

        public bool CheckForEnemyPresence()
        {
            return currentEnemyService.CheckForEnemyPresence(playerNodeID);
        }

    }
}