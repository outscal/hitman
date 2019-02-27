using Common;
using PathSystem;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerService : IPlayerService
    {
        private PlayerController playerController;
        private IPathService currentPathService;
        private PlayerDeathSignal playerDeathSignal;
        private PlayerScriptableObject playerScriptableObject;
        private Vector3 spawnLocation;
        private int playerNodeID;

        public PlayerService(IPathService _pathService, PlayerScriptableObject _playerScriptableObject)
        {
            currentPathService = _pathService;
            playerScriptableObject = _playerScriptableObject;
        }

        public void SetDirection(Directions _direction)
        {
            int nextNodeID = currentPathService.GetNextNodeID(playerNodeID, _direction);
<<<<<<< HEAD
            Vector3 nextLocation = currentPathService.GetNodeLocation(nextNodeID);
=======
            if(nextNodeID==-1)
            {
                return;
            }
            Vector3 nextLocation = currentPathService.GetNodeLocation(nextNodeID);
            
>>>>>>> fce40dc3f5bda65f2644f01f554157912183462a
            playerController.MoveToLocation(nextLocation);
            playerNodeID = nextNodeID;

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
    }
}