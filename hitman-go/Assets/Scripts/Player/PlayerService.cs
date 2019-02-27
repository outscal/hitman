using Common;
using PathSystem;
using Enemy;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerService : IPlayerService
    {

        private PlayerController playerController;
        private IPathService currentPathService;
        private IEnemyService currentEnemyService;
       
        private PlayerScriptableObject playerScriptableObject;
        private Vector3 spawnLocation;
        private int playerNodeID;

        public PlayerService(IPathService _pathService, IEnemyService _enemyService, PlayerScriptableObject _playerScriptableObject)
        {
            currentPathService = _pathService;
            currentEnemyService = _enemyService;
            playerScriptableObject = _playerScriptableObject;
        }

        public void SetSwipeDirection(Directions _direction)
        {
            int nextNodeID = currentPathService.GetNextNodeID(playerNodeID, _direction);
           if(nextNodeID==-1)
            {
                return;
            }
            Vector3 nextLocation = currentPathService.GetNodeLocation(nextNodeID);
            
            playerController.MoveToLocation(nextLocation);
            playerNodeID = nextNodeID;
            if(CheckForEnemyPresence())
            {
                KillEnemy();
            }

            

        }

        public void SpawnPlayer()
        {
            playerNodeID = currentPathService.GetPlayerNodeID();
            spawnLocation = currentPathService.GetNodeLocation(playerNodeID);
            playerController = new PlayerController(this, spawnLocation, playerScriptableObject);

        }

        public void IncreaseScore()
        {
            Debug.Log("increase score called");
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
            throw new System.NotImplementedException();
        }

        public bool CheckForEnemyPresence()
        {
           return currentEnemyService.CheckForEnemyPresence(playerNodeID);
        }
    }
}