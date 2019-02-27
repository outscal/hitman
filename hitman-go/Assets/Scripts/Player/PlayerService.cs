using Common;
using PathSystem;
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
        private PlayerScriptableObject playerScriptableObject;
        private Vector3 spawnLocation;
        private int playerNodeID;

        public PlayerService(IPathService _pathService, PlayerScriptableObject _playerScriptableObject, SignalBus signalBus)
        {
            _signalBus = signalBus;
            currentPathService = _pathService;
            playerScriptableObject = _playerScriptableObject;
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
            _signalBus.TryFire(new PlayerMoveSignal() { playerNodeID = nextNodeID });

        }

        public void SpawnPlayer()
        {
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
    }
}